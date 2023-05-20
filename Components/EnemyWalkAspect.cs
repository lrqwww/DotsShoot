using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public readonly partial struct EnemyWalkAspect : IAspect
{
    public readonly Entity Entity;

    public readonly TransformAspect transformAspect;
    private readonly RefRO<EnemyProperties> walkProperties;
    private readonly RefRW<EnemyTimer> walkTimer;
    private readonly RefRW<EnemyHeading> heading;

    private float WalkSpeed => walkProperties.ValueRO.WalkSpeed;
    public float Heading => heading.ValueRO.Value;

    private float WalkTimer
    {
        get => walkTimer.ValueRO.Timer;
        set => walkTimer.ValueRW.Timer = value;
    }
    public void Walk(float deltaTime)
    {
        WalkTimer += deltaTime;
        transformAspect.LocalPosition += transformAspect.Forward * WalkSpeed * deltaTime;
    }

    public bool IsInStopRange(float3 playerPosition, float shootRange)
    {
        return math.distancesq(playerPosition, transformAspect.LocalPosition) <= shootRange;
    }
}
