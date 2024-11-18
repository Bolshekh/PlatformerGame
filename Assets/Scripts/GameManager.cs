using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] GameObject platformPrefab;

	public event EventHandler<UniversalEventArgs<GameObject>> PlatformCreated;
	// Start is called before the first frame update
	void Start()
	{
		Instance = this;
	}
	public void CreateNewPlatform(GameObject CurrentPlatform)
	{
		var _newPlatform = Instantiate(platformPrefab, 
			new Vector3(CurrentPlatform.transform.position.x + UnityEngine.Random.Range(10, 20), UnityEngine.Random.Range(-3f, -10f)),
			CurrentPlatform.transform.rotation);

		PlatformCreated?.Invoke(this, new UniversalEventArgs<GameObject>() { Name = "New Platform", CustomVariable = _newPlatform });
	}
	public void GameOver()
	{
		//TODO: GAME OVER
		Debug.Log("GAME OVER");
	}
}