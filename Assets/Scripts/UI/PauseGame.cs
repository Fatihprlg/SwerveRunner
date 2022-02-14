using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] GameObject pauseMenu;
    Animator pauseAnimator;

    private void Start()
    {
        pauseAnimator = pauseMenu.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseAnimator.SetBool("menuClosing", false);
        pauseAnimator.SetBool("menuOpening", true);
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseAnimator.SetBool("menuOpening", false);
        pauseAnimator.SetBool("menuClosing", true);
        isGamePaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
