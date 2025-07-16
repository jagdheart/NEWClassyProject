using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed = 75f;
    public float jumpForce;
    public Rigidbody rig;
    public int coinCount;
    public int health;

    public Animator anim;

  void Move()
    {
        // get the input axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
       Vector3 rotation = Vector3.up * x;
       Quaternion anglerRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;
        rig.velocity = dir;
       // rig.MoveRotation(rig.rotation * anglerRot);
       if(Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void TryJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, 1.5f)) {
            anim.SetTrigger("isJumping");
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
        if(health <= 0)
        {
            anim.SetBool("die", true);
            StartCoroutine("die");
        }
    }
    IEnumerator DieButCool()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.name == "Enemy")
        {
            health -= 10;
        }
       if(other.gameObject.name == "Fallcollider")
        {
            SceneManager.LoadScene(0);
        }
    }
}
