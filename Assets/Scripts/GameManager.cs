using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int p_money;
    [SerializeField]
    private int p_moneyPerSecond = 0;

    private void Start()
    {
        p_money = 0;
        StartCoroutine(AddMoneyPerSecond());
    }

    IEnumerator AddMoneyPerSecond()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            UpdateMoney(p_moneyPerSecond);
        }
    }

    public void OnClick()
    {
        UpdateMoney(1);
    }

    private void UpdateMoney(int value)
    {
        p_money+= value;
        // Update UI
    } 
}
