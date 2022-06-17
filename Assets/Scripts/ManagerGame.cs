using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ManagerGame : MonoBehaviour
{
	[SerializeField] private ManagerSpawn managerSpawn;
	[SerializeField] private uiCanvas uiCanvas;
	[SerializeField] private uiPause uiPause;
	[SerializeField] public bool isGameOver = false;
	[SerializeField] private bool isGamePaused = false;
	
	[HideInInspector] public Player[] players;
	
	private void Awake()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(SceneMetrics.TAG_PLAYER);
		players = Array.ConvertAll(gameObjects, (GameObject obj) => obj.GetComponent<Player>());
		Array.ForEach(players, (Player player) => player.OnDestroyed += CheckIfGameIsOver);
	}
	
	private void CheckIfGameIsOver()
	{
		foreach(Player player in players)
		{
			if(player.isDestroyed == false) return;
		}
		
		managerSpawn.StopSpawning();
		uiCanvas.RefreshGameOverStatus(true);
		isGameOver = true;
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
