using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public readonly partial struct EnemyShootAspect : IAspect
{
    public readonly Entity Entity;

    public readonly TransformAspect transformAspect;
    private readonly RefRO<EnemyShootProperties> enemyShootProperties;
    private readonly RefRW<EnemyTimer> enemyShootTime;
    private readonly RefRO<EnemyHeading> heading;

    private float ShootRate => enemyShootProperties.ValueRO.ShootRate;
    public float ShootDistance => enemyShootProperties.ValueRO.ShootDistance;
    public Entity Bullet => enemyShootProperties.ValueRO.BulletPre;

    private float EnemyShootTime
    {
        get => enemyShootTime.ValueRO.Timer;
        set => enemyShootTime.ValueRW.Timer = value;
    }

    private float Heading => heading.ValueRO.Value;

    public void Shoot(float deltaTime, Entity target)
    {

    }

    public bool IsInShootRange(float3 playerPosition, float shootRange)
    {
        return math.distancesq(playerPosition, transformAspect.Position) <= shootRange;
    }
}
