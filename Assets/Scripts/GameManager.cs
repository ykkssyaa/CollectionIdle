using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    #region Serialized Fields

    public GameUI gameUI;

    #endregion

    #region Fields

    [Space(30)]
    [Header("Fields")]
    [SerializeField]
    private int p_money;
    [SerializeField]
    private int p_moneyPerSecond = 0;

    #endregion

    #region Unity Methods
    private void Start()
    {
        p_money = 0;
        StartCoroutine(AddMoneyPerSecond());
    }
    #endregion

    #region Methods

    public void UpdateMoneyOnClick()
    {
        UpdateMoney(1);
    }

    private void UpdateMoney(int value)
    {
        p_money+= value;
        // Update UI
        gameUI.UpdateMoneyText(p_money.ToString());
    } 

    #endregion

    IEnumerator AddMoneyPerSecond()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            UpdateMoney(p_moneyPerSecond);
        }
    }

}
