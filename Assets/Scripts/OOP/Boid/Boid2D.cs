using UnityEngine;

public class Boid2D : MonoBehaviour
{
    private Vector2 _velocity;
    private Vector2 _acceleration;

    private void Start()
    {
        _velocity = Random.insideUnitCircle * Random.Range(1f, 4f);
    }

    void Update()
    {
        Edges();
        Flock();
        MoveForward();
        
        _acceleration = Vector2.zero;
    }

    private void MoveForward()
    {
        transform.position += (Vector3) _velocity;
        
        _velocity += _acceleration;
        _velocity = Vector2.ClampMagnitude(_velocity, GetMaxSpeed());
        //transform.rotation = Quaternion.LookRotation(_velocity.normalized + transform.forward);
    }

    private void Edges()
    {
        if(transform.position.x > GetWidth())
            transform.position = new Vector2(-GetWidth(), transform.position.y);
        else if(transform.position.x < -GetWidth())
            transform.position = new Vector2(GetWidth(), transform.position.y);
        
        if(transform.position.y > GetHeight()) 
            transform.position = new Vector2(transform.position.x, -GetHeight());
        else if(transform.position.y < -GetHeight())
            transform.position = new Vector2(transform.position.x, GetHeight());
    }

    private void Flock()
    {
        Vector2 alignment = Align();
        Vector2 cohesion = Cohesion();
        Vector2 separation = Separation();
        
        alignment *= GetPowerAlignment();
        cohesion *= GetPowerCohesion();
        separation *= GetPowerSeparation();
        
        _acceleration += alignment;
        _acceleration += cohesion;
        _acceleration += separation;
    }

    private Vector2 Align()
    {
        Vector2 steering = Vector2.zero;
        int total = 0;

        foreach (Boid2D boid2D in GameController.Instance.GetBoids2D())
        {
            if (boid2D != this)
            {
                float distance = Vector2.Distance(transform.position, boid2D.transform.position);

                if (distance < GetPerceptionRadiusAlignment())
                {
                    steering += boid2D.GetVelocity();
                    total++;
                }
            }
        }
        
        if (total > 0)
        {
            steering /= total;
            steering = steering.normalized * GetMaxSpeed();
            steering -= _velocity;
            steering = Vector2.ClampMagnitude(steering, GetMaxForce());
        }

        return steering;
    }

    private Vector2 Cohesion()
    {
        Vector2 steering = Vector3.zero;
        int total = 0;

        foreach (Boid2D boid in GameController.Instance.GetBoids2D())
        {
            if (boid != this)
            {
                float distance = Vector2.Distance(transform.position, boid.transform.position);

                if (distance < GetPerceptionRadiusCohesion())
                {
                    steering += (Vector2) boid.transform.position;
                    total++;
                }
            }
        }
        
        if (total > 0)
        {
            steering /= total;
            steering -= (Vector2) transform.position;
            steering = steering.normalized * GetMaxSpeed();
            steering -= _velocity;
            steering = Vector2.ClampMagnitude(steering, GetMaxForce());
        }

        return steering;
    }
    
    private Vector2 Separation()
    {
        Vector2 steering = Vector3.zero;
        int total = 0;

        foreach (Boid2D boid in GameController.Instance.GetBoids2D())
        {
            if (boid != this)
            {
                float distance = Vector2.Distance(transform.position, boid.transform.position);

                if (distance <= GetPerceptionRadiusSeparation())
                {
                    Vector2 diff = transform.position - boid.transform.position;
                    diff /= distance * distance;
                    steering += diff;
                    total++;
                }
            }
        }
        
        if (total > 0)
        {
            steering /= total;
            steering = steering.normalized * GetMaxSpeed();
            steering -= _velocity;
            steering = Vector2.ClampMagnitude(steering, GetMaxForce());
        }

        return steering;
    }
    
    private Vector2 GetVelocity() => _velocity;
    private float GetMaxSpeed() => GameController.Instance.GetMaxSpeed();
    private float GetMaxForce() => GameController.Instance.GetMaxForce();
    private float GetPowerAlignment() => GameController.Instance.GetPowerAlignment();
    private float GetPerceptionRadiusAlignment() => GameController.Instance.GetPerceptionRadiusAlignment();
    private float GetPowerCohesion() => GameController.Instance.GetPowerCohesion();
    private float GetPerceptionRadiusCohesion() => GameController.Instance.GetPerceptionRadiusCohesion();
    private float GetPowerSeparation() => GameController.Instance.GetPowerSeparation();
    private float GetPerceptionRadiusSeparation() => GameController.Instance.GetPerceptionRadiusSeparation();
    private float GetWidth() => GameController.Instance.GetWidth();
    private float GetHeight() => GameController.Instance.GetHeight();
}
