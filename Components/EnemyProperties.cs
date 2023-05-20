using Unity.Entities;

public struct EnemyProperties : IComponentData, IEnableableComponent
{
    public float WalkSpeed;
}

public struct EnemyHeading : IComponentData
{
    public float Value;
}

public struct EnemyShootProperties : IComponentData, IEnableableComponent
{
    public Entity BulletPre;
    public float ShootRate;
    public float ShootDistance;
}

public struct EnemyTimer: IComponentData
{
    public float Timer;
}

public struct EnemyTag : IComponentData { }