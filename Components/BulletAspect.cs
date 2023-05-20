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
        // 对player造成damage伤害
    }

    public bool IsShootPlayer()
    {
        // 子弹.positon - player.positong < 1f
        // timer = 0 子弹生命周期为0时 销毁子弹
        // return true，
        return false;
    }
}
