using Unity.Entities;

public struct PlayerProperties : IComponentData
{
    public float MaxHealth;
    public float curHeamlth;
}

public struct PlayerWalkProperties: IComponentData
{
    public float PlayerSpeed;
}

public struct PlayerTag : IComponentData { }