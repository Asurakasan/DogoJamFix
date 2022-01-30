using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyManager> ennemyManager;
    private EnemyManager currentEnemy;
     
    bool isaded = false;
    private MainGame mainGame;
    public bool canadd = false;

    // Start is called before the first frame update
    void Start()
    {

        mainGame = MainGame.instance;
        
    }
    public IEnumerator WaveSpawn()
    {

        for (int h = 0; h < ennemyManager.Count;)
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
                            mainGame.ennemyObject = GameObject.FindGameObjectsWithTag("Ennemis");
                            isaded = true;
                            yield return new WaitForSeconds(0.5f);

                        }

               

                    //currentEnemy.EnemyCount++;
                    h++;


                }
        }


    }
    // Update is called once per frame
    void Update()
    {
            if (isaded == true)
            {
                foreach (var item in mainGame.ennemyObject)
                {
                    if (item == null)
                    {
                        mainGame.ArenaWall[mainGame.index].SetActive(false);
                        mainGame.TriggerArene[mainGame.index].GetComponent<Collider2D>().enabled = !mainGame.TriggerArene[mainGame.index].GetComponent<Collider2D>().enabled;
                        isaded = false;
                    }

                }



            }
        }
     
       

    }

