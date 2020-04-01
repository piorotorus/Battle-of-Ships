using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private ShipStats[] shipStatsScripts;

    void Start()
    {TurnOnShooting();
        Time.timeScale = 0;
    }
    
    public void StartButtonClick()
    {
       TurnOnShooting();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    
    void TurnOnShooting()
    {
        foreach (var script in shipStatsScripts)
        {
            script.ChangeCanShooting();
        }
    }
}
