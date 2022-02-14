using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool isFailed = false, isSuccess = false;
    //public int gemMultiplier;

    private int totalGems;
    

    void Start()
    {
        //gemMultiplier = PlayerPrefs.GetInt("GemsMultiplier", 1);
        totalGems = PlayerPrefs.GetInt("TotalGems", 0);
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        StartGame();
    }

    public void AddTotalGems(int value)
    {
        totalGems += value;
        PlayerPrefs.SetInt("TotalGems", totalGems);
    }

    void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CharacterControl.instance.isGameRunning = true;
            CharacterControl.instance.animatorController.SetBool("isRunning", true);
        }
    }
   /* public void SetGemsMultiplier(int value)
    {
        gemMultiplier = value;
        PlayerPrefs.SetInt("GemsMultiplier", value);
    }*/
}
