using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEntity : MonoBehaviour
{
	[SerializeField] GameObject entity;
	[SerializeField] bool followRotation;
	[SerializeField] bool followScale;
	[SerializeField] bool smoothPosition;
	// Update is called once per frame
	void Update()
	{
		if(!smoothPosition) transform.position = entity.transform.position;
		else
		{
			transform.position = Vector3.Lerp(transform.position, entity.transform.position, 0.1f);
		}
		if (followRotation) transform.rotation = entity.transform.rotation;
		if (followScale) transform.localScale = entity.transform.localScale;
	}
}
