using Unity.Entities;

public struct BoidSpawnerComponent : IComponentData
{
    public Entity boidEntityPrefab;
    public Entity boidPrefab;
}
