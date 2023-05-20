using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;


public readonly partial struct GroundBaseAspect : IAspect
{
    public readonly Entity Entity;
    private readonly TransformAspect transformAspect;

    private readonly RefRO<GroundBaseProperties> groundBaseProperties;
    private readonly RefRW<GroundBaseRandom> groundBaseRandom;
    private readonly RefRW<EnemyUpdateTime> enemyUpdateTime;
    public int EnemyNumbersToSpawn => groundBaseProperties.ValueRO.EnemyNumbersToSpawn;
    public Entity EnemyPrefab => groundBaseProperties.ValueRO.EnemyPrefab;

#region 获取随机位置
    public UniformScaleTransform GetRandomSpawn()
    {
        return new UniformScaleTransform
        {
            Position = GetRandomPosition(),
            Rotation = GetRandomRotation(),
            Scale = 1.0f
        };
    }

    public  float3 GetRandomPosition() => groundBaseRandom.ValueRW.Value.NextFloat3(minSize, maxSize);

    private float3 minSize => transformAspect.Position - HalfDimensions;
    private float3 maxSize => transformAspect.Position + HalfDimensions;

    private float3 HalfDimensions => new()
    {
        x = groundBaseProperties.ValueRO.FieldDimensions.x * 0.5f,
        y = 0,
        z = groundBaseProperties.ValueRO.FieldDimensions.x * 0.5f
    };

    public quaternion GetRandomRotation() => quaternion.RotateY(groundBaseRandom.ValueRW.Value.NextFloat(-1f, 1f));
#endregion

#region 更新敌人
    public float EnemyUpdateTime
    {
        get => enemyUpdateTime.ValueRO.Value;
        set => enemyUpdateTime.ValueRW.Value = value;
    }

    public bool UpdateEnemy => EnemyUpdateTime <= 0f;

    public float UpdateRate => groundBaseProperties.ValueRO.UpdateRate;
#endregion
}
