using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;

[BurstCompile]
public partial struct EnemyUpdateSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemyUpdateTime>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

        new EnemyUpdateJob
        {
            DeltaTime = deltaTime,
            ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
        }.Run();
    }
}

[BurstCompile]
public partial struct EnemyUpdateJob: IJobEntity
{
    public float DeltaTime;
    public EntityCommandBuffer ECB;

    [BurstCompile]
    private void Execute(GroundBaseAspect groundBaseAspect)
    {
        groundBaseAspect.EnemyUpdateTime -= DeltaTime;

        if (!groundBaseAspect.UpdateEnemy) return;

        groundBaseAspect.EnemyUpdateTime = groundBaseAspect.UpdateRate;
        var newEnemy = ECB.Instantiate(groundBaseAspect.EnemyPrefab);
        var newEnemyTrans = groundBaseAspect.GetRandomSpawn();
        ECB.SetComponent(newEnemy, new LocalToWorldTransform { Value = newEnemyTrans });     
    }
}