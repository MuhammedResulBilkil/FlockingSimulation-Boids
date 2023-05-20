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
    
    public void Flock(QueryEnumerable<LocalTransform> queryEnumerableLocalTransforms, QueryEnumerable<Boids3DComponent> queryEnumerableBoids3DComponents)
    {
        float3 alignment = Align(queryEnumerableLocalTransforms, queryEnumerableBoids3DComponents);
        //Vector3 cohesion = Cohesion();
        //Vector3 separation = Separation();
        
        alignment *= 0.1f;
        //cohesion *= GetPowerCohesion();
        //separation *= GetPowerSeparation();
        
        _boids3DComponent.ValueRW.acceleration += alignment;
        //_acceleration += cohesion;
        //_acceleration += separation;
    }
    
    private float3 Align(QueryEnumerable<LocalTransform> queryEnumerableLocalTransforms, QueryEnumerable<Boids3DComponent> queryEnumerableBoids3DComponents)
    {
        float3 steering = float3.zero;
        int total = 0;

        foreach (var localTransform in queryEnumerableLocalTransforms)
        {
            float distance = math.distance(_localTransform.ValueRO.Position, localTransform.Position);
            
            if (distance < 0.5f)
            {
                // TODO: Find a way to get the velocity of the other boid Entity!!!!!
                //steering += localTransform.GetVelocity();
                total++;
            }
        }
        
        if (total > 0)
        {
            steering /= total;
            steering = math.normalize(steering) * 0.16f;
            steering -= _boids3DComponent.ValueRO.velocity;
            steering = math.clamp(steering, -0.32f, 0.32f);
            //steering = Vector3.ClampMagnitude(steering, 0.32f);
        }

        return steering;
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
    
    public RefRW<LocalTransform> GetRefRWLocalTransform() => _localTransform;
    public RefRW<Boids3DComponent> GetRefRWBoids3DComponent() => _boids3DComponent;
}