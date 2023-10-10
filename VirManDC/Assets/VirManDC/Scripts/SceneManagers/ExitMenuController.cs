// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenuController : MonoBehaviour
{
	public GameObject menuPanel;
	
	private bool isInMenu = true;
	public GameObject returnToMenuButton;
	
	public void OpenCloseMenu()
	{
		menuPanel.SetActive(!menuPanel.activeSelf);
		returnToMenuButton.SetActive(!isInMenu);
	}
	
	public void ReturnToStartMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		// <- To allow check menu
		StaticDataHolder.DebugMode = 0;
		// ->
		EnteredStartMenu();
	}
	
	public void ExitApp()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit ();
		#endif
	}
	
	public void ExitedStartMenu()
	{
		isInMenu = false;
	}
	
	public void EnteredStartMenu()
	{
		isInMenu = true;
	}

}
