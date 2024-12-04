using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
	[SerializeField] List<RawImage> layers;
	[SerializeField] float speed;
	[SerializeField] Transform followEntity;
	[SerializeField] float speedDecrease;
	// Update is called once per frame
	void Update()
	{
		UvChange();
	}
	void UvChange()
	{
		int i = 0;
		foreach (RawImage img in layers)
		{
			img.uvRect = new Rect(speed * Mathf.Pow(speedDecrease, i) * followEntity.position.x, img.uvRect.y, 1, 1);
			i++;
		}
	}
}
