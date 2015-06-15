using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	public Slider musicSlider;
	public Slider effectsSlider;
	
	void Update () {
		DataSaver.S.musicVolume = musicSlider.value;
		DataSaver.S.effectsVolume = effectsSlider.value;
	}
}
