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

    [SerializeField] private GameObject GameScreen;
    [SerializeField] private GameObject ShopScreen;

    [Space]
    [SerializeField] private TextMeshProUGUI MoneyText;

    [SerializeField] private TextMeshProUGUI whiteEgg;
    [SerializeField] private TextMeshProUGUI brownEgg;
    [SerializeField] private TextMeshProUGUI silverEgg;
    [SerializeField] private TextMeshProUGUI goldenEgg;
    [SerializeField] private TextMeshProUGUI dimondEgg;

    [Space]
    [SerializeField] private TextMeshProUGUI clicksToReward;
    [SerializeField] private TextMeshProUGUI locationName;

    [Space]
    [Header("Shop")]
    [SerializeField] private TextMeshProUGUI ClickPriceValue;
    [SerializeField] private TextMeshProUGUI ClickPricePrice;
    [SerializeField] private TextMeshProUGUI ClicksPerTickValue;
    [SerializeField] private TextMeshProUGUI ClicksPerTickPrice;
    [SerializeField] private TextMeshProUGUI LuckValue;
    [SerializeField] private TextMeshProUGUI LuckPrice;

    #endregion

    #region Button Methods

    public void OnClick()
    {
        gameManager.MakeTic();
    }

    public void ShopButton()
    {
        GameScreen.SetActive(false);
        ShopScreen.SetActive(true);
    }

    public void ExitShopButton()
    {
        GameScreen.SetActive(true);
        ShopScreen.SetActive(false);
    }

    #region Shop

    public void OnUpgradeClickPrice() 
    {
        // Check for balance(method)

        gameManager.GetActiveLocation().UpgradeClickPrice();
        UpdateShop();
    }

    public void OnUpgradeClickPerTick() 
    {
        // Check for balance(method)

        gameManager.GetActiveLocation().UpgradeClicksPerTick();
        UpdateShop();
    }

    public void OnUpgradeLuck()
    {
        // Check for balance(method)

        gameManager.GetActiveLocation().UpgradeLuck();
        UpdateShop();
    }

    #endregion

    #endregion

    #region Update Canvas Methods

    public void InitUI()
    {
        UpdateMoneyText("0");
        UpdateEggText();
        UpdateClicksToReward();
        UpdateShop();
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

    // Update Information in Shop
    public void UpdateShop()
    {
        Location activeLoc= gameManager.GetActiveLocation();

        if (activeLoc == null) return;

        // Update Info

        ClicksPerTickValue.text = activeLoc.clicksPerTic.ToString();
        ClicksPerTickPrice.text = activeLoc.shop.ClicksPerTickPrice.ToString();

        ClickPriceValue.text = activeLoc.clicksPerClick.ToString();
        ClicksPerTickPrice.text = activeLoc.shop.ClickPricePrice.ToString();

        LuckValue.text = activeLoc.luckValue.ToString("0.###");
        LuckPrice.text = activeLoc.shop.LuckPrice.ToString();

    }

    public void ChangeLocationName(string name)
    {
        locationName.text = name;
        UpdateClicksToReward();
    }

    #endregion
}
