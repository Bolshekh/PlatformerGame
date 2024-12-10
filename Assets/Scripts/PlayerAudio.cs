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
	}
}
