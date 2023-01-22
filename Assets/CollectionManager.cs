using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private Sprite LockedSprite;

    // ����� ��� �������� ���� � ��������� 
    [SerializeField] private RareColor[] Colors = new RareColor[6];

}

public class RareColor
{
    public Color color;
    public EggRare rare;
}
