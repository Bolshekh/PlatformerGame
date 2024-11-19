using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	int platformToGenerate = 1;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlatformGenerator") && platformToGenerate > 0)
		{
			platformToGenerate--;
			GameManager.Instance.CreateNewPlatform(gameObject);
		}

	}
}
