using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;

[BurstCompile]
public partial struct Boids3DISystem : ISystem
{
    private RefRW<RandomComponent> _randomComponent;
    private BoidsBoxSizeComponent _boxSizeComponent;
    private float _deltaTime;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BoidsBoxSizeComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        _randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        _boxSizeComponent = SystemAPI.GetSingleton<BoidsBoxSizeComponent>();
        
        _deltaTime = SystemAPI.Time.DeltaTime;
        
        JobHandle calculateEdgeJobHandle = new CalculateEdgeJob
        {
            boidsBoxSizeComponent = _boxSizeComponent
        }.ScheduleParallel(state.Dependency);
        
        calculateEdgeJobHandle.Complete();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }
}

[BurstCompile]
public partial struct CalculateEdgeJob : IJobEntity
{
    public BoidsBoxSizeComponent boidsBoxSizeComponent;

    public void Execute(MoveToPositionAspect moveToPositionAspect)
    {
        moveToPositionAspect.Edges(boidsBoxSizeComponent);
    }
}