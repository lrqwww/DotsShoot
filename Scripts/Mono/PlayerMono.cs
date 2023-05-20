using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class PlayerMono : MonoBehaviour
{
    public float MaxHealth;
    public float PlayerSpeed;
}

public class PlayerBaker : Baker<PlayerMono>
{
    public override void Bake(PlayerMono authoring)
    {
        AddComponent(new PlayerProperties
        {
            MaxHealth = authoring.MaxHealth,
            curHeamlth = authoring.MaxHealth
        });
        AddComponent(new PlayerWalkProperties {
            PlayerSpeed = authoring.PlayerSpeed
        });
        AddComponent<PlayerTag>();
    }
}
