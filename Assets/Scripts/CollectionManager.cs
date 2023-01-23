using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private Sprite LockedSprite;

    // ÷вета дл€ названи€ €йца в коллекции 
    [SerializeField] private RareColor[] Colors = new RareColor[6];
    // –одитель всех €иц в иерархии
    [SerializeField] private Transform content;

    public List<CollectEgg> AllRareEggs = new List<CollectEgg>();


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
}

public class RareColor
{
    public Color color;
    public EggRare rare;
}
