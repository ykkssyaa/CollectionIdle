using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int money;

    private void Start()
    {
        money = 0;
    }
}
