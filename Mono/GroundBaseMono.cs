using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class GroundBaseMono : MonoBehaviour
{
    public float2 FieldDimensions;
    public int EnemyNumbersToSpawn;
    public GameObject EnemyPrefab;

    public float UpdateRate;

    public uint RandomSeed;
}

public class GroundBaseBaker : Baker<GroundBaseMono>
{
    public override void Bake(GroundBaseMono authoring)
    {
        AddComponent(new GroundBaseProperties
        {
            FieldDimensions = authoring.FieldDimensions,
            EnemyNumbersToSpawn = authoring.EnemyNumbersToSpawn,
            EnemyPrefab = GetEntity(authoring.EnemyPrefab),
            UpdateRate = authoring.UpdateRate
        });
        AddComponent<EnemyUpdateTime>();
        AddComponent(new GroundBaseRandom
        {
            Value = Unity.Mathematics.Random.CreateFromIndex(authoring.RandomSeed)
        });
    }
}
