using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

[BurstCompile]
public partial struct Boids3DISystem : ISystem
{
    private RefRW<RandomComponent> _randomComponent;
    private BoidsBoxSizeComponent _boxSizeComponent;
    private QueryEnumerable<LocalTransform> _queryEnumerableLocalTransforms;
    private QueryEnumerable<Boids3DComponent> _queryEnumerableBoids3DComponents;
    private NativeArray<Entity> _entityQuery;
    
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
        
        _queryEnumerableLocalTransforms = SystemAPI.Query<LocalTransform>();
        _queryEnumerableBoids3DComponents = SystemAPI.Query<Boids3DComponent>();
        _entityQuery = SystemAPI.QueryBuilder().WithAspect<MoveToPositionAspect>().Build().ToEntityArray(Allocator.TempJob);

        _deltaTime = SystemAPI.Time.DeltaTime;
        
        JobHandle calculateEdgeJobHandle = new CalculateEdgeJob
        {
            boidsBoxSizeComponent = _boxSizeComponent
        }.ScheduleParallel(state.Dependency);
        
        calculateEdgeJobHandle.Complete();
        
        JobHandle calculateFlockJobHandle = new CalculateFlockJob
        {
            queryEnumerableLocalTransforms = _queryEnumerableLocalTransforms,
            queryEnumerableBoids3DComponents = _queryEnumerableBoids3DComponents
        }.ScheduleParallel(state.Dependency);
        
        calculateFlockJobHandle.Complete();
        
        
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

[BurstCompile]
public partial struct CalculateFlockJob : IJobEntity
{
    public QueryEnumerable<LocalTransform> queryEnumerableLocalTransforms;
    public QueryEnumerable<Boids3DComponent> queryEnumerableBoids3DComponents;

    public void Execute(MoveToPositionAspect moveToPositionAspect)
    {
        moveToPositionAspect.Flock(queryEnumerableLocalTransforms, queryEnumerableBoids3DComponents);
    }
}

