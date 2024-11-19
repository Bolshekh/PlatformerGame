using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
	public int Score { get; internal set; }
	[SerializeField] TMP_Text uiText;

	private void Start()
	{
		Score = 0;
	}
	public void UpdateUi()
	{
		uiText.text = Score.ToString();
	}
	public void ScoreUp()
	{
		Score++;
		UpdateUi();
	}
}
