using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    #region Fields

    public int money;
    
     public GameUI gameUI;

    private List<EggCollection> collection = new List<EggCollection> { 
        new EggCollection("�����", "", 1),
        new EggCollection("����������", "", 3),
        new EggCollection("�����������", "", 10),
        new EggCollection("�������", "", 50),
        new EggCollection("��������", "", 100),
    };

    #endregion

    public void LoadCollection()
    {
        money = 0;
        collection[0].count = 10;
        collection[1].count = 50;
        collection[2].count = 100;
        collection[3].count = 300;
        collection[4].count = 500;
    }

    public void UpdateMoney(int value)
    {
        money += value;
        // ���������� UI
    }

    public void UpdateCollection(string name, int value = 1)
    {
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

public class Egg
{
    public string name;
    public string description;
    public int price;

    public Egg(string n, string d, int p)
    {
        name = n; 
        description = d;
        price = p;
    }
}

public class EggCollection : Egg
{
    public int count;

    public EggCollection(string n, string d, int p) : base(n, d, p)
    {
        count = 0;
    }
}
