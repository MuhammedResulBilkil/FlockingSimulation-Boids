using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    [SerializeField] private Transform _boidsParent;

    [SerializeField] private CinemachineVirtualCamera _mainCinemachineVirtualCamera;
    
    [SerializeField] private Boid3D _boid3DPrefab;
    [SerializeField] private Boid2D _boid2DPrefab;

    [SerializeField] private int _boidAmount;
    
    [SerializeField] private float _width;
    [SerializeField] private float _height;
    [SerializeField] private float _depth;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _powerAlignment;
    [SerializeField] private float _perceptionRadiusAlignment;
    [SerializeField] private float _powerCohesion;
    [SerializeField] private float _perceptionRadiusCohesion;
    [SerializeField] private float _powerSeparation;
    [SerializeField] private float _perceptionRadiusSeparation;
    [SerializeField] private float _cameraDistance;
    
    [SerializeField] private bool _is2D;

    private CinemachineFramingTransposer _mainCameraFramingTransposer;
    
    private int _defaultSpawnAmount;
    
    private float _defaultWidth;
    private float _defaultHeight;
    private float _defaultDepth;
    private float _defaultMaxSpeed;
    private float _defaultMaxForce;
    private float _defaultPowerAlignment;
    private float _defaultPerceptionRadiusAlignment;
    private float _defaultPowerCohesion;
    private float _defaultPerceptionRadiusCohesion;
    private float _defaultPowerSeparation;
    private float _defaultPerceptionRadiusSeparation;
    private float _defaultCameraDistance;

    private List<Boid3D> _boids3D = new();
    private List<Boid2D> _boids2D = new();

    private void Awake()
    {
        Instance = this;

        _defaultSpawnAmount = _boidAmount;
        
        _mainCameraFramingTransposer = _mainCinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        _defaultWidth = _width;
        _defaultHeight = _height;
        _defaultDepth = _depth;
        _defaultMaxSpeed = _maxSpeed;
        _defaultMaxForce = _maxForce;
        _defaultPowerAlignment = _powerAlignment;
        _defaultPerceptionRadiusAlignment = _perceptionRadiusAlignment;
        _defaultPowerCohesion = _powerCohesion;
        _defaultPerceptionRadiusCohesion = _perceptionRadiusCohesion;
        _defaultPowerSeparation = _powerSeparation;
        _defaultPerceptionRadiusSeparation = _perceptionRadiusSeparation;

        _defaultCameraDistance = _cameraDistance;
        
        SetCameraDistance(_cameraDistance);
    }

    void Start()
    {
        if(_is2D)
            Spawn2DBoids();
        else
            Spawn3DBoids();
    }

    public void RestartBoid(bool isBoidAmountChanged)
    {
        if (!isBoidAmountChanged)
        {
            _is2D = !_is2D;
            UIController.Instance.ChangeMake3DButtonText(_is2D);
        }

        for (int i = 0; i < _boids2D.Count; i++)
            Destroy(_boids2D[i].gameObject);
        
        for (int i = 0; i < _boids3D.Count; i++)
            Destroy(_boids3D[i].gameObject);
        
        _boids2D.Clear();
        _boids3D.Clear();
        
        if(_is2D)
            Spawn2DBoids();
        else
            Spawn3DBoids();
    }
    
    private void Spawn2DBoids()
    {
        for (int i = 0; i < _boidAmount; i++)
        {
            Vector2 randomVector2 = Random.insideUnitCircle;
            
            Boid2D boid2D = Instantiate(_boid2DPrefab, (randomVector2 * 10), Quaternion.identity, _boidsParent);
            
            _boids2D.Add(boid2D);
        }
    }
    
    private void Spawn3DBoids()
    {
        for (int i = 0; i < _boidAmount; i++)
        {
            Vector3 randomVector3 = Random.insideUnitSphere;
            
            Boid3D boid3D = Instantiate(_boid3DPrefab, (randomVector3 * 10), Quaternion.identity, _boidsParent);

            _boids3D.Add(boid3D);
        }
    }

    public void ResetValues()
    {
        _boidAmount = _defaultSpawnAmount;
        
        _width = _defaultWidth;
        _height = _defaultHeight;
        _depth = _defaultDepth;
        _maxSpeed = _defaultMaxSpeed;
        _maxForce = _defaultMaxForce;
        _powerAlignment = _defaultPowerAlignment;
        _perceptionRadiusAlignment = _defaultPerceptionRadiusAlignment;
        _powerCohesion = _defaultPowerCohesion;
        _perceptionRadiusCohesion = _defaultPerceptionRadiusCohesion;
        _powerSeparation = _defaultPowerSeparation;
        _perceptionRadiusSeparation = _defaultPerceptionRadiusSeparation;
        
        _cameraDistance = _defaultCameraDistance;
        _mainCameraFramingTransposer.m_CameraDistance = _cameraDistance;
    }
    
    public void SetBoidAmount(int value)
    {
        _boidAmount = value;
        
        RestartBoid(true);
    }

    public int GetBoidAmount() => _boidAmount;
    public float GetMaxSpeed() => _maxSpeed;
    public void SetMaxSpeed(float value) => _maxSpeed = value;
    public float GetMaxForce() => _maxForce;
    public void SetMaxForce(float value) => _maxForce = value;
    public float GetPowerAlignment() => _powerAlignment;
    public void SetPowerAlignment(float value) => _powerAlignment = value;
    public float GetPerceptionRadiusAlignment() => _perceptionRadiusAlignment;
    public void SetPerceptionRadiusAlignment(float value) => _perceptionRadiusAlignment = value;
    public float GetPowerCohesion() => _powerCohesion;
    public void SetPowerCohesion(float value) => _powerCohesion = value;
    public float GetPerceptionRadiusCohesion() => _perceptionRadiusCohesion;
    public void SetPerceptionRadiusCohesion(float value) => _perceptionRadiusCohesion = value;
    public float GetPowerSeparation() => _powerSeparation;
    public void SetPowerSeparation(float value) => _powerSeparation = value;
    public float GetPerceptionRadiusSeparation() => _perceptionRadiusSeparation;
    public void SetPerceptionRadiusSeparation(float value) => _perceptionRadiusSeparation = value;
    public void SetWidth(float value) => _width = value;
    public float GetWidth() => _width;
    public void SetHeight(float value) => _height = value;
    public float GetHeight() => _height;
    public void SetDepth(float value) => _depth = value;
    public float GetDepth() => _depth;
    public void SetCameraDistance(float value)
    {
        _cameraDistance = value;
        _mainCameraFramingTransposer.m_CameraDistance = _cameraDistance;
    }
    public float GetCameraDistance() => _cameraDistance;

    public List<Boid3D> GetBoids3D() => _boids3D;
    public List<Boid2D> GetBoids2D() => _boids2D;
}
