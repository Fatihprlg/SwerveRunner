using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    private int totalGems;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject InGameMenu;
    [SerializeField] GameObject passedMenu;
    [SerializeField] GameObject failedMenu;

    public static GameController Instance { get { return _instance; } }

    [HideInInspector] public bool isFailed = false, isSuccess = false;
    [HideInInspector] public int gemMultiplier;
    [HideInInspector] public int maxHealth;

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


    public void AddTotalGems(int value)
    {
        totalGems += value;
        PlayerPrefs.SetInt("TotalGems", totalGems);
    }

    public void StartGame()
    {
        if (!isSuccess && !isFailed)
        {
            CharacterControl.Instance.isGameRunning = true;
            CharacterControl.Instance.animatorController.SetBool("isRunning", true);
            this.mainMenu.SetActive(false);
            this.InGameMenu.SetActive(true);
        }
    }

    public void LevelPassed()
    {
        CharacterControl.Instance.isGameRunning = false;
        AddTotalGems(CharacterControl.Instance.collectedGems);
        CharacterControl.Instance.animatorController.SetBool("isVictory", true);
        isSuccess = true;
        passedMenu.SetActive(true);
        InGameMenu.SetActive(false);
    }

    public void LevelFailed()
    {
        CharacterControl.Instance.animatorController.SetBool("isFailed", true);
        CharacterControl.Instance.animatorController.SetBool("isRunning", false);
        CharacterControl.Instance.isGameRunning = false;
        isFailed = true;
        failedMenu.SetActive(true);
        InGameMenu.SetActive(false);
    }

    public void ReturnMenu() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void NextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings >= SceneManager.GetActiveScene().buildIndex + 2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else ReturnMenu();
        
    }

    public bool SetGemsMultiplier()
    {
        if (totalGems >= (gemMultiplier * 10))
        {
            totalGems -= (gemMultiplier * 10);
            gemMultiplier++;
            PlayerPrefs.SetInt("GemsMultiplier", gemMultiplier);
            PlayerPrefs.SetInt("TotalGems", totalGems);
            return true;
        }
        else return false;
    }
    public bool SetMaxHealth()
    {
        if (totalGems >= (maxHealth * 20))
        {
            totalGems -= (maxHealth * 20);
            maxHealth++;
            PlayerPrefs.SetInt("MaxHealth", maxHealth);
            PlayerPrefs.SetInt("TotalGems", totalGems);
            CharacterControl.Instance.Refresh();
            return true;
        }
        else return false;
    }
}
