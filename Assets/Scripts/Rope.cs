using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
	[SerializeField] GameObject pillar1;
	[SerializeField] GameObject pillar2;
	[SerializeField] GameObject ropeLine;
	public Vector2 RopeBeginPoint => pillar1.transform.position;
	public Vector2 RopeEndPoint => pillar2.transform.position;
	public float RopeAngle => Vector2.Angle(RopeBeginPoint, RopeEndPoint);

	SliderJoint2D joint;
	private void Start()
	{
		joint = GetComponentInChildren<SliderJoint2D>();
		SetUpRope();
	}
	public void AttachRigidBody(Rigidbody2D Rigidbody)
	{
		joint.connectedBody = Rigidbody;
	}
	public void DeattachRigidBody()
	{
		joint.connectedBody = null;
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

		if (joint == null)
		{
			joint = GetComponentInChildren<SliderJoint2D>();
		}
		joint.angle = RopeAngle;
		joint.limits = new JointTranslationLimits2D()
		{
			min = 0, max = Vector2.Distance(pillar1.transform.localPosition, pillar1.transform.localPosition)
		};
	}
}
