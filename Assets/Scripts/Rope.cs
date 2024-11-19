using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
	[SerializeField] GameObject pillar1;
	[SerializeField] GameObject pillar2;
	[SerializeField] GameObject ropeLine;
	private void Start()
	{
		SetUpRope();
	}
	public void SetUpRope(GameObject platformBegin = null, GameObject platformEnd = null)
	{
		if (platformBegin != null && platformEnd != null)
		{
			var _pointBegin = new Vector2(platformBegin.transform.position.x + (0.5f * platformBegin.GetComponent<BoxCollider2D>().size.x),
				platformBegin.transform.position.y + (0.5f * pillar1.transform.localScale.y));

			var _pointEnd = new Vector2(platformEnd.transform.position.x - (0.5f * platformEnd.GetComponent<BoxCollider2D>().size.x),
				platformEnd.transform.position.y + (0.5f * pillar2.transform.localScale.y));

			pillar1.transform.position = _pointBegin;
			pillar2.transform.position = _pointEnd;

		}
		var _line = ropeLine.GetComponent<LineRenderer>();
		_line.SetPositions(new Vector3[] { pillar1.transform.localPosition, pillar2.transform.localPosition });

		var _edgeColl = ropeLine.GetComponent<EdgeCollider2D>();
		_edgeColl.points = new Vector2[] { pillar1.transform.localPosition, pillar2.transform.localPosition };

	}
}
