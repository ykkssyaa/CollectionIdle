using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    
    [SerializeField] private PlayerCollector collector;

    private List<EggInform> eggs = new List<EggInform> {
        new EggInform("�����", "", 1, 0.85f),
        new EggInform("����������", "", 3, 0.08f),
        new EggInform("�����������", "", 10, 0.05f),
        new EggInform("�������", "", 50, 0.018f),
        new EggInform("��������", "", 100, 0.002f),
    };

    #region Fields
    [Space(30)]
    [Header("Fields")]

    // ������� ������ ����� ������ ���������� �� ���� ���
    [SerializeField] private int clicksPerTic = 0;
    [SerializeField] private int clicksPerClick = 1;
    // ��� � ������� ������ ����� �������� ����������
    [SerializeField] private int secondsInTic = 1;

    // ���������� ������ ��� ��������� ������ ����
    public int clicksToEarn = 1;
    // ������� ���������� ������������� ������
    public int curClicks = 0;

    public string locName;
    public bool isActive = false;
    #endregion

    public IEnumerator AutoClicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsInTic);
            MakeTic();
        }
    }

    // ������� ���� 
    public void MakeTic()
    {
        Debug.Log("Make Tic in Location");

        curClicks += clicksPerClick;

        
        if(curClicks >= clicksToEarn)
        {
            if (clicksToEarn == 0)
                return;

            int c = curClicks / clicksToEarn;

            for(int i = 0; i < c; i++)
            {
                EarnRandomEgg();
            }

            curClicks = curClicks % clicksToEarn;
        }

        collector.gameUI.UpdateClicksToReward();
    }

    private void EarnRandomEgg()
    {
        Debug.Log("Earn New Egg");

        float val = Random.value;

        foreach(var item in eggs)
        {
            if(item.weight <= val)
            {
                collector.UpdateCollection(item.name);
                break;
            }
        }
    }

}

public class EggMiner
{

}

public class EggInform : Egg
{
    public float weight;

    public EggInform(string n, string d, int p, float weight) : base(n, d, p)
    {
        this.weight = weight;
    }
}

