
using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
[UpdateAfter(typeof(EnemyWalkSystem))]

public partial struct EnemyShootSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        var playerPos = SystemAPI.GetComponent<LocalToWorldTransform>(playerEntity).Value.Position;
        var shootDis = 30f;

        new EnemyShootJob
        {
            DeltaTime = deltaTime,
            ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            PlayerPos = playerPos,
            ShootDis = shootDis
        }.ScheduleParallel();
    }
}

[BurstCompile] 
public partial struct EnemyShootJob: IJobEntity
{
    public float DeltaTime;
    public EntityCommandBuffer.ParallelWriter ECB;
    public float3 PlayerPos;
    public float ShootDis;

    [BurstCompile]
    private void Execute(EnemyShootAspect enemyShootAspect, [EntityInQueryIndex] int sort)
    {
        if (enemyShootAspect.IsInShootRange(PlayerPos, enemyShootAspect.ShootDistance))
        {
            var bulletPre = ECB.Instantiate(sort, enemyShootAspect.Bullet);
            var bulletPos = new UniformScaleTransform { 
                Position = enemyShootAspect.transformAspect.Position,
                Rotation = quaternion.identity,
                Scale = 1f
            };
            ECB.SetComponent(sort, bulletPre, new LocalToWorldTransform { Value = bulletPos });
        }
    }
}