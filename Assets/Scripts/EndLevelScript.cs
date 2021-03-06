using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelScript : MonoBehaviour
{
    [SerializeField] private AudioSource winSound;
    public Button nextLevelButton;
    public Button replayButton;
    public GameObject NextLevelUI;


    private void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        CompleteLevel();
    }

    private void CompleteLevel()
    {
       
        PlaySound();
        NextLevelUI.SetActive(true);
    }


    void PlaySound()
    {
        winSound.Play();
        Destroy(winSound, 4f);
    }


    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    // Bu kod refactor edilip ScoreManager üzerinden daha sonra çekilecek
    // public void FirstLevelScore()
    // {
    //     if (Time.timeSinceLevelLoad <= 40 && PlayerManager.money >= 5 || Time.timeSinceLevelLoad <= 40 ||
    //         PlayerManager.money >= 5)
    //     {
    //         scoreText.enabled = true;
    //         scoreText.text = "3 STAR";
    //     }
    //
    //     else if (Time.timeSinceLevelLoad >= 40 && Time.timeSinceLevelLoad <= 60 && PlayerManager.money >= 3 ||
    //              Time.timeSinceLevelLoad >= 40 && Time.timeSinceLevelLoad <= 60 ||
    //              PlayerManager.money >= 3 && PlayerManager.money < 5)
    //     {
    //         scoreText.enabled = true;
    //         scoreText.text = "2 STAR";
    //     }
    //
    //     else
    //     {
    //         scoreText.enabled = true;
    //         scoreText.text = "1 STAR";
    //     }
    // }
}