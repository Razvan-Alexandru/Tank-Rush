using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas overlay;
    public GameObject resumeRestart;
    public GameObject mainMenu;
    public TMP_Text scoreText;
    private bool isPaused = false;
    public bool isGameOver = false;

    private GameObject player;
    void Start()  {
        gameObject.GetComponent<Canvas>().enabled = false;
        overlay.enabled = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        scoreText.text = player.GetComponent<Player>().score.ToString();

        if(!isGameOver && Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }

        if(isGameOver) {
            Time.timeScale = 0f;
            isPaused = false;
            overlay.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameObject.GetComponent<Canvas>().enabled = true;
        }

    }

    void Pause() {
        if(!isPaused) {
            isPaused = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            overlay.enabled = false;
            gameObject.GetComponent<Canvas>().enabled = true;
        }
        else if(isPaused) {
            isPaused = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.GetComponent<Canvas>().enabled = false;
            overlay.enabled = true;
        }
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
