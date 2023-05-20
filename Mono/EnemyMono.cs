using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class EnemyMono : MonoBehaviour
{
    public float WalkSpeed;
    public float ShootRate;
    public float ShootDistance;
    public GameObject BulletPre;
}

public class EnemyBaker : Baker<EnemyMono>
{
    public override void Bake(EnemyMono authoring)
    {
        AddComponent(new EnemyProperties
        {
            WalkSpeed = authoring.WalkSpeed
        });
        AddComponent(new EnemyShootProperties
        {
            BulletPre = GetEntity(authoring.BulletPre),
            ShootDistance = authoring.ShootDistance,
            ShootRate = authoring.ShootRate
        });
        AddComponent<EnemyHeading>();
        AddComponent<EnemyTimer>();
        AddComponent<EnemyTag>();
    }
}
