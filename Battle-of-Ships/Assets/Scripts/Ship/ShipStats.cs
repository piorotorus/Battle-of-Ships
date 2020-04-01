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
   [SerializeField] private GameObject deathParticle;
   [SerializeField] private GameObject gameOverMenu;
   private AudioSource audioSource;
   private const float DIE_SPEED_MULTIPLIER = 0.35f;
   private const string HIT_SOUND_NAME = "HitSFX";
   private const float HIT_AUDIO_DELAY_TIME = 0.5f;
   private Quaternion shipDeadRotation;
   private float maxHealth;
    public float currentHealth = 100;
    private float reloadTimeLeft = 5;
    private float reloadTimeRight = 5;
    private bool isDead;
    private bool canPlayHitSound=true;

    enum healthState
    {
        healthAboveHalf,
        healthUnderHalf,
        healthUnderQuater
    }

    private healthState shipHealthState = healthState.healthAboveHalf;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Can't find audiosource component",audioSource);
        }
        if (shipController == null)
        {
            Debug.LogError("ShipController script don't added", shipController);
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

        if (deathParticle == null)
        {
            Debug.LogError("DeathParticle don't added",deathParticle);
        }

        if (gameOverMenu == null)
        {
            Debug.LogError("End screen don't added",gameOverMenu);
        }
    }

    void Start()
    {  shipDeadRotation = transform.rotation;
        shipDeadRotation*= Quaternion.Euler(0, 0, 90);
        maxHealth = currentHealth;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
            CheckHealth();
            CheckCanPlayHitSound();
    }

    void CheckCanPlayHitSound()
    {
        if (canPlayHitSound)
        {
           PlayHitSound();
           StartCoroutine(HitSoundDelay());
        }
    }

    IEnumerator HitSoundDelay()
    {
        canPlayHitSound = false;
        yield return new WaitForSeconds(HIT_AUDIO_DELAY_TIME);
        canPlayHitSound = true;
    }

    void PlayHitSound()
    {
        AudioManager.audioManagerInstance.PlaySound(HIT_SOUND_NAME,audioSource);
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
        ChangeCanShooting();
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
        GameObject deathParticleInstance = Instantiate(deathParticle,deathParticle.transform.position, deathParticle.transform.rotation);
        deathParticleInstance.SetActive(true);
        GameOverUI.gameEnded = true;
        Destroy(gameObject);
    }

   public void ChangeCanShooting()
    {
        foreach (var script in shotCannonBall)
        {
            script.canShot = !script.canShot;
        } 
    }
}
