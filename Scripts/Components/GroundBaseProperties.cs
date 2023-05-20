using Unity.Entities;
using Unity.Mathematics;

public struct GroundBaseProperties : IComponentData
{
    public float2 FieldDimensions;
    public int EnemyNumbersToSpawn;
    public Entity EnemyPrefab;

    public float UpdateRate;
}

public struct EnemyUpdateTime: IComponentData
{
    public float Value;
}
