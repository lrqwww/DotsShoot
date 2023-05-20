using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
[UpdateAfter(typeof(EnemyUpdateSystem))]
public partial struct EnemyWalkSystem : ISystem
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
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        var playerPos = SystemAPI.GetComponent<LocalToWorldTransform>(playerEntity).Value.Position;

        new EnemyWalkJob
        {
            deltaTime = deltaTime,
            ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            playerPos = playerPos,
            shootDis = 30f
        }.ScheduleParallel();
    }
}

[BurstCompile]
public partial struct EnemyWalkJob: IJobEntity
{
    public float deltaTime;
    public EntityCommandBuffer.ParallelWriter ECB;
    public float3 playerPos;
    public float shootDis;

    [BurstCompile]
    private void Execute(EnemyWalkAspect enemyWalk, [EntityInQueryIndex] int sortKey)
    {   
        if (enemyWalk.IsInStopRange(playerPos, shootDis))
        {
            var enemyHeading = MathHeading.GetHeading(enemyWalk.transformAspect.Position, playerPos);
            ECB.SetComponent(sortKey, enemyWalk.Entity, new EnemyHeading { Value = enemyHeading });

            ECB.SetComponentEnabled<EnemyProperties>(sortKey, enemyWalk.Entity, false);
            ECB.SetComponentEnabled<EnemyShootProperties>(sortKey, enemyWalk.Entity, true);
        }
        enemyWalk.Walk(deltaTime);
    }
}
