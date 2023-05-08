using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _perceptionRadius;

    private Vector3 _velocity;
    private Vector3 _accelerationVector;

    private void Start()
    {
        _accelerationVector = _acceleration * transform.forward;
    }

    void Update()
    {
        Edges();
        Flock();
        MoveForward();
        
        _accelerationVector = Vector3.zero;
    }

    private void MoveForward()
    {
        _velocity = transform.forward * (_moveSpeed * Time.deltaTime);
        _velocity += _accelerationVector;
        transform.rotation = Quaternion.LookRotation(_velocity.normalized + transform.forward);
        transform.position += _velocity;
    }

    private void Edges()
    {
        if(transform.position.x > 5)
            transform.position = new Vector3(-4, transform.position.y, transform.position.z);
        else if(transform.position.x < -5)
            transform.position = new Vector3(4, transform.position.y, transform.position.z);
        
        if(transform.position.y > 5) 
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        else if(transform.position.y < -5)
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        
        if(transform.position.z > 15) 
            transform.position = new Vector3(transform.position.x, transform.position.y, -14);
        else if(transform.position.z < -15)
            transform.position = new Vector3(transform.position.x, transform.position.y, 14);
    }

    private void Flock()
    {
        Vector3 alignment = Align();
        Vector3 cohesion = Cohesion();
        Vector3 separation = Separation();
        
        alignment *= UIController.Instance.GetAlignmentSliderValue();
        cohesion *= UIController.Instance.GetCohesionSliderValue();
        separation *= UIController.Instance.GetSeparationSliderValue();
        
        _accelerationVector += alignment;
        _accelerationVector += cohesion;
        _accelerationVector += separation;
    }

    private Vector3 Align()
    {
        Vector3 steering = Vector3.zero;
        int total = 0;

        foreach (Boid boid in GameController.Instance.GetBoids3D())
        {
            if (boid != this)
            {
                float distance = Vector3.Distance(transform.position, boid.transform.position);

                if (distance <= _perceptionRadius)
                {
                    steering += boid.GetVelocity();
                    total++;
                }
            }
        }
        
        if (total > 0)
        {
            steering /= total;
            steering = steering.normalized * _moveSpeed;
            steering -= _velocity;
            steering = Vector3.ClampMagnitude(steering, _acceleration);
        }

        return steering;
    }

    private Vector3 Cohesion()
    {
        Vector3 steering = Vector3.zero;
        int total = 0;

        foreach (Boid boid in GameController.Instance.GetBoids3D())
        {
            if (boid != this)
            {
                float distance = Vector3.Distance(transform.position, boid.transform.position);

                if (distance <= _perceptionRadius)
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
            steering = steering.normalized * _moveSpeed;
            steering -= _velocity;
            steering = Vector3.ClampMagnitude(steering, _acceleration);
        }

        return steering;
    }
    
    private Vector3 Separation()
    {
        Vector3 steering = Vector3.zero;
        int total = 0;

        foreach (Boid boid in GameController.Instance.GetBoids3D())
        {
            if (boid != this)
            {
                float distance = Vector3.Distance(transform.position, boid.transform.position);

                if (distance <= _perceptionRadius)
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
            steering = steering.normalized * _moveSpeed;
            steering -= _velocity;
            steering = Vector3.ClampMagnitude(steering, _acceleration);
        }

        return steering;
    }
    
    private Vector3 GetVelocity() => _velocity;
}
