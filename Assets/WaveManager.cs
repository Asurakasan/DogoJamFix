using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    public List<EnemyManager> ennemyManager;
    public EnemyManager currentEnemy;
     

  
    private MainGame mainGame;


    // Start is called before the first frame update
    void Start()
    {

        mainGame = MainGame.instance;
        
    }
    public IEnumerator WaveSpawn()
    {
        
        for (int h = 0; h < ennemyManager.Count; h++)
        {

            
                     currentEnemy = ennemyManager[h];
                    mainGame.countdown = currentEnemy.CoolDownBeforeSpawn;
                    yield return new WaitForSeconds(currentEnemy.CoolDownBeforeSpawn);
                    for (int j = 0; j < currentEnemy.EnemyCount; j++)
                    {

                        if (currentEnemy == null)
                        {
                            mainGame.isEnter = false;
                            mainGame.ArenaWall[mainGame.index].SetActive(false);
                        }
                        else
                        {
                        
                          mainGame.SpawnEnemy(currentEnemy.Enemy, currentEnemy.spawnPoint);
                          yield return new WaitForSeconds(0.5f);

                        }
                //currentEnemy.EnemyCount++;

                    }

            if (h == ennemyManager.Count -1 )
            {
                mainGame.empty = true;

            }

        }


    }
    // Update is called once per frame
    void Update()
    {

       // ennemyManager.Remove(currentEnemy);
       
    }
     
      

    }

