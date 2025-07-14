using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Runner").GetComponent<PlayerController>();
    }
     void OnTriggerEnter(Collider other)
    {
        if (other.name == "Runner")
        {
            player.coinCount++;
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
