using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public readonly partial struct BulletAspect : IAspect
{
    private readonly Entity Entity;

    private readonly TransformAspect transformAspect;
    private readonly RefRO<BulletProperties> bulletProperties;
    private readonly RefRW<BulletTime> lifeTime;
    private readonly RefRO<BulletTag> bulletTag;

    public float Damage => bulletProperties.ValueRO.Damage;
    public float Speed => bulletProperties.ValueRO.Speed;

    public float Timer
    {
        get => lifeTime.ValueRO.LifeTimer;
        set => lifeTime.ValueRW.LifeTimer = value;
    }

    public void TakeDamage(float damage)
    {
        // ��player���damage�˺�
    }

    public bool IsShootPlayer()
    {
        // �ӵ�.positon - player.positong < 1f
        // timer = 0 �ӵ���������Ϊ0ʱ �����ӵ�
        // return true��
        return false;
    }
}
