using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Boids3DAuthoring : MonoBehaviour
{
    
}

public class Boids3DBaker : Baker<Boids3DAuthoring>
{
    public override void Bake(Boids3DAuthoring authoring)
    {
        Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
        AddComponent(entity, new Boids3DComponent
        {
            velocity = float3.zero,
            acceleration = float3.zero
        });
    }
}