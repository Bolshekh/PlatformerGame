using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public void LoadGame()
	{
		SceneManager.LoadGame();
	}
	public void Menu()
	{
		SceneManager.MainMenu();
	}
	public void Quit()
	{
		SceneManager.Quit();
	}
}
