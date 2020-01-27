using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
   [SerializeField] private ShotCannonBall[] shotCannonBall;
    private float maxHealth = 100;
    public float currentHealth = 100;
    private float reloadTimeLeft = 5;
    private float reloadTimeRight = 5;
    
    void Start()
    {
        if (shotCannonBall == null)
        {
            Debug.LogError("ShotCannonBall scripts don't added");
        }
    }
    

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        
        if (CheckIsDead()) 
        {
            IsDead();
        }
    }

     bool CheckIsDead()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    void IsDead()
    {
        Destroy(gameObject);
        TurnOffShooting();
    }

    void TurnOffShooting()
    {
        foreach (var script in shotCannonBall)
        {
            script.canShot = false;
        } 
    }
}
