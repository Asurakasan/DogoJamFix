using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyManager> ennemyManager;
    private EnemyManager currentEnemy;

    private MainGame mainGame;

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

                mainGame.SpawnEnemy(currentEnemy.Enemy, currentEnemy.spawnPoint);
                yield return new WaitForSeconds(0.5f);
            }

            currentEnemy.EnemyCount++;
            h++;



        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
