using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	//basic
	Rigidbody2D playerRB;
	[SerializeField] LayerMask groundLayer;
	PlayerScore playerScore;

	public bool IsGrounded { get; set; }

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

	//koyot time

	//ui
	[SerializeField] Slider slider;

	//Rope
	bool IsOnRope { get; set; }

	//events
	public event EventHandler<UniversalEventArgs<PlayerMovement>> PlayerJumped;
	public event EventHandler<UniversalEventArgs<bool>> GroundCheckChanged;
	void Start()
	{
		playerRB = GetComponent<Rigidbody2D>();
		playerScore = GetComponent<PlayerScore>();

		StartGroundedCheck();
	}
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

		UpdateUi();
	}

	void Jump()
	{
		if (!IsGrounded && !IsOnRope) return;

		PlayerJumped?.Invoke(this, new UniversalEventArgs<PlayerMovement>() { CustomVariable = this, Name = "PlayerMovement" });

		playerRB.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);

		currentJumpPower = 0;
		jumpMultiplyer = minJumpPower;

		IsOnRope = false;
		UpdateUi();
	}

	async void StartGroundedCheck()
	{
		float _downScale = 0.8f;
		while (!gameObject.IsDestroyed())
		{
			var _prevState = IsGrounded;
			RaycastHit2D _hit = Physics2D.BoxCast(transform.position, _downScale * transform.localScale, 0, -1 * transform.up, transform.localScale.y + 0.1f, groundLayer);

			if (_hit.collider != null) IsGrounded = true;
			else IsGrounded = false;

			if (_prevState != IsGrounded) GroundCheckChanged?.Invoke(this, new UniversalEventArgs<bool>() { CustomVariable = IsGrounded, Name = "IsGrounded" });

			await Task.Delay(100);
		}
	}
	void HopOnRope(Rope Rope)
	{
		IsOnRope = true;
		speedMultiplier = 2f;
		Rope.AttachRigidBody(playerRB);

		PlayerJumped += (s, e) =>
		{
			speedMultiplier = 1f;
			Rope.DeattachRigidBody();
		};

	}
	void UpdateUi()
	{
		slider.value = currentJumpPower;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Death") && !IsOnRope)
		{
			GlobalScore.SetScore(playerScore.Score);
			GameManager.Instance.GameOver();
		}

		if (collision.gameObject.CompareTag("Ground")) playerScore.ScoreUp();

		if (collision.gameObject.CompareTag("Rope")) HopOnRope(collision.gameObject.GetComponentInParent<Rope>());
	}
}
