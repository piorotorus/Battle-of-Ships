using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ShipStats : MonoBehaviour
{
   [SerializeField] private ShotCannonBall[] shotCannonBall;
   [SerializeField] private GameObject[] halfHealthParticle;
   [SerializeField] private GameObject[] quaterHealthParticle;
   [SerializeField] private ShipController shipController;
   private const float DIE_SPEED_MULTIPLIER = 0.35f;
   private Quaternion shipDeadRotation;
   private float maxHealth;
    public float currentHealth = 100;
    private float reloadTimeLeft = 5;
    private float reloadTimeRight = 5;
    private bool isDead;

    enum healthState
    {
        healthAboveHalf,
        healthUnderHalf,
        healthUnderQuater
    }

    private healthState shipHealthState = healthState.healthAboveHalf;

    private void Awake()
    {
        if (shipController == null)
        {
            Debug.LogError("ShipController script don't added",shipController);
        }
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

    /*
    private void Update()
    {
        
            transform.rotation=Quaternion.Lerp(transform.rotation, shipDeadRotation, Time.deltaTime);
        

        if (transform.rotation.z <= 0.4f)
            Debug.Log(transform.rotation.z );
    }
    */
    
 

    void Start()
    {  shipDeadRotation = transform.rotation;
        shipDeadRotation*= Quaternion.Euler(0, 0, 90);
        maxHealth = currentHealth;
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
        else if(currentHealth<=0&&!isDead)
        {
            isDead = true;
            IsDead();
            
        }
    }

    void IsDead()
    {
        shipDeadRotation = transform.rotation;
        shipDeadRotation*= Quaternion.Euler(0, 0, 90);
        TurnOffShooting();
        shipController.canMove = false;
        StartCoroutine(RotateShipToDiePosition());
    }

    IEnumerator RotateShipToDiePosition()
    {
        while (transform.rotation.z <= 0.4f)
        {
            transform.rotation=Quaternion.Lerp(transform.rotation, shipDeadRotation, Time.deltaTime*DIE_SPEED_MULTIPLIER);
            yield return null;

        }
        DestroyShip();
        yield break;
    }

    void DestroyShip()
    {
        Destroy(gameObject);
    }

    void TurnOffShooting()
    {
        foreach (var script in shotCannonBall)
        {
            script.canShot = false;
        } 
    }
}
