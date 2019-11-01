using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSpawner : Bolt.GlobalEventListener
{
    float enemyTimeLeft = 15.0f;
    float coinTimeLeft = 5.0f;
    private int randPosX = 0;
    private int randPosY = 0;
    private int difficultyTimer = 0;

    private void Start()
    {
        var spawnPosition = new Vector3(0, 0.5f, 0);
        BoltNetwork.Instantiate(BoltPrefabs.Campfire, spawnPosition, Quaternion.identity);
    }

    void Update()
    {
        enemyTimeLeft -= BoltNetwork.FrameDeltaTime;
        coinTimeLeft -= BoltNetwork.FrameDeltaTime;
        if (enemyTimeLeft < 0)
        {

            while (Mathf.Abs(randPosX) < 15 && Mathf.Abs(randPosY) < 15)
            {
                randPosX = Random.Range(-25, 25);
                randPosY = Random.Range(-25, 25);
            }
            var enemySpawnPosition = new Vector3(randPosX, 0.5f, randPosY);
            BoltNetwork.Instantiate(BoltPrefabs.Enemy, enemySpawnPosition, Quaternion.identity);
            if (difficultyTimer < 10)
            {
                enemyTimeLeft = 5.0f;
                difficultyTimer +=1;
            }
            else
            {
                enemyTimeLeft = 3.0f;
            }
            randPosX = 0;
            randPosY = 0;
        }

        if (coinTimeLeft < 0)
        {
            var coinSpawnPosition = new Vector3(Random.Range(-10, 10), 0.25f, Random.Range(-10, 10));
            BoltNetwork.Instantiate(BoltPrefabs.Coin, coinSpawnPosition, Quaternion.identity);
            coinTimeLeft = 2.0f;
        }
    }
}
