using UnityEngine;
using UnityEngine.UI;

public class FillSliderScript : MonoBehaviour {
    [SerializeField]
    private Slider slider;
    public void SetPoints(int points){
        slider.value = points;
    }
}