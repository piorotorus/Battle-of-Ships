using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
   [SerializeField] private ShotCannonBall[] shotCannonBall;
   [SerializeField] private GameObject[] halfHealthParticle;
   [SerializeField] private GameObject[] quaterHealthParticle;
    private float maxHealth = 100;
    public float currentHealth = 100;
    private float reloadTimeLeft = 5;
    private float reloadTimeRight = 5;

    enum healthState
    {
        healthAboveHalf,
        healthUnderHalf,
        healthUnderQuater
    }

    private healthState shipHealthState = healthState.healthAboveHalf;
    
    void Start()
    {
        if (shotCannonBall == null)
        {
            Debug.LogError("ShotCannonBall scripts don't added");
        }
        if (halfHealthParticle == null)
        {
            Debug.LogError("HalfHealthParticle don't added");
        }

        if (quaterHealthParticle == null)
        {
            Debug.LogError("QuaterHealthParticle don't added");
        }
    }
    

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        CheckHealth();
    }

     void CheckHealth()
    {
        if (currentHealth<=maxHealth/2&&shipHealthState==healthState.healthAboveHalf)
        {
            foreach (var particle in halfHealthParticle)
            {
                particle.SetActive(true);
            }
            shipHealthState = healthState.healthUnderHalf;
        }
        else if (currentHealth <= maxHealth / 4 && shipHealthState == healthState.healthUnderHalf)
        {
            foreach (var particle in quaterHealthParticle)
            {
                particle.SetActive(true);
            }

            shipHealthState = healthState.healthUnderQuater;
        }
        else if(currentHealth<=0)
        {
            IsDead();
        }
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
