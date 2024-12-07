using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	[SerializeField] Slider AudioSlider;
	public void UpdateAudio()
	{
		GlobalAudio.AudioVolume = AudioSlider.value;
	}
}
