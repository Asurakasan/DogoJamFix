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
     private bool isspawn;
    public List<GameObject>  ArenaWall;
    [HideInInspector] public int index;
    public GameObject[] ennemyObject;
    

    private void Awake()
    {
        instance = this;
    }

   

    private void Start()
    {
        
    }

    void Update()
    {
        if(isEnter == true )
        {
         

                StartCoroutine(waveManagers[index].WaveSpawn());
                ArenaWall[index].SetActive(true);
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
