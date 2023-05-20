using Unity.Entities;

public struct BulletProperties : IComponentData
{
    public float Damage;
    public float Speed;
}

public struct BulletTime: IComponentData
{
    public float LifeTimer;
}

public struct BulletTag : IComponentData { }