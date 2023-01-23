using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Egg", menuName = "Data/New Egg", order = 1)]
public class EggAsset : ScriptableObject
{
    public string ID = Guid.NewGuid().ToString().ToUpper();

    public string eggName;
    public int price;
    public string description;
    public EggRare rare;

    public Sprite icon;
}
