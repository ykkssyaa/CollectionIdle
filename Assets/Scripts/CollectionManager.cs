using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private Sprite LockedSprite;

    // ÷вета дл€ названи€ €йца в коллекции 
    [SerializeField] private RareColor[] Colors = new RareColor[6];
    // –одитель всех €иц в иерархии
    [SerializeField] private Transform content;
    [SerializeField] private PlayerCollector playerCollector;


    [SerializeField] private GameObject InformationEggScreen; 
    [SerializeField] private GameObject CollectionContentScreen;

    private CollectEgg activeEgg;

    public List<CollectEgg> AllRareEggs = new List<CollectEgg>();

    #region Information Text
    [Space(20)]
    [Header("Information Text")]
    [SerializeField] private TextMeshProUGUI eggNameText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image eggIcon;

    #endregion

    #region Celling Eggs

    [Space(10)]

    private int cellCount = 0;
    private int cellPrice = 0;
    [SerializeField] private TextMeshProUGUI cellCountText;
    [SerializeField] private TextMeshProUGUI cellPriceText;

    public void PlusMinusButton(int value)
    {
        if (value > 0 && cellCount + value >= activeEgg.count) return;
        else if(value < 0 && cellCount + value < 0) return;

        cellCount += value;
        cellCountText.text = cellCount.ToString();

        cellPrice = cellCount * activeEgg.Data.price;
        cellPriceText.text = cellPrice.ToString();
    }

    public void CellButton()
    {
        if(cellCount == 0) return;

        // Add money
        playerCollector.UpdateMoney(cellPrice);
        
        // Update values
        cellPrice = 0;
        activeEgg.count -= cellCount;
        cellCount = 0;

        // Update UI
        cellPriceText.text = "0";
        cellCountText.text = "0";
        countText.text = activeEgg.count.ToString();
    }

    #endregion

    public void LoadRareEggs()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            AllRareEggs.Add(content.GetChild(i).GetComponent<CollectEgg>());
        }
    }

    public void InitCollection()
    {
        LoadRareEggs();

        foreach(var egg in AllRareEggs)
        {
            egg.InitEgg();
        }
    }

    public void OnEggClick(CollectEgg egg)
    {
        if (egg.count <= 0) return;

        activeEgg = egg;

        // Update Egg Info
        eggNameText.text = egg.Data.eggName;
        countText.text = egg.count.ToString();
        descriptionText.text = egg.Data.description; 
        priceText.text = egg.Data.price.ToString();

        eggIcon.sprite = activeEgg.Data.icon;

        // Update Celling info
        cellPrice = 0; cellCount = 0;
        cellPriceText.text = "0";
        cellCountText.text = "0";

        InformationEggScreen.SetActive(true);
        CollectionContentScreen.SetActive(false);
    }
}

[System.Serializable]
public class RareColor
{
    public Color color;
    public EggRare rare;
}
