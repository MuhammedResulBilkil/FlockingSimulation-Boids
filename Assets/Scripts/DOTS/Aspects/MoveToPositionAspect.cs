using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct MoveToPositionAspect : IAspect
{
    private readonly Entity _entity;

    private readonly RefRW<LocalTransform> _localTransform;
    private readonly RefRW<Boids3DComponent> _boids3DComponent;

    public void Edges(BoidsBoxSizeComponent boidsBoxSizeComponent)
    {
        if(_localTransform.ValueRO.Position.x > boidsBoxSizeComponent.boxSize.x)
            _localTransform.ValueRW.Position = new float3(-boidsBoxSizeComponent.boxSize.x, _localTransform.ValueRO.Position.y, _localTransform.ValueRO.Position.z);
        else if(_localTransform.ValueRO.Position.x < -boidsBoxSizeComponent.boxSize.x)
            _localTransform.ValueRW.Position = new float3(boidsBoxSizeComponent.boxSize.x, _localTransform.ValueRO.Position.y, _localTransform.ValueRO.Position.z);
        
        if(_localTransform.ValueRO.Position.y > boidsBoxSizeComponent.boxSize.y) 
            _localTransform.ValueRW.Position = new float3(_localTransform.ValueRO.Position.x, -boidsBoxSizeComponent.boxSize.y, _localTransform.ValueRO.Position.z);
        else if(_localTransform.ValueRO.Position.y < -boidsBoxSizeComponent.boxSize.y)
            _localTransform.ValueRW.Position = new float3(_localTransform.ValueRO.Position.x, boidsBoxSizeComponent.boxSize.y, _localTransform.ValueRO.Position.z);
        
        if(_localTransform.ValueRO.Position.z > boidsBoxSizeComponent.boxSize.z) 
            _localTransform.ValueRW.Position = new float3(_localTransform.ValueRO.Position.x, _localTransform.ValueRO.Position.y, -boidsBoxSizeComponent.boxSize.z);
        else if(_localTransform.ValueRO.Position.z < -boidsBoxSizeComponent.boxSize.z)
            _localTransform.ValueRW.Position = new float3(_localTransform.ValueRO.Position.x, _localTransform.ValueRO.Position.y, boidsBoxSizeComponent.boxSize.z);
    }
    
    public void Move(float deltaTime, RefRW<RandomComponent> randomComponent, float3 boxSize)
    {
        //float3 direction = math.normalize(_targetPosition.ValueRO.targetPosition - _localTransform.ValueRO.Position);

        _localTransform.ValueRW.Position = GetRandomPosition(randomComponent, boxSize);
    }

    /*public void TestReachedTargetPosition(RefRW<RandomComponent> randomComponent)
    {
        float reachedTargetDistance = 1f;
        if (math.distancesq(_localTransform.ValueRO.Position, _targetPosition.ValueRO.targetPosition) <=
            reachedTargetDistance)
        {
            //Debug.LogFormat($"Distance = {math.distance(_localTransform.ValueRO.Position, _targetPosition.ValueRO.targetPosition)}");
            //Debug.LogFormat($"Distance Squared= {math.distancesq(_localTransform.ValueRO.Position, _targetPosition.ValueRO.targetPosition)}");

            _targetPosition.ValueRW.targetPosition = GetRandomPosition(randomComponent);

            //Debug.LogFormat($"Random Target Pos = " + _targetPosition.ValueRO.targetPosition);
        }
    }*/

    private float3 GetRandomPosition(RefRW<RandomComponent> randomComponent, float3 boxSize)
    {
        return new float3(
            randomComponent.ValueRW.random.NextFloat(-boxSize.x, boxSize.x),
            randomComponent.ValueRW.random.NextFloat(-boxSize.y, boxSize.y),
            randomComponent.ValueRW.random.NextFloat(-boxSize.z, boxSize.z));
    }
}