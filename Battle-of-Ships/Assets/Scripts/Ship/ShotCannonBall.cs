using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotCannonBall : MonoBehaviour
{
    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] private KeyCode sideToShotKey;
    [SerializeField] private List<GameObject> cannonBallSpawners;
    [SerializeField] private List<GameObject> shootParticlesSpawners;
    [SerializeField] private GameObject shootParticle;
    public bool canShot;
    private const float SPEED_MULTIPLE = 35;
    private const float TIME_AFTER_BALL_DESTROY = 3;
    private const float RELOAD_TIME = 5;
    private const float TIME_AFTER_OFF_SHOT_PARTICLE = 4.9f;

   
    void Update()
    {
        if (Input.GetKeyDown(sideToShotKey)&&canShot)
        {
            ActivateSpawners();
            StartCoroutine(StartReloading());
        }
    }

    IEnumerator StartReloading()
    {
        canShot = false;
        yield return new WaitForSeconds(RELOAD_TIME);
        canShot = true;
    }

    void ActivateSpawners()
    {
       CreateShootParticle();
        foreach (var spawner in cannonBallSpawners)
        {
            StartCoroutine(CreateBall(spawner));
        }
    }

    void CreateShootParticle()
    {
        foreach (var spawner in shootParticlesSpawners)
        {
            GameObject particle = Instantiate(shootParticle, spawner.transform.position, spawner.transform.rotation);
        } 
    }
    
    IEnumerator CreateBall(GameObject spawner)
    {
       yield return new WaitForSeconds(Random.Range(0,0.2f));
       GameObject ball = Instantiate(cannonBallPrefab, spawner.transform.position, spawner.transform.rotation*GetRandomShot());
       AddForceToBall(ball);
       StartCoroutine(BallDestroy(ball));
    }
    IEnumerator BallDestroy(GameObject ball)
    {
        yield return new WaitForSeconds(TIME_AFTER_BALL_DESTROY);
        Destroy(ball);
    }

    Quaternion GetRandomShot()
    {
        return Quaternion.Euler(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-5,5));
    }
    
    void AddForceToBall(GameObject ball)
    {
        ball.GetComponent<Rigidbody>().AddForce(ball.transform.forward*SPEED_MULTIPLE,ForceMode.Impulse);
    }
}
