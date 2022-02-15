using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public static GameController Instance { get { return _instance; } }
    public bool isFailed = false, isSuccess = false;

    public int gemMultiplier;
    public int maxHealth;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject InGameMenu;
    [SerializeField] GameObject passedMenu;
    [SerializeField] GameObject failedMenu;
    private int totalGems;

    void Start()
    {
        gemMultiplier = PlayerPrefs.GetInt("GemsMultiplier", 1);
        totalGems = PlayerPrefs.GetInt("TotalGems", 0);
        maxHealth = PlayerPrefs.GetInt("MaxHealth", 3);
        mainMenu.SetActive(true);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void FixedUpdate()
    {
        if (!MainMenu.Instance.onMenu)
            StartGame();
    }


    public void AddTotalGems(int value)
    {
        totalGems += value;
        PlayerPrefs.SetInt("TotalGems", totalGems);
    }

    public void StartGame()
    {
        if (Input.GetMouseButtonDown(0) && (!isSuccess && !isFailed))
        {
            CharacterControl.Instance.isGameRunning = true;
            CharacterControl.Instance.animatorController.SetBool("isRunning", true);
            mainMenu.SetActive(false);
            InGameMenu.SetActive(true);
        }
    }

    public void LevelPassed()
    {
        CharacterControl.Instance.isGameRunning = false;
        AddTotalGems(CharacterControl.Instance.collectedGems);
        CharacterControl.Instance.animatorController.SetBool("isVictory", true);
        isSuccess = true;
        passedMenu.SetActive(true);
        Invoke("NextLevel", 3f);
    }

    public void LevelFailed()
    {
        CharacterControl.Instance.animatorController.SetBool("isFailed", true);
        CharacterControl.Instance.animatorController.SetBool("isRunning", false);
        CharacterControl.Instance.isGameRunning = false;
        isFailed = true;
        failedMenu.SetActive(true);
        Invoke("ReturnMenu", 2f);
    }

    void ReturnMenu() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    void NextLevel()
    {
        if (SceneManager.sceneCount >= SceneManager.GetActiveScene().buildIndex + 2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else ReturnMenu();
        
    }

    public bool SetGemsMultiplier()
    {
        if (totalGems >= (gemMultiplier * 10))
        {
            gemMultiplier++;
            PlayerPrefs.SetInt("GemsMultiplier", gemMultiplier);
            totalGems -= (maxHealth * 20);
            PlayerPrefs.SetInt("TotalGems", totalGems);
            return true;
        }
        else return false;
    }
    public bool SetMaxHealth()
    {
        if (totalGems >= (maxHealth * 20))
        {
            maxHealth++;
            PlayerPrefs.SetInt("MaxHealth", maxHealth);
            totalGems -= (maxHealth * 20);
            PlayerPrefs.SetInt("TotalGems", totalGems);
            CharacterControl.Instance.Refresh();
            return true;
        }
        else return false;
    }
}
