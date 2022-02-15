using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelEndMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI incomingGemsTxt;
    [SerializeField] TextMeshProUGUI totalGemsTxt;

    int _collectedGems;
    int _totalGems;

    void Start()
    {
        _collectedGems = CharacterControl.Instance.collectedGems;
        _totalGems = PlayerPrefs.GetInt("TotalGems", 0);

        incomingGemsTxt.text = _collectedGems.ToString();
        totalGemsTxt.text = _totalGems.ToString();
    }
}
