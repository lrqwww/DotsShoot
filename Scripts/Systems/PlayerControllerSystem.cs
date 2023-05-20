using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public partial class PlayerControllerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var input = new float3(horizontal, 0, vertical);
        var deltaTime = SystemAPI.Time.DeltaTime;

        foreach (PlayerAspect playerAspect in SystemAPI.Query<PlayerAspect>())
        {
            Debug.Log("’“µΩ");
            playerAspect.Move(input, deltaTime);
        }
    }
}