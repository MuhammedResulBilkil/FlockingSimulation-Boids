using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    [SerializeField] private Transform _boidsParent;
    
    [SerializeField] private Boid _boidPrefab;

    private List<Boid> _boids = new List<Boid>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            Vector3 randomVector3 = Random.insideUnitSphere;
            
            Boid bird = Instantiate(_boidPrefab, (randomVector3 * 10), Quaternion.identity, _boidsParent);
            
            Quaternion lookRotation = Quaternion.LookRotation(randomVector3);
            bird.transform.rotation = lookRotation;
            
            _boids.Add(bird);
        }
    }
    
    public List<Boid> GetBoids() => _boids;
}
