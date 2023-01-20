using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectEgg : MonoBehaviour
{
    public EggCollection egg;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image image;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;

    public void InitEgg() 
    {
        if(!egg.isEarned) 
        { 
            nameText.text = "???";
            image.sprite = lockedSprite;
        }
        else
        {
            UnlockEgg();
        }

    }

    public void UnlockEgg()
    {
        nameText.text = egg.name;
        image.sprite = unlockedSprite;
    }

    
}

[System.Serializable]
public class CollectEggInfo
{
    public CollectEgg egg;
    public float weight;
}
