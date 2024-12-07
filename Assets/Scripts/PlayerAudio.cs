using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAudio : MonoBehaviour
{
	PlayerMovement playerMovement;

	[SerializeField] List<AudioSource> playerAllAudio;

	// Start is called before the first frame update
	void Start()
	{
		playerMovement = GetComponent<PlayerMovement>();

		playerMovement.PlayerJumped += (s, e) => playerAllAudio.Find(s => s.clip.name == "Jump2").Play();

		GlobalAudio.AudioChange += (s, e) => AudioUpdate();

		AudioUpdate();
	}
	void AudioUpdate()
	{
		foreach(var a in playerAllAudio)
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
