using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct SpawnEnemySystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GroundBaseProperties>();
    }
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var groundBaseEntity = SystemAPI.GetSingletonEntity<GroundBaseProperties>();
        var groundBase = SystemAPI.GetAspectRW<GroundBaseAspect>(groundBaseEntity);

        var ecb = new EntityCommandBuffer(Allocator.Temp);

        for (var i = 0; i < groundBase.EnemyNumbersToSpawn; i++)
        {
            var newEnemy = ecb.Instantiate(groundBase.EnemyPrefab);
            var newEnemyTrans = groundBase.GetRandomSpawn();

            ecb.SetComponent(newEnemy, new LocalToWorldTransform { Value = newEnemyTrans});
        }
        ecb.Playback(state.EntityManager);
    }
}