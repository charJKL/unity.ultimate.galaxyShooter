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
	
	[HideInInspector] public List<GameObject> players;
	
	private void Awake()
	{
		players = new List<GameObject>(GameObject.FindGameObjectsWithTag(SceneMetrics.TAG_PLAYER));
		players.ForEach((GameObject gameObject) => gameObject.GetComponent<Player>().OnDestroyied += CheckIfGameIsOver);
	}
	
	private void CheckIfGameIsOver(object sender, EventArgs args)
	{
		foreach(GameObject gameObject in players)
		{
			if(gameObject.GetComponent<Player>().isDestroyied == false) return;
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
