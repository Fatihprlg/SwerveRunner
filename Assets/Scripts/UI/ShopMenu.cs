using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{

    [SerializeField] Button gemMultiplierBtn;
    [SerializeField] TextMeshProUGUI gemMultiplierPriceTxt;
    [SerializeField] Text gemMultiplierCurrentTxt;
    [SerializeField] Button healthUpgradeBtn;
    [SerializeField] TextMeshProUGUI healthUpgradePriceTxt;
    [SerializeField] Text healthUpgradeCurrentTxt;
    [SerializeField] GameObject notEnoughMoneyTxt;
    [SerializeField] GameObject shopMenu;


    [SerializeField] int maxMultiplier;
    [SerializeField] int maxHealthUpgrade;
    int maxHealth;
    int gemMultiplier;

    void Start()
    {
        
        maxHealth = PlayerPrefs.GetInt("MaxHealth", 3);
        gemMultiplier = PlayerPrefs.GetInt("GemsMultiplier", 1);

        gemMultiplierPriceTxt.text = (gemMultiplier * 10).ToString();
        gemMultiplierCurrentTxt.text = gemMultiplier.ToString();
        healthUpgradePriceTxt.text = (maxHealth * 20).ToString();
        healthUpgradeCurrentTxt.text = maxHealth.ToString();

    }


    public void UpgradeHealth()
    {
        bool isSuccess = false;
        if (maxHealth < maxHealthUpgrade)
        {
            isSuccess = GameController.Instance.SetMaxHealth();
            if (isSuccess)
            {
                maxHealth++;
                healthUpgradeCurrentTxt.text = maxHealth.ToString();
                healthUpgradePriceTxt.text = (maxHealth * 20).ToString();
            }
            else StartCoroutine(ShowMoneyTxt());
        }

        if (maxHealth == maxHealthUpgrade)
        {
            healthUpgradeBtn.interactable = false;
        }

    }
    public void UpgradeGemMultiplier()
    {
        bool isSuccess = false;
        if (gemMultiplier < maxMultiplier)
        {
            isSuccess = GameController.Instance.SetGemsMultiplier();
            if (isSuccess)
            {
                gemMultiplier++;
                gemMultiplierCurrentTxt.text = gemMultiplier.ToString();
                gemMultiplierPriceTxt.text = (gemMultiplier * 10).ToString();
            }
            else StartCoroutine(ShowMoneyTxt());

        }

        if (gemMultiplier == maxMultiplier)
        {
            gemMultiplierBtn.interactable = false;  
        }
    }
    
    public void BackButton()
    {
        shopMenu.SetActive(false);
        MainMenu.Instance.Refresh();
    }

    IEnumerator ShowMoneyTxt()
    {
        notEnoughMoneyTxt.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        notEnoughMoneyTxt.SetActive(false);
    }
}
