using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    #region Serialized Fields

    [Header("Serialized Fields")]

    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerCollector collector;

    [Space]
    [SerializeField] private TextMeshProUGUI MoneyText;

    [SerializeField] private TextMeshProUGUI whiteEgg;
    [SerializeField] private TextMeshProUGUI brownEgg;
    [SerializeField] private TextMeshProUGUI silverEgg;
    [SerializeField] private TextMeshProUGUI goldenEgg;
    [SerializeField] private TextMeshProUGUI dimondEgg;

    [Space]
    [SerializeField] private TextMeshProUGUI clicksToReward;
    #endregion

    #region Button Methods

    public void OnClick()
    {
        Debug.Log("Make Tic in GameUI");
        gameManager.MakeTic();
    }

    #endregion

    #region Update Canvas Methods
    
    public void InitUI()
    {
        UpdateMoneyText("0");
        UpdateEggText();
        UpdateClicksToReward();
    }

    public void UpdateMoneyText(string text)
    {
        MoneyText.text = text;
    }

    public void UpdateEggText()
    {
        List<int>egc = collector.GetEggsCounts();

        if (egc.Count != 5) { 
            Debug.Log("Problem with UpdateEggText");
            return;
        }

        whiteEgg.text = egc[0].ToString();
        brownEgg.text = egc[1].ToString();
        silverEgg.text = egc[2].ToString();
        goldenEgg.text = egc[3].ToString();
        dimondEgg.text = egc[4].ToString();
    }

    public void UpdateClicksToReward()
    {
        Location loc = gameManager.GetActiveLocation();

        if (loc == null) return;

        clicksToReward.text = loc.curClicks.ToString() + "/" + loc.clicksToEarn.ToString();
    }

    #endregion
}
