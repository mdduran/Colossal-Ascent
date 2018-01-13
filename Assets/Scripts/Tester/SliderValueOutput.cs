using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueOutput : MonoBehaviour {
    public Text sliderVal;
    public Slider slider;

	// Use this for initialization
	void Start () {
		if(sliderVal != null)
        {
            sliderVal.text = slider.value.ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
        sliderVal.text = slider.value.ToString();
	}
}
