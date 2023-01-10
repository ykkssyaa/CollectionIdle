using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    #region Serialized Fields

    public GameUI gameUI;

    [SerializeField] private PlayerCollector playerCollector;

    [SerializeField] private List<Location> locations;
    private Location activeLocation;
    private int activeLocationIndex;
    #endregion

    #region Fields

/*    [Space(30)]
    [Header("Fields")]*/

    #endregion

    #region Unity Methods

    private void Start()
    {
        activeLocationIndex = 0;
        activeLocation = locations[activeLocationIndex];
        activeLocation.isActive = true;

        playerCollector.LoadCollection();

        gameUI.InitUI();
    }

    #endregion

    #region Methods

    public void MakeTic()
    {
        Debug.Log("Make Tic in GameManager");

        if (activeLocation == null) { return;}

        activeLocation.MakeTic();
    }

    public Location GetActiveLocation() { return activeLocation; }

    #endregion

}
