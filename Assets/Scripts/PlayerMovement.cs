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
	PlayerScore playerScore;
	public GroundedCheck groundedCheck { get; internal set; }

	public bool IsGrounded => groundedCheck.IsGrounded;
	public bool IsDead { get; internal set; } = false;

	//speed
	[SerializeField] float speed;
	[SerializeField] float speedMultiplier;
	public float Speed => speed * speedMultiplier;

	//jump
	[SerializeField] float jumpHeight;
	[SerializeField] float jumpMultiplyer;
	public float JumpHeight => jumpHeight * jumpMultiplyer;
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
	//menu
	public bool IsInMenu { get; internal set; }
	[SerializeField] GameObject ExitMenu;
	void Start()
	{
		playerRB = GetComponent<Rigidbody2D>();
		playerScore = GetComponent<PlayerScore>();
		groundedCheck = GetComponent<GroundedCheck>();

		HideMenu();

		IsDead = false;
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

		if (Input.GetButtonDown("Cancel"))
		{
			ChangeCursorVisibility();
		}
	}
	public void HideMenu()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (!IsDead) ExitMenu.SetActive(false);
		IsInMenu = false;
	}
	public void ShowMenu()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		if (!IsDead) ExitMenu.SetActive(true);
		IsInMenu = true;
	}
	public void ChangeCursorVisibility()
	{
		GlobalScore.SetScore(playerScore.Score);
		if (IsInMenu) HideMenu();
		else ShowMenu();
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
			IsDead = true;
			GlobalScore.SetScore(playerScore.Score);
			ShowMenu();
			GameManager.Instance.GameOver();
		}

		if (collision.gameObject.CompareTag("Ground")) playerScore.ScoreUp();

		if (collision.gameObject.CompareTag("Rope")) HopOnRope(collision.gameObject.GetComponentInParent<Rope>());
	}
}
