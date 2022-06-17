using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ManagerGame : MonoBehaviour
{
	[SerializeField] private ManagerSpawn managerSpawn;
	[SerializeField] private Asteroid asteroid;
	[SerializeField] private uiCanvas uiCanvas;
	[SerializeField] private uiPause uiPause;
	[SerializeField] public bool isGameOver = false;
	[SerializeField] private bool isGamePaused = false;
	
	[HideInInspector] public Player[] players;
	
	private void Awake()
	{
		players = FindAllPlayersObjects();
		
		asteroid.OnDestroyed += StartGame;
		Array.ForEach(players, AssingPlayerListeners);
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
	
	private Player[] FindAllPlayersObjects()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(SceneMetrics.TAG_PLAYER);
		return Array.ConvertAll(gameObjects, (GameObject obj) => obj.GetComponent<Player>());
	}
	
	private void AssingPlayerListeners(Player player)
	{
		player.OnDestroyed += SaveScore;
		player.OnDestroyed += CheckIfGameIsOver;
	}
	
	private void StartGame(Asteroid asteroid)
	{
		managerSpawn.StartSpawning();
	}
	
	private void SaveScore(Player player)
	{
		Debug.Log("");
	}
	
	private void CheckIfGameIsOver(Player player)
	{
		if(Array.TrueForAll(players, (Player player) => player.isDestroyed))
		{
			managerSpawn.StopSpawning();
			uiCanvas.RefreshGameOverStatus(true);
			isGameOver = true;
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
