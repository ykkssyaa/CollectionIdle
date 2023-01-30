using System.Collections;
using System.Collections.Generic;
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

    #region Unity Methods

    private void Start()
    {
        // Location settings
        InitAllLocations();
        ChangeLocation(0);

        playerCollector.LoadCollection();

        gameUI.InitUI();

    }

    #endregion

    #region Methods

    public void MakeTic()
    {
        //Debug.Log("Make Tic in GameManager");

        if (activeLocation == null) { return;}

        activeLocation.MakeTic();
    }

    public void ChangeLocation(int index)
    {
        if (!locations[index].isActive) return;

        activeLocationIndex = index;
        activeLocation = locations[index];
        activeLocation.isActive = true;

        gameUI.ChangeLocationName(activeLocation.locName);
        gameUI.UpdateShop();
    }

    private void InitAllLocations()
    {
        foreach(var location in locations)
        {
            if(location.isActive) location.InitLocation();
        }
    }

    public Location GetActiveLocation() { return activeLocation; }

    #endregion

}
