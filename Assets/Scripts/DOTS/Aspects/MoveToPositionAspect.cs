using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct MoveToPositionAspect : IAspect
{
    private readonly Entity _entity;

    private readonly RefRW<LocalTransform> _localTransform;
    private readonly RefRW<Boids3DTagComponent> _boids3DTagComponent;

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