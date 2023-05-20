using UnityEngine;
using Unity.Entities;

public class BulletMono : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public float LifeTimer;
}

public class BulletBaker : Baker<BulletMono>
{
    public override void Bake(BulletMono authoring)
    {
        AddComponent(new BulletProperties 
        { 
            Damage = authoring.Damage,
            Speed = authoring.Speed
        });
        AddComponent(new BulletTime { LifeTimer = authoring.LifeTimer });
        AddComponent<BulletTag>();
    }
}