using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    #region Fields

    public int money;
    
     public GameUI gameUI;

    private List<EggCollection> collection = new List<EggCollection> { 
        new EggCollection("Белое", "", 1),
        new EggCollection("Коричневое", "", 3),
        new EggCollection("Серебрянное", "", 10),
        new EggCollection("Золотое", "", 50),
        new EggCollection("Алмазное", "", 100),
    };

    #endregion

    public void LoadCollection()
    {
        money = 0;
        collection[0].count = 0;
        collection[1].count = 0;
        collection[2].count = 0;
        collection[3].count = 0;
        collection[4].count = 0;
    }

    public void UpdateMoney(int value)
    {
        money += value;
        // Обновление UI
    }

    public void UpdateCollection(string name, int value = 1)
    {
        Debug.Log("Add " + name);

        foreach(var item in collection)
        {
            if(item.name== name) { item.count += value; break; }
        }
        // Update UI
        gameUI.UpdateEggText();
    }

    public List<int> GetEggsCounts()
    {
        var list = new List<int>();

        foreach(var item in collection)
        {
            list.Add(item.count);
        }

        return list;
    }
}

// Базовый класс яйца
public class Egg
{
    public string name;
    private int price;

    public Egg(string n, int p)
    {
        name = n; 
        price = p;
    }

    public int GetPrice { 
        get { return price; }
        set { }
    }
}

// Класс редкого яйца
public class EggCollection : Egg
{
    public int count;
    public string description;

    public EggCollection(string n, string d, int p) : base(n, p)
    {
        description = d;
        count = 0;
    }
}
