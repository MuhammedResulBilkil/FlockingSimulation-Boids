using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    [SerializeField] private Transform _boidsParent;
    
    [SerializeField] private Boid _boid3DPrefab;
    [SerializeField] private Boid2D _boid2DPrefab;
    
    [SerializeField] private float _width;
    [SerializeField] private float _height;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _perceptionRadius;

    private List<Boid> _boids3D = new List<Boid>();
    private List<Boid2D> _boids2D = new List<Boid2D>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 randomVector3 = Random.insideUnitSphere;
            Vector2 randomVector2 = Random.insideUnitCircle;
            
            Boid2D boid2D = Instantiate(_boid2DPrefab, (randomVector2 * 10), Quaternion.identity, _boidsParent);
            
            /*Quaternion lookRotation = Quaternion.LookRotation(randomVector2);
            boid2D.transform.rotation = lookRotation;*/
            
            _boids2D.Add(boid2D);
        }
    }
    
    public float GetMaxSpeed() => _maxSpeed;
    public float GetMaxForce() => _maxForce;
    public float GetPerceptionRadius() => _perceptionRadius;
    public float GetWidth() => _width;
    public float GetHeight() => _height;
    
    public List<Boid> GetBoids3D() => _boids3D;
    public List<Boid2D> GetBoids2D() => _boids2D;
}
