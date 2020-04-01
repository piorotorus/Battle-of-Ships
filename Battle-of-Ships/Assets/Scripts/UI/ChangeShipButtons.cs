using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShipButtons : MonoBehaviour
{
   [SerializeField] private GameObject smallShip;
   [SerializeField] private GameObject mediumShip;
   [SerializeField] private GameObject bigShip;
   private GameObject[] shipArray;

  public enum ShipSize
   {Small,
      Normal,
      Big
   }

  private void Awake()
  {
     if (smallShip == null)
     {
        Debug.LogError("Small ship don't added", smallShip);
     }
     if (mediumShip == null)
     {
        Debug.LogError("Medium ship don't added", mediumShip);
     }
     if (bigShip == null)
     {
        Debug.LogError("Big ship don't added", bigShip);
     }
  }

  private void Start()
   {
      shipArray= new GameObject[]{smallShip,mediumShip,bigShip};
   }

   public void OnShipButtonClick(int shipSize)
   {
      foreach (var ship in shipArray)
      {
         ship.SetActive(false);
      }
      if (shipSize == (int)ShipSize.Small)
      {
         smallShip.SetActive(true);
      }else if(shipSize==(int)ShipSize.Normal)
      {
         mediumShip.SetActive(true);
      }
      else
      {
         bigShip.SetActive(true);
      }
   }
}
