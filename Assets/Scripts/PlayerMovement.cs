using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//basic
	Rigidbody2D playerRB;
	[SerializeField] LayerMask groundLayer;

	//jump
	[SerializeField] float jumpHeight;
	[SerializeField] float jumpMultiplyer;
	public float JumpHeight => jumpHeight * jumpMultiplyer;

	//speed
	[SerializeField] float speed;
	[SerializeField] float speedMultiplier;
	public float Speed => speed * speedMultiplier;

	//jump charge
	float currentJumpPower;
	[SerializeField] float jumpChargeSpeed = 0.5f;
	[SerializeField] float minJumpPower = 1f;
	[SerializeField] float maxJumpPower = 2f;
	// Start is called before the first frame update
	void Start()
	{
		playerRB = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonUp("Jump"))
		{
			Jump();
		}

		Move();

		if(Input.GetButton("Jump"))
		{
			ChargeJump();
		}
	}

	void Move()
	{
		Vector2 _dir = new Vector2(Speed * Input.GetAxis("Horizontal"), playerRB.velocity.y);

		if (_dir.x == 0) return;

		playerRB.velocity = _dir;
	}

	void ChargeJump()
	{
		if(currentJumpPower + minJumpPower > maxJumpPower) return;

		currentJumpPower += jumpChargeSpeed * Time.deltaTime;

		jumpMultiplyer = minJumpPower + currentJumpPower;
	}

	void Jump()
	{
		if (!IsGrounded()) return;

		playerRB.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);

		currentJumpPower = 0;
		jumpMultiplyer = minJumpPower;
	}

	bool IsGrounded()
	{
		float _downScale = 0.8f;
		RaycastHit2D _hit = Physics2D.BoxCast(transform.position , _downScale * transform.localScale, 0,  -1 * transform.up, ((1-_downScale) * transform.localScale.y) + 0.1f, groundLayer);

		if (_hit.collider != null) return true;
		
		return false;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Death")) GameManager.Instance.GameOver();

		if (collision.gameObject.CompareTag("Ground")) Debug.Log("Score Up"); //TODO: PLAYER SCORE
	}
}
