using Unity.Entities;
using UnityEngine;

public class Boid3DSpawnerAuthoring : MonoBehaviour
{
    public GameObject boidPrefab;
}

public class Boid3DSpawnerAuthoringBaker : Baker<Boid3DSpawnerAuthoring>
{
    public override void Bake(Boid3DSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity,
            new BoidSpawnerComponent
            {
                boidPrefab = GetEntity(authoring.boidPrefab, TransformUsageFlags.Dynamic)
            });
    }
}