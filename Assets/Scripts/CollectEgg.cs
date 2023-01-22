using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum EggRare
{
    Common,
    Rare,
    Silver,
    Gold,
    Diamond,
    None
}

public class CollectEgg : MonoBehaviour
{
    #region Field

    public EggAsset Data;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image image;

    #endregion

    public void InitEgg(Sprite srt) 
    {
        if(Data.count == 0) 
        { 
            nameText.text = "???";
            image.sprite = srt;
        }
        else
        {
            UnlockEgg();
        }

    }

    public void UnlockEgg()
    {
        nameText.text = Data.eggName;
        image.sprite = Data.icon;
    }

}

[System.Serializable]
public class CollectEggInfo
{
    public CollectEgg egg;
    public float weight;
}
