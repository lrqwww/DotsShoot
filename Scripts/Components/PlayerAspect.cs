using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public readonly partial struct PlayerAspect : IAspect
{
    public readonly Entity Entity;

    public readonly TransformAspect transformAspect;
    private readonly RefRW<PlayerProperties> playerHealth;
    private readonly RefRO<PlayerWalkProperties> walkSpeed;
    private readonly DynamicBuffer<PlayerDamageBufferElement> playerDamageBuffer;

    public float WalkSpeed => walkSpeed.ValueRO.PlayerSpeed;

    public void DamagePlayer()
    {
        foreach(var element in playerDamageBuffer)
        {
            // ��ǰѪ�� = ��ǰѪ�� - ÿ֡�ܵ����˺�������buffer���element��֮�ͣ��ӵ��˺���
        }
    }

    public void Move(float3 pos, float deltaTime)
    {
        var t = transformAspect.LocalToWorld;
        t.Position += pos * deltaTime * WalkSpeed;
        transformAspect.LocalToWorld = t;
    }
}
