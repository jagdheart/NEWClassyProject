using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Variables")]
    public PlayerController player;
    public float time;
    public bool timeActive;


    [Header("Game UI")]
    public TMP_Text gameUI_score;
    public TMP_Text gameUI_health;
    public TMP_Text gameUI_time;



    [Header("countdown UI")]
    public int countdown;
    public TMP_Text countdownText;

    [Header("End Screen UI")]
    public TMP_Text endUI_score;
    public TMP_Text endUI_time;

    [Header("Screens")]
    public GameObject countdownUI;
    public GameObject gameUI;
    public GameObject endUI;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip yay;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Runner").GetComponent<PlayerController>();
        time = 0;
        player.enabled = false;
        SetScreen(countdownUI);
        StartCoroutine(CountDownRoutine());

    }

    IEnumerator CountDownRoutine() {
        countdownText.gameObject.SetActive(true);
        countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "START";
        yield return new WaitForSeconds(1f);
        player.enabled = true;

        startGame();

    }

    void startGame()
    {
        timeActive = true;
        SetScreen(gameUI);

    }
    public void endGame()
    {
        timeActive = false;
        player.enabled = false;
        endUI_score.text = "companies: " + player.coinCount;
        endUI_time.text = "time: " + (time * 1).ToString("F2");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetScreen(endUI);
        source.clip = yay;
        source.Play();



    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeActive)
        {
            time = time + Time.deltaTime;
        }
        gameUI_score.text = "companies: " + player.coinCount;
        gameUI_health.text = "money in billions: " + player.health;
        gameUI_time.text = "Time: " + (time * 1).ToString("F2");
    }

    public void SetScreen(GameObject screen)
    {
        gameUI.SetActive(false);
        countdownUI.SetActive(false);
        endUI.SetActive(false);
        screen.SetActive(true);
    }
}
