using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEntity : MonoBehaviour
{
	[SerializeField] GameObject entity;
	[SerializeField] bool followRotation;
	[SerializeField] bool followScale;

	// Update is called once per frame
	void Update()
	{
		transform.position = entity.transform.position;
		if (followRotation) transform.rotation = entity.transform.rotation;
		if (followScale) transform.localScale = entity.transform.localScale;
	}
}
