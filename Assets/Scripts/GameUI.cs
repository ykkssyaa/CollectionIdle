using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    #region Serialized Fields

    [Header("Serialized Fields")]

    public GameManager gameManager;

    [Space]
    [SerializeField] private TextMeshProUGUI MoneyText;

    #endregion

    #region Unity Methods

    void Start()
    {
        InitUI();
    }

    #endregion

    #region Button Methods

    public void OnClick()
    {
        gameManager.UpdateMoneyOnClick();
    }

    #endregion

    #region Update Canvas Methods
    
    public void InitUI()
    {
        UpdateMoneyText("0");
    }

    public void UpdateMoneyText(string text)
    {
        MoneyText.text = text;
    }

    #endregion
}
