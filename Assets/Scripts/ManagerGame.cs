using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ui;

public class ManagerGame : MonoBehaviour
{
	[SerializeField] private uiPause uiPause;
	[SerializeField] public bool isGameOver = false;
	[SerializeField] private bool isGamePaused = false;
	
	[HideInInspector] public GameObject[] players;
	
	private void Awake()
	{
		players = GameObject.FindGameObjectsWithTag(SceneMetrics.TAG_PLAYER);
	}
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			switch(isGamePaused)
			{
				case true: ResumeGame(); break;
				case false: PauseGame(); break;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.R) && isGameOver)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		
		if(Input.GetKeyDown(KeyCode.Escape) && isGameOver)
		{
			BackToMenu();
		}
	}
	
	public void ResumeGame()
	{
		uiPause.Hide();
		Time.timeScale = 1;
		isGamePaused = false;
	}
	
	public void PauseGame()
	{
		uiPause.Show();
		Time.timeScale = 0;
		isGamePaused = true;
	}
	
	public void BackToMenu()
	{
		SceneManager.LoadScene("Main");
	}
}
