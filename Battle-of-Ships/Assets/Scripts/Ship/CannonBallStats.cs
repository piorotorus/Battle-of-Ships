using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallStats : MonoBehaviour
{
    private const float DMG= 1;
    private const string SHIP_TAG = "Ship";

    private void OnTriggerEnter(Collider other)
    {
       IsCollidingShip(other);
    }

    void IsCollidingShip(Collider other)
    {
        if (other.tag.Equals(SHIP_TAG))
        {
            other.GetComponent<ShipStats>().TakeDamage(DMG);
            
            Destroy(gameObject);
        }
    }
}
