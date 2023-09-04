using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class SliderValueController : MonoBehaviour
{
    public Slider slider;      // Reference to the Slider component
    public PlayerAttack scriptToReference;

    void Update()
    {
        slider.value = scriptToReference.charge;
    }

}
