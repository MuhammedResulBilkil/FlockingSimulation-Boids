using Unity.Entities;
using Unity.Mathematics;


public partial class Boid3DSpawnerSystem : SystemBase
{
    private RefRW<RandomComponent> _randomComponent;
    
    private readonly int _spawnAmount = 10000;
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();

        EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(World.Unmanaged);
        
        BoidSpawnerComponent boidSpawnerComponent = SystemAPI.GetSingleton<BoidSpawnerComponent>();
        _randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

        for (int i = 0; i < _spawnAmount; i++)
        {
            Entity spawnedEntity = entityCommandBuffer.Instantiate(boidSpawnerComponent.boidPrefab);
        }
        
        /*Entities.ForEach((RefRW<LocalTransform> localTransform) =>
        {
            localTransform.ValueRW.Position = randomComponent.ValueRW.random.NextFloat3(-100f, 100f);
        }).Run();*/
    }

    protected override void OnUpdate()
    {
        _randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        
        foreach (var moveToPositionAspect in SystemAPI.Query<MoveToPositionAspect>())
        {
            moveToPositionAspect.Move(SystemAPI.Time.DeltaTime, _randomComponent, new float3(5f,5f,5f));
        }
    }
}