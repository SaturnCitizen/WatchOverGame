using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : Bolt.EntityBehaviour<IKingState>
{
    public Rigidbody enemy;
    public GameObject Town;
    public float MaxDist = 5.0f;
    public float MinDist = 3.0f;

    public override void Attached()
    {
        state.OnSpawn = Spawn;
    }

    private void Spawn()
    {
        var spawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        Rigidbody enemyClone = Instantiate(enemy, spawnPosition, this.transform.rotation);
        var enemySpeed = 4.0f;

        enemyClone.transform.LookAt(Town.transform);

        if (Vector3.Distance(enemyClone.transform.position, Town.transform.position) >= MinDist)
        {

            enemyClone.transform.position += enemyClone.transform.forward * enemySpeed * BoltNetwork.FrameDeltaTime;

            if (Vector3.Distance(enemyClone.transform.position, Town.transform.position) <= MaxDist)
            {
                enemySpeed = 0.0f;
            }

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && entity.IsOwner)
        {
            state.Spawn();
        }
    }
}
