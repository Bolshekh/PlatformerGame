using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public static class SceneManager
{
	public static void LoadGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene((int)Scenes.GameMainScene);
	}
	public static void MainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene((int)Scenes.MainMenu);
	}
	public static void Quit()
	{
		Application.Quit();
	}
}
public static class GlobalScore
{
	public static int Score { get; internal set; } = 0;
	public static void SetScore(int Score)
	{
		if(Score > GlobalScore.Score)
		{
			GlobalScore.Score = Score;
		}
	}
}

public static class GlobalAudio
{
	public static event EventHandler<UniversalEventArgs<float>> AudioChange;

	private static float vol;
	public static float AudioVolume
	{
		get => vol;
		set
		{
			vol = value;
			AudioChange?.Invoke(null, new UniversalEventArgs<float>() { Name = "AudioVolume", CustomVariable = vol });
		}
	}
}