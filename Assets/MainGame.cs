using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameObject Player;

    [SerializeField]
    private List<WaveManager> waveManagers;


    // private bool isup;

    public float countdown = 2f;

    private int waveNumber = 1;

    public static MainGame instance;

    public List<Collider2D> TriggerArene;
    public bool isEnter;
    public GameObject ArenaWall;

    

    private void Awake()
    {
        instance = this;
    }

   

    private void Start()
    {
        
    }

    void Update()
    {
        if(isEnter == true)
        {
            StartCoroutine(waveManagers[0].WaveSpawn());
            isEnter = false;
        }
        
        if(countdown >= 0 )
        {
            countdown -= Time.deltaTime;

        }

    }



    public void SpawnEnemy(GameObject enemyPrefab, Transform SpawnPoint)
    {

        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
    }
}
