using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Bolt.EntityBehaviour<ICreepState>
{
    public GameObject Town;
    public float MinDist = 8.0f;
    public float enemySpeed = 1f;

    public override void Attached()
    {
        state.SetTransforms(state.SphereTransform, transform);
        transform.LookAt(Town.transform);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, Town.transform.position) >= MinDist)
        {
            transform.position += transform.forward * enemySpeed * BoltNetwork.FrameDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<WallSpawning>().loseMoney();
        }        
        if (other.gameObject.tag == "Wall")
        {
            other.GetComponent<WallBehaviour>().wallLoseHealth();
            BoltNetwork.Destroy(gameObject);
        }
        if (other.gameObject.tag == "Campfire")
        {
            other.GetComponent<CampfireBehaviour>().campfireLoseHealth();
            BoltNetwork.Destroy(gameObject);
        }
    }
}
