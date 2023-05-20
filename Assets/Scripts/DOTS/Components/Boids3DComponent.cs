using Unity.Entities;
using Unity.Mathematics;

public struct Boids3DComponent : IComponentData
{
    public float3 velocity;
    public float3 acceleration;
}