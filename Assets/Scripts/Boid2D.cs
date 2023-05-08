using UnityEngine;
using UnityEngine.Serialization;

public class Boid2D : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _acceleration;

    private void Start()
    {
        _velocity = Random.insideUnitCircle * Random.Range(1f, 4f);
    }

    void Update()
    {
        Edges();
        Flock();
        MoveForward();
        
        _acceleration = Vector3.zero;
    }

    private void MoveForward()
    {
        transform.position += _velocity;
        
        _velocity += _acceleration;
        _velocity = Vector3.ClampMagnitude(_velocity, GetMaxSpeed());
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
        Vector3 alignment = Align();
        Vector3 cohesion = Cohesion();
        Vector3 separation = Separation();
        
        alignment *= UIController.Instance.GetAlignmentSliderValue();
        cohesion *= UIController.Instance.GetCohesionSliderValue();
        separation *= UIController.Instance.GetSeparationSliderValue();
        
        _acceleration += alignment;
        _acceleration += cohesion;
        _acceleration += separation;
    }

    private Vector2 Align()
    {
        Vector3 steering = Vector2.zero;
        int total = 0;

        foreach (Boid2D boid2D in GameController.Instance.GetBoids2D())
        {
            if (boid2D != this)
            {
                float distance = Vector3.Distance(transform.position, boid2D.transform.position);

                if (distance < GetPerceptionRadius())
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
        Vector3 steering = Vector3.zero;
        int total = 0;

        foreach (Boid2D boid in GameController.Instance.GetBoids2D())
        {
            if (boid != this)
            {
                float distance = Vector3.Distance(transform.position, boid.transform.position);

                if (distance < GetPerceptionRadius())
                {
                    steering += boid.transform.position;
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
            steering = Vector2.ClampMagnitude(steering, GetMaxForce());
        }

        return steering;
    }
    
    private Vector2 Separation()
    {
        Vector3 steering = Vector3.zero;
        int total = 0;

        foreach (Boid2D boid in GameController.Instance.GetBoids2D())
        {
            if (boid != this)
            {
                float distance = Vector3.Distance(transform.position, boid.transform.position);

                if (distance <= GetPerceptionRadius())
                {
                    Vector3 diff = transform.position - boid.transform.position;
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
    
    private Vector3 GetVelocity() => _velocity;
    private float GetMaxSpeed() => GameController.Instance.GetMaxSpeed();
    private float GetMaxForce() => GameController.Instance.GetMaxForce();
    private float GetPerceptionRadius() => GameController.Instance.GetPerceptionRadius();
    public float GetWidth() => GameController.Instance.GetWidth();
    public float GetHeight() => GameController.Instance.GetHeight();
}
