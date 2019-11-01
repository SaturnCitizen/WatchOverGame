using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireBehaviour : Bolt.EntityBehaviour<ICampfireState>
{
    public int localCampfireHealth = 5;
    float loseTimeLeft = 2.0f;

    public override void Attached()
    {
        state.CampfireHealth = localCampfireHealth;

        state.AddCallback("CampfireHealth", HealthCallBack);
    }

    private void HealthCallBack()
    {
        localCampfireHealth = state.CampfireHealth;
        if (localCampfireHealth <= 0)
        {
            BoltNetwork.Destroy(gameObject);
            if (loseTimeLeft < 0)
            {
                BoltNetwork.Shutdown();
            }

        }
    }

    public void campfireLoseHealth()
    {
        state.CampfireHealth -= 1;
    }

    private void Update()
    {
        loseTimeLeft -= BoltNetwork.FrameDeltaTime;
    }
}
