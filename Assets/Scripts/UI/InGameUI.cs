using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI healthTxt;
    [SerializeField] TextMeshProUGUI levelTxt;

    public static InGameUI instance;

    void Start()
    {
        levelTxt.text = SceneManager.GetActiveScene().name.ToUpper();
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateHealthTxt(int value) => healthTxt.text = value.ToString();
    public void UpdateScoreTxt(int value) => scoreTxt.text = value.ToString();
}
