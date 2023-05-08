using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private GameObject _allUIPanel;
    
    [SerializeField] private Button _hideUIButton;
    [SerializeField] private TextMeshProUGUI _hideUIButtonText;
    
    [SerializeField] private Button _resetValuesButton;
    
    [SerializeField] private Button _make3DButton;
    [SerializeField] private TextMeshProUGUI _make3DButtonText;
    
    [SerializeField] private Slider _widthSlider;
    [SerializeField] private TextMeshProUGUI _widthText;
    [SerializeField] private Slider _heightSlider;
    [SerializeField] private TextMeshProUGUI _heightText;
    [SerializeField] private Slider _depthSlider;
    [SerializeField] private TextMeshProUGUI _depthText;
    [SerializeField] private Slider _maxSpeedSlider;
    [SerializeField] private TextMeshProUGUI _maxSpeedText;
    [SerializeField] private Slider _maxForceSlider;
    [SerializeField] private TextMeshProUGUI _maxForceText;
    
    [SerializeField] private Slider _powerAlignmentSlider;
    [SerializeField] private TextMeshProUGUI _powerAlignmentText;
    [SerializeField] private Slider _perceptionRadiusAlignmentSlider;
    [SerializeField] private TextMeshProUGUI _perceptionRadiusAlignmentText;
    [SerializeField] private Slider _powerCohesionSlider;
    [SerializeField] private TextMeshProUGUI _powerCohesionText;
    [SerializeField] private Slider _perceptionRadiusCohesionSlider;
    [SerializeField] private TextMeshProUGUI _perceptionRadiusCohesionText;
    [SerializeField] private Slider _powerSeparationSlider;
    [SerializeField] private TextMeshProUGUI _powerSeparationText;
    [SerializeField] private Slider _perceptionRadiusSeparationSlider;
    [SerializeField] private TextMeshProUGUI _perceptionRadiusSeparationText;

    private bool _isUIHidden = false;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void OnEnable()
    {
        _hideUIButton.onClick.AddListener(HideAllUI);
        _resetValuesButton.onClick.AddListener(ResetValues);
        _make3DButton.onClick.AddListener(RestartBoid);
        
        _widthSlider.onValueChanged.AddListener(SetWidth);
        _heightSlider.onValueChanged.AddListener(SetHeight);
        _depthSlider.onValueChanged.AddListener(SetDepth);
        
        _maxSpeedSlider.onValueChanged.AddListener(SetMaxSpeed);
        _maxForceSlider.onValueChanged.AddListener(SetMaxForce);
        
        _powerAlignmentSlider.onValueChanged.AddListener(SetPowerAlignment);
        _perceptionRadiusAlignmentSlider.onValueChanged.AddListener(SetPerceptionRadiusAlignment);
        _powerCohesionSlider.onValueChanged.AddListener(SetPowerCohesion);
        _perceptionRadiusCohesionSlider.onValueChanged.AddListener(SetPerceptionRadiusCohesion);
        _powerSeparationSlider.onValueChanged.AddListener(SetPowerSeparation);
        _perceptionRadiusSeparationSlider.onValueChanged.AddListener(SetPerceptionRadiusSeparation);
    }

    private void OnDisable()
    {
        _hideUIButton.onClick.RemoveListener(HideAllUI);
        _resetValuesButton.onClick.RemoveListener(ResetValues);
        _make3DButton.onClick.RemoveListener(RestartBoid);
        
        _widthSlider.onValueChanged.RemoveListener(SetWidth);
        _heightSlider.onValueChanged.RemoveListener(SetHeight);
        _depthSlider.onValueChanged.RemoveListener(SetDepth);
        
        _maxSpeedSlider.onValueChanged.RemoveListener(SetMaxSpeed);
        _maxForceSlider.onValueChanged.RemoveListener(SetMaxForce);
        
        _powerAlignmentSlider.onValueChanged.RemoveListener(SetPowerAlignment);
        _perceptionRadiusAlignmentSlider.onValueChanged.RemoveListener(SetPerceptionRadiusAlignment);
        _powerCohesionSlider.onValueChanged.RemoveListener(SetPowerCohesion);
        _perceptionRadiusCohesionSlider.onValueChanged.RemoveListener(SetPerceptionRadiusCohesion);
        _powerSeparationSlider.onValueChanged.RemoveListener(SetPowerSeparation);
        _perceptionRadiusSeparationSlider.onValueChanged.RemoveListener(SetPerceptionRadiusSeparation);
    }
    
    private void HideAllUI()
    {
        _isUIHidden = !_isUIHidden;
        
        _allUIPanel.SetActive(!_isUIHidden);
        
        _hideUIButtonText.text = _isUIHidden ? "Show UI" : "Hide UI";
    }

    private void RestartBoid() => GameController.Instance.RestartBoid();
    private void ResetValues()
    {
        GameController.Instance.ResetValues();
        
        SetWidth(GameController.Instance.GetWidth());
        _widthSlider.value = GameController.Instance.GetWidth();
        SetHeight(GameController.Instance.GetHeight());
        _heightSlider.value = GameController.Instance.GetHeight();
        SetDepth(GameController.Instance.GetDepth());
        _depthSlider.value = GameController.Instance.GetDepth();
        SetMaxSpeed(GameController.Instance.GetMaxSpeed());
        _maxSpeedSlider.value = GameController.Instance.GetMaxSpeed();
        SetMaxForce(GameController.Instance.GetMaxForce());
        _maxForceSlider.value = GameController.Instance.GetMaxForce();
        SetPowerAlignment(GameController.Instance.GetPowerAlignment());
        _powerAlignmentSlider.value = GameController.Instance.GetPowerAlignment();
        SetPerceptionRadiusAlignment(GameController.Instance.GetPerceptionRadiusAlignment());
        _perceptionRadiusAlignmentSlider.value = GameController.Instance.GetPerceptionRadiusAlignment();
        SetPowerCohesion(GameController.Instance.GetPowerCohesion());
        _powerCohesionSlider.value = GameController.Instance.GetPowerCohesion();
        SetPerceptionRadiusCohesion(GameController.Instance.GetPerceptionRadiusCohesion());
        _perceptionRadiusCohesionSlider.value = GameController.Instance.GetPerceptionRadiusCohesion();
        SetPowerSeparation(GameController.Instance.GetPowerSeparation());
        _powerSeparationSlider.value = GameController.Instance.GetPowerSeparation();
        SetPerceptionRadiusSeparation(GameController.Instance.GetPerceptionRadiusSeparation());
        _perceptionRadiusSeparationSlider.value = GameController.Instance.GetPerceptionRadiusSeparation();
    }
    
    private void SetWidth(float value)
    {
        GameController.Instance.SetWidth(value);
        
        _widthText.text = "Width: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }
    
    private void SetHeight(float value)
    {
        GameController.Instance.SetHeight(value);
        
        _heightText.text = "Height: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }
    
    private void SetDepth(float value)
    {
        GameController.Instance.SetDepth(value);
        
        _depthText.text = "Depth: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }

    private void SetMaxSpeed(float value)
    {
        GameController.Instance.SetMaxSpeed(value);
        
        _maxSpeedText.text = "MaxSpeed: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }

    private void SetMaxForce(float value)
    {
        GameController.Instance.SetMaxForce(value);
        
        _maxForceText.text = "MaxForce: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }

    private void SetPowerAlignment(float value)
    {
        GameController.Instance.SetPowerAlignment(value);
        
        _powerAlignmentText.text = "Power: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }

    private void SetPowerCohesion(float value)
    {
        GameController.Instance.SetPowerCohesion(value);
        
        _powerCohesionText.text = "Power: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }

    private void SetPowerSeparation(float value)
    {
        GameController.Instance.SetPowerSeparation(value);
        
        _powerSeparationText.text = "Power: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }

    private void SetPerceptionRadiusAlignment(float value)
    {
        GameController.Instance.SetPerceptionRadiusAlignment(value);
        
        _perceptionRadiusAlignmentText.text = "PerceptionRadius: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }
    
    private void SetPerceptionRadiusCohesion(float value)
    {
        GameController.Instance.SetPerceptionRadiusCohesion(value);
        
        _perceptionRadiusCohesionText.text = "PerceptionRadius: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }
    
    private void SetPerceptionRadiusSeparation(float value)
    {
        GameController.Instance.SetPerceptionRadiusSeparation(value);
        
        _perceptionRadiusSeparationText.text = "PerceptionRadius: " + value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }

    public void ChangeMake3DButtonText(bool is2D)
    {
        _make3DButtonText.text = is2D ? "3D" : "2D";
    }
}
