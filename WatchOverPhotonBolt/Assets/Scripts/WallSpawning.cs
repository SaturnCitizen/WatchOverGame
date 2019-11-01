using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawning : Bolt.EntityBehaviour<IKingState>
{
    public int localMoney = 0;

    public override void Attached()
    {
        state.OnSpawn = Spawn;
        state.AddCallback("Money", MoneyCallBack);
        if (entity.IsOwner)
        {
            state.Money = localMoney;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && entity.IsOwner)
        {
            if (localMoney >= 5)
            {
                state.Spawn();
                state.Money -= 4;
            } 
        }
    }

    void OnGUI()
    {
        if (entity.IsOwner)
        {
            GUI.color = state.CubeColor;
            GUILayout.Label("Watch Over Session");
            GUI.color = Color.white;
        }
    }

    private void Spawn()
    {
        if (transform.position.z < -5)
        {
            var spawnPosition = new Vector3(transform.position.x, 0.5f, transform.position.z - 2);
            GameObject wallClone = BoltNetwork.Instantiate(BoltPrefabs.wall_Y_axis, spawnPosition, transform.rotation);
        }
        else
        if (transform.position.z > 5)
        {
            var spawnPosition = new Vector3(transform.position.x, 0.5f, transform.position.z + 2);
            GameObject wallClone = BoltNetwork.Instantiate(BoltPrefabs.wall_Y_axis, spawnPosition, transform.rotation);
        }
        else
        if (transform.position.x < 0)
        {
            var spawnPosition = new Vector3(transform.position.x - 2, 0.5f, transform.position.z);
            GameObject wallClone = BoltNetwork.Instantiate(BoltPrefabs.wall_X_axis, spawnPosition, transform.rotation);
        }
        else
        {
            var spawnPosition = new Vector3(transform.position.x + 2, 0.5f, transform.position.z);
            GameObject wallClone = BoltNetwork.Instantiate(BoltPrefabs.wall_X_axis, spawnPosition, transform.rotation);
        }
    }

    private void MoneyCallBack()
    {
        localMoney = state.Money;
        if (localMoney < 0)
        {
            localMoney = 0;
        }
    }

    public void loseMoney()
    {
        state.Money -= 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            BoltNetwork.Destroy(other.gameObject);
            state.Money += 1;
        }
    }

}
