using UnityEngine;

public class Boid3D : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _acceleration;

    private void Start()
    {
        _velocity = Random.insideUnitSphere * Random.Range(1f, 4f);
    }

    void Update()
    {
        Edges();
        Flock();
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position += _velocity;
        transform.rotation = Quaternion.LookRotation(_velocity.normalized);
        
        //Debug.DrawRay(transform.position, _velocity.normalized * 2f, Color.red);
        
        _velocity += _acceleration;
        _velocity = Vector3.ClampMagnitude(_velocity, GetMaxSpeed());
        
        _acceleration = Vector3.zero;
    }

    private void Edges()
    {
        if(transform.position.x > GetWidth())
            transform.position = new Vector3(-GetWidth(), transform.position.y);
        else if(transform.position.x < -GetWidth())
            transform.position = new Vector3(GetWidth(), transform.position.y);
        
        if(transform.position.y > GetHeight()) 
            transform.position = new Vector3(transform.position.x, -GetHeight());
        else if(transform.position.y < -GetHeight())
            transform.position = new Vector3(transform.position.x, GetHeight());
        
        if(transform.position.z > GetDepth()) 
            transform.position = new Vector3(transform.position.x, transform.position.y, -GetDepth());
        else if(transform.position.z < -GetDepth())
            transform.position = new Vector3(transform.position.x, transform.position.y, GetDepth());
    }

    private void Flock()
    {
        Vector3 alignment = Align();
        Vector3 cohesion = Cohesion();
        Vector3 separation = Separation();
        
        alignment *= GetPowerAlignment();
        cohesion *= GetPowerCohesion();
        separation *= GetPowerSeparation();
        
        _acceleration += alignment;
        _acceleration += cohesion;
        _acceleration += separation;
    }

    private Vector3 Align()
    {
        Vector3 steering = Vector3.zero;
        int total = 0;

        foreach (Boid3D boid3D in GameController.Instance.GetBoids3D())
        {
            if (boid3D != this)
            {
                float distance = Vector3.Distance(transform.position, boid3D.transform.position);

                if (distance < GetPerceptionRadiusAlignment())
                {
                    steering += boid3D.GetVelocity();
                    total++;
                }
            }
        }
        
        if (total > 0)
        {
            steering /= total;
            steering = steering.normalized * GetMaxSpeed();
            steering -= _velocity;
            steering = Vector3.ClampMagnitude(steering, GetMaxForce());
        }

        return steering;
    }

    private Vector3 Cohesion()
    {
        Vector3 steering = Vector3.zero;
        int total = 0;

        foreach (Boid3D boid3D in GameController.Instance.GetBoids3D())
        {
            if (boid3D != this)
            {
                float distance = Vector3.Distance(transform.position, boid3D.transform.position);

                if (distance < GetPerceptionRadiusCohesion())
                {
                    steering += boid3D.transform.position;
                    total++;
                }
            }
        }
        
        if (total > 0)
        {
            steering /= total;
            steering -= transform.position;
            steering = steering.normalized * GetMaxSpeed();
            steering -= _velocity;
            steering = Vector3.ClampMagnitude(steering, GetMaxForce());
        }

        return steering;
    }
    
    private Vector3 Separation()
    {
        Vector3 steering = Vector3.zero;
        int total = 0;

        foreach (Boid3D boid3D in GameController.Instance.GetBoids3D())
        {
            if (boid3D != this)
            {
                float distance = Vector3.Distance(transform.position, boid3D.transform.position);

                if (distance <= GetPerceptionRadiusSeparation())
                {
                    Vector3 diff = transform.position - boid3D.transform.position;
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
            steering = Vector3.ClampMagnitude(steering, GetMaxForce());
        }

        return steering;
    }
    
    private Vector3 GetVelocity() => _velocity;
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
    private float GetDepth() => GameController.Instance.GetDepth();
}
