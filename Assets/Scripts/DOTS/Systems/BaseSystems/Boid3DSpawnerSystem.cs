using Unity.Entities;


public partial class Boid3DSpawnerSystem : SystemBase
{
    private readonly int _spawnAmount = 10000;
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();

        EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(World.Unmanaged);
        
        BoidSpawnerComponent boidSpawnerComponent = SystemAPI.GetSingleton<BoidSpawnerComponent>();
        
        for (int i = 0; i < _spawnAmount; i++)
        {
            Entity spawnedEntity = entityCommandBuffer.Instantiate(boidSpawnerComponent.boidPrefab);
        }
    }

    protected override void OnUpdate()
    {
        
    }
}