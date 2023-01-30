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

    public int count = 0;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image image;

    #endregion

    public void InitEgg() 
    {
        if(count == 0) 
        {
            return;
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
