using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI healthTxt;
    [SerializeField] TextMeshProUGUI levelTxt;

    private static InGameUI _instance;

    public static InGameUI Instance { get { return _instance; } }

    void Start()
    {
        levelTxt.text = SceneManager.GetActiveScene().name.ToUpper();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void UpdateHealthTxt(int value) => healthTxt.text = value.ToString();
    public void UpdateScoreTxt(int value) => scoreTxt.text = value.ToString();
}
