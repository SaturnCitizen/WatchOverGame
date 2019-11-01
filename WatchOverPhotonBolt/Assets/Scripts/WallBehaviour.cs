using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : Bolt.EntityBehaviour<IWallState>
{
    public int localWallHealth = 2;

    public override void Attached()
    {
        state.WallHealth = localWallHealth;

        state.AddCallback("WallHealth", HealthCallBack);
    }

    private void HealthCallBack()
    {
        localWallHealth = state.WallHealth;
        if (localWallHealth <= 0)
        {
            BoltNetwork.Destroy(gameObject);
        }
    }

    public void wallLoseHealth()
    {
        state.WallHealth -= 1;
    }
}
