using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    [SerializeField] private Slider _alignmentSlider;
    [SerializeField] private Slider _cohesionSlider;
    [SerializeField] private Slider _separationSlider;

    private void Awake()
    {
        Instance = this;
    }
    
    public float GetAlignmentSliderValue() => _alignmentSlider.value;
    public float GetCohesionSliderValue() => _cohesionSlider.value;
    public float GetSeparationSliderValue() => _separationSlider.value;
}
