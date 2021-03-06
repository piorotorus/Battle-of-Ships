﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallStats : MonoBehaviour
{
    private const float DMG= 1;
    private const string SHIP_TAG = "Ship";
    private const string SEA_TAG = "Sea";
    private const string ENVIRONMENT_TAG = "Environment";
    private const float EXPLOSION_TIME = 1.8f;
    private const float WATER_SPLASH_TIME = 1.0f;
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private GameObject waterSplashParticle;
    private Rigidbody rigidbody;
    private SphereCollider sphereCollider;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            Debug.LogError("Can't find Rigidbody component",rigidbody);
        }

        if (explosionParticle == null)
        {
            Debug.LogError("Explosion particle don't added",explosionParticle);
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        IsCollidingShip(other);
       IsCollidingSea(other);
       IsCollidingEnvironment(other);
    }

    void IsCollidingEnvironment(Collider other)
    {
        if (other.tag.Equals(ENVIRONMENT_TAG))
        {
            DisableCannonBall();
            PlayParticle(explosionParticle);
            StartCoroutine(DestroyCannonBall(EXPLOSION_TIME));
        }
    }
    
    void IsCollidingShip(Collider other)
    {
        if (other.tag.Equals(SHIP_TAG))
        {
            other.GetComponent<ShipStats>().TakeDamage(DMG);
            DisableCannonBall();
            PlayParticle(explosionParticle);
            StartCoroutine(DestroyCannonBall(EXPLOSION_TIME));
        }
    }

    void IsCollidingSea(Collider other)
    {
        if (other.tag.Equals(SEA_TAG))
        {
            DisableCannonBall();
            PlayParticle(waterSplashParticle);
            StartCoroutine(DestroyCannonBall(WATER_SPLASH_TIME));
        }
    }

    void DisableCannonBall()
    {
        rigidbody.isKinematic = true;
        meshRenderer.enabled = false;
        sphereCollider.enabled = false;
    }

    void PlayParticle(GameObject particleGameObject)
    {
        particleGameObject.SetActive(true);
    }

    IEnumerator DestroyCannonBall(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
