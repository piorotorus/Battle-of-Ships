using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ram : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private const string SHIP_TAG = "Ship";

    private void Awake()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody don't added to script",rb);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
     IsCollidingEnemy(other);
    }

    void IsCollidingEnemy(Collider other)
    {
        if (other.tag.Equals(SHIP_TAG))
        {
           int dmg= CalculateDmg();
            DealDmgToEnemy(other,dmg);
        }
    }

    void DealDmgToEnemy(Collider enemyShip,int dmg)
    {
        ShipStats enemyShipStats = enemyShip.gameObject.GetComponent<ShipStats>();
        enemyShipStats.TakeDamage(dmg);
    }
    
    int CalculateDmg()
    {
        float dmg = rb.velocity.magnitude * 6;
        return (int)dmg;
    }
}
