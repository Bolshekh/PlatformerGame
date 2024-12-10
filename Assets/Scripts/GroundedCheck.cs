using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
	[SerializeField] LayerMask groundLayer;
	public bool IsGrounded { get; internal set; }

	float _downScale = 0.8f;

	//events
	public event EventHandler<UniversalEventArgs<bool>> GroundCheckChanged;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		var _prevState = IsGrounded;
		RaycastHit2D _hit = Physics2D.BoxCast(transform.position, _downScale * transform.localScale, 0, -1 * transform.up, transform.localScale.y + 0.1f, groundLayer);

		if (_hit.collider != null) IsGrounded = true;
		else IsGrounded = false;

		if (_prevState != IsGrounded) GroundCheckChanged?.Invoke(this, new UniversalEventArgs<bool>() { CustomVariable = IsGrounded, Name = "IsGrounded" });

	}
}
