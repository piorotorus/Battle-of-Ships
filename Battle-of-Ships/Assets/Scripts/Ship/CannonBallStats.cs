using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallStats : MonoBehaviour
{
    private const float DMG= 1;
    private const string SHIP_TAG = "Ship";
    private const float EXPLOSION_TIME = 1.8f;
    [SerializeField] private GameObject explosionParticle;
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
    }

    void IsCollidingShip(Collider other)
    {
        if (other.tag.Equals(SHIP_TAG))
        {
            other.GetComponent<ShipStats>().TakeDamage(DMG);
            
            DisableCannonBall();
            PlayParticle();
            StartCoroutine(DestroyCannonBall());
        }
    }

    void DisableCannonBall()
    {
        rigidbody.isKinematic = true;
        meshRenderer.enabled = false;
        sphereCollider.enabled = false;
    }

    void PlayParticle()
    {
        explosionParticle.SetActive(true);
    }

    IEnumerator DestroyCannonBall()
    {
        yield return new WaitForSeconds(EXPLOSION_TIME);
        Destroy(gameObject);
    }
}
