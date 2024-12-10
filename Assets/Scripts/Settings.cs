using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	[SerializeField] Slider audioSlider;
	public static Settings SettingsInstance { get; set; }
	[SerializeField] List<AudioSource> allAudio;
	public void Start()
	{
		SettingsInstance = this;
		UpdateVolume();
		GlobalAudio.AudioChange += (s, e) => Settings.SettingsInstance.UpdateVolume();
		audioSlider.value = GlobalAudio.AudioVolume;
		audioSlider.onValueChanged.AddListener(delegate { SetAudio(audioSlider.value); });
	}
	public void SetAudio(float value)
	{
		Debug.Log("Audio set: " + value);
		GlobalAudio.AudioVolume = audioSlider.value;
	}

	public void UpdateVolume()
	{
		foreach (var a in allAudio)
		{
			switch (a.clip.name)
			{
				case "music":
					a.volume = GlobalAudio.AudioVolume * 0.05f;
					break;
				default:
					a.volume = GlobalAudio.AudioVolume;
					break;
			}
		};
	}
}
