using UnityEngine;
using System.Collections;

public class CubeBehaviour : Bolt.EntityBehaviour<IKingState>
{
    public override void Attached()
    {
        state.SetTransforms(state.CubeTransform, transform);

        if (entity.IsOwner)
        {
            state.CubeColor = new Color(Random.value, Random.value, Random.value);
        }

        state.AddCallback("CubeColor", ColorChanged);
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

    public override void SimulateOwner()
    {
        var speed = 8f;
        var movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) { movement.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movement.z -= 1; }
        if (Input.GetKey(KeyCode.A)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { movement.x += 1; }

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }
    }

    void ColorChanged()
    {
        GetComponent<Renderer>().material.color = state.CubeColor;
    }
}
