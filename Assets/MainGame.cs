using System;
using System.Collections;
using System.Collections.Generic;
    using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameObject Player;
    public CameraManager cameraManager;

    [SerializeField]
    private List<WaveManager> waveManagers;

    

    [HideInInspector] public bool isfollow;

    // private bool isup;

    public float countdown = 2f;

    private int waveNumber = 1;

    public static MainGame instance;

    public List<Collider2D> TriggerArene;
    public bool isEnter;
     private bool isspawn;
    public List<GameObject>  ArenaWall;
    public int index;
   
    public List<GameObject> ennemylist = new List<GameObject>();

    public bool empty = false;
    private void Awake()
    {
        instance = this;
    }

   

    private void Start()
    {
        cameraManager = CameraManager.instance;
    }

    void Update()
    {
      
        if (isEnter == true )
        {
            StartCoroutine(waveManagers[index].WaveSpawn());

            empty = false;
                ArenaWall[index].SetActive(true);
                isEnter = false;
        }

        if(countdown >= 0 )
        {
            countdown -= Time.deltaTime;

        }

    }

    public void Canexplore()
    {
        if( ennemylist.Count == 0)
        {
            if(ArenaWall.Count > index && empty == true)
            {
                cameraManager.cameraIsfollow = true;
                ArenaWall[index].SetActive(false);
                waveManagers.Remove(waveManagers[index]);
                ArenaWall.Remove(ArenaWall[index]);
                index++;
            }

        }
        
    }


    public void SpawnEnemy(GameObject enemyPrefab, Transform SpawnPoint)
    {

       GameObject Newennemy = GameObject.Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation) as GameObject;
        ennemylist.Add(Newennemy);
    }
}
