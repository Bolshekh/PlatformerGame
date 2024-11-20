using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] GameObject platformPrefab;
	[SerializeField] GameObject ropePrefab;

	public event EventHandler<UniversalEventArgs<GameObject>> PlatformCreated;
	public event EventHandler<UniversalEventArgs<GameObject>> RopeCreated;

	[SerializeField] float ropeChance = 10f;
	// Start is called before the first frame update
	void Start()
	{
		Instance = this;
	}
	public void CreateNewPlatform(GameObject CurrentPlatform)
	{
		float _newPlatformX;
		float _newPlatformY;
		if (Random.FullRandom(ropeChance))
		{
			_newPlatformX = CurrentPlatform.transform.position.x + UnityEngine.Random.Range(50, 100);
			_newPlatformY = CurrentPlatform.transform.position.y + UnityEngine.Random.Range(10f, -10f);
		}
		else
		{
			_newPlatformX = CurrentPlatform.transform.position.x + UnityEngine.Random.Range(10, 18);
			_newPlatformY = CurrentPlatform.transform.position.y + UnityEngine.Random.Range(2f, -2f);
		}

		var _newPlatformPoint = new Vector3(_newPlatformX, _newPlatformY);
		var _newPlatform = Instantiate(platformPrefab, 
			_newPlatformPoint,
			CurrentPlatform.transform.rotation);


		PlatformCreated?.Invoke(this, new UniversalEventArgs<GameObject>() { Name = "New Platform", CustomVariable = _newPlatform });
		
		if (Vector2.Distance(_newPlatform.transform.position, CurrentPlatform.transform.position) > 40 )
		{
			var _newRope = Instantiate(ropePrefab,
				new Vector3(0,0),
				CurrentPlatform.transform.rotation);

			_newRope.GetComponent<Rope>().SetUpRope(CurrentPlatform, _newPlatform);

			RopeCreated?.Invoke(this, new UniversalEventArgs<GameObject>() { Name = "New Rope", CustomVariable = _newRope });
		}
	}
	public void GameOver()
	{
		//TODO: GAME OVER
		Debug.Log("GAME OVER");
	}
}