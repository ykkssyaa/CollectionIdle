using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{

    #region Serialized Fields
    [SerializeField] private PlayerCollector collector;

    private List<EggInform> eggs = new List<EggInform> {
        new EggInform("Белое", 1, 0.85f),
        new EggInform("Коричневое", 3, 0.08f),
        new EggInform("Серебрянное", 10, 0.05f),
        new EggInform("Золотое", 50, 0.018f),
        new EggInform("Алмазное", 100, 0.002f),

    };

    public EggInform[] egg = {
        new EggInform("Белое", 1, 0.85f),
        new EggInform("Коричневое", 3, 0.08f),
        new EggInform("Серебрянное", 10, 0.05f),
        new EggInform("Золотое", 50, 0.018f),
        new EggInform("Алмазное", 100, 0.002f),
        new EggInform("Редкое", 0, 0) };

    public CollectEggInfo[] rareEggs;

    public Shop shop;
    #endregion

    #region Fields
    [Space(30)]
    [Header("Fields")]

    // Шансы выпадения яиц(6 - специальное яйцо)
    [SerializeField] private float[] weights = new float[6] { 0, 0, 0, 0, 0, 0};

    // Сколько кликов будет делать автокликер за один раз
    public int clicksPerTic = 0;
    public int clicksPerClick = 1;
    // Раз в сколько секунд будет работать автокликер
    [SerializeField] private int secondsInTic = 1;

    // Количество кликов для получения одного яйца
    public int clicksToEarn = 1;
    // Текущее количество произведенных кликов
    public int curClicks = 0;

    public string locName;
    public bool isActive = false;

    public float luckValue = 0.0f;

    #endregion

    public IEnumerator AutoClicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsInTic);

            for(int i = 0; i < clicksPerTic; i++)
            {
                MakeTic();
            }
            
        }
    }

    #region Methods

    public void InitLocation()
    {
        CheckWeights();
        shop = InitShop();

        StartCoroutine(AutoClicker());
        
        for(int i = 0; i < eggs.Count; i++)
        {
            eggs[i].weight = weights[i];
        }
    }

    // Инициализация класса с ценами на улучшения
    public Shop InitShop()
    {
        Shop s = new Shop(1, 1, 1);

        return s;
    }

    // Проверка корректности ввода шансов выпадения
    private void CheckWeights()
    {
        float res = 0;

        foreach(float w in weights)
            res += w;

        string logStr = string.Format("Location {0} has wrong chance weights, sum = {1}", locName, res);

        if(res > 1 || res <= 0) Debug.Log(logStr);
    } 

    // Сделать клик 
    public void MakeTic()
    {
        Debug.Log("Make Tic in Location");

        curClicks += clicksPerClick;

        
        if(curClicks >= clicksToEarn)
        {
            if (clicksToEarn == 0)
                return;

            int c = curClicks / clicksToEarn;

            for(int i = 0; i < c; i++)
            {
                EarnRandomEgg();
            }

            curClicks = curClicks % clicksToEarn;
        }

        collector.gameUI.UpdateClicksToReward();
    }

    // Определение, какое яйцо выпадет
    private void EarnRandomEgg()
    {
        Debug.Log("Earn New Egg in " + locName) ;

        float val = Random.value;
        float t = 0;

        foreach(var item in eggs)
        {
            t += item.weight;

            if(t >= val)
            {
                collector.UpdateCollection(item.name);
                break;
            }
        }
    }

    #region Upgrade 

    public void UpgradeClickPrice(int value = 1)
    {
        clicksPerClick = clicksPerClick + value;
    }

    public void UpgradeClicksPerTick(int value = 1)
    {
        clicksPerTic = clicksPerTic + value;
    }

    public void UpgradeLuck(float value = 0.005f)
    {
        luckValue = luckValue + value;
    }

    #endregion

    #endregion
}

// Класс для хранения информации о текущих ценах на улучшения в магазине
public class Shop
{
    public int ClickPricePrice;
    public int ClicksPerTickPrice;
    public int LuckPrice;

    public Shop(int ClickPricePrice, int ClicksPerTickPrice, int LuckPrice)
    {
        this.ClickPricePrice = ClickPricePrice;
        this.LuckPrice = LuckPrice;
        this.ClicksPerTickPrice = ClicksPerTickPrice;
    }
}

// Класс для того чтоб задать вероятности выпадения валют - яиц на локации
[System.Serializable]
public class EggInform : Egg
{
    public float weight;

    public EggInform(string n, int p, float weight) : base(n, p)
    {
        this.weight = weight;
    }

    public EggInform(string n, int p) : base(n, p)
    {
        this.weight = 0;
    }
}

public enum Locations
{
    Location1 = 1, 
    Location2,
    Location3,
    Location4,
    Location5
}

