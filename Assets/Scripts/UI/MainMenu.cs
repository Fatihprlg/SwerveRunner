using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gemsTxt;
    [SerializeField] TextMeshProUGUI multiplierTxt;
    [SerializeField] GameObject soundButton;
    [SerializeField] GameObject shopMenu;


    private int isSoundMuted;
    private static MainMenu _instance;

    public static MainMenu Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        Refresh();

        isSoundMuted = PlayerPrefs.GetInt("isSoundMuted", 0);
    }
    

    public void SoundButton()
    {

        if(isSoundMuted == 1)
        {
            soundButton.GetComponent<Image>().color = new Color32(126, 126, 126, 255);
            PlayerPrefs.SetInt("isSoundMuted", 0);
            AudioListener.volume = 1;
            isSoundMuted = 0;
        }
        else
        {
            soundButton.GetComponent<Image>().color = Color.white;
            PlayerPrefs.SetInt("isSoundMuted", 1);
            AudioListener.volume = 0;
            isSoundMuted = 1;
        }
    }
    
    public void SettingsButton()
    {
        if (soundButton.activeInHierarchy)
        {
            soundButton.SetActive(false);
        }
        else
        {
            soundButton.SetActive(true);
        }
    }
    
    public void ShopButton()
    {
        if (shopMenu.activeInHierarchy)
        {
            shopMenu.SetActive(false);
            Refresh();
        }
        else
        {
            shopMenu.SetActive(true);
        }
    }

    public void Refresh()
    {
        gemsTxt.text = PlayerPrefs.GetInt("TotalGems", 0).ToString();
        multiplierTxt.text = "x" + PlayerPrefs.GetInt("GemsMultiplier", 1).ToString();
    }
}
