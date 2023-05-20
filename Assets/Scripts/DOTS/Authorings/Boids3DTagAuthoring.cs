using Unity.Entities;
using UnityEngine;

public class Boids3DTagAuthoring : MonoBehaviour
{
}

public class Boids3DTagBaker : Baker<Boids3DTagAuthoring>
{
    public override void Bake(Boids3DTagAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Boids3DTagComponent());
    }
}