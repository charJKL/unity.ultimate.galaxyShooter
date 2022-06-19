using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class ManagerGame : MonoBehaviour
{
	[Serializable]
	private struct PlayerInstance
	{
		public Player player;
		public uiInfo uiInfo;
		public uiHighScores uiHighScores;
	}
	
	[SerializeField] private ManagerSpawn managerSpawn;
	[SerializeField] private Asteroid asteroid;
	[SerializeField] private uiCanvas uiCanvas;
	[SerializeField] private uiPause uiPause;
	[SerializeField] public bool isGameOver = false;
	[SerializeField] private bool isGamePaused = false;
	[SerializeField] private PlayerInstance[] players;
	
	[HideInInspector] private List<int> scores;
	[HideInInspector] private string dataFilepath;
	
	private struct Data 
	{
		public int[] scores;
	}
	
	private void Awake()
	{
		dataFilepath = Application.dataPath + "/data.json";
		scores = loadData();
		
		// Assign listeners for events:
		asteroid.OnDestroyed += StartGame;
		Array.ForEach(players, AssingPlayerListeners);
	}
	
	private void AssingPlayerListeners(PlayerInstance p)
	{
		p.player.OnLiveChanged += (int lives) => RefreshUiPlayerLiveCount(p.uiInfo, lives);
		p.player.OnScoreChanged += (int score) => RefreshUiPlayerScoreCount(p.uiInfo, score);
		p.player.OnScoreChanged += (int score) => RefreshUiHighScoreRank(p.uiHighScores, score);
		p.player.OnDestroyed += () => SavePlayerScore(p.player);
		p.player.OnDestroyed += CheckIfGameIsOver;
	}
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			PauseGame();
		}
		if(Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
		{
			BackToMenu();
		}
		if(Input.GetKeyDown(KeyCode.R) && isGamePaused)
		{
			ResetGame();
		}
		if(Input.GetKeyDown(KeyCode.P) && isGamePaused)
		{
			ResumeGame();
		}
	}
	
	private Player[] FindAllPlayersObjects()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(SceneMetrics.TAG_PLAYER);
		return Array.ConvertAll(gameObjects, (GameObject obj) => obj.GetComponent<Player>());
	}
	
	private void StartGame()
	{
		managerSpawn.StartSpawning();
	}
	
	private void RefreshUiPlayerLiveCount(uiInfo uiInfo, int lives)
	{
		uiInfo.RefreshLive(lives);
	}
	
	private void RefreshUiPlayerScoreCount(uiInfo uiInfo, int score)
	{
		uiInfo.RefreshScore(score);
	}
	
	private void RefreshUiHighScoreRank(uiHighScores uiHighScores, int score)
	{
		uiHighScores.RefreshScores(scores, score);
	}
	
	private void SavePlayerScore(Player player)
	{
		scores.Add(player.Score);
		scores.Sort((x, y) => y - x); // sort scores descending
		SaveData();
	}
	
	private void CheckIfGameIsOver()
	{
		if(Array.TrueForAll(players, (p) => p.player.isDestroyed))
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
	
	public void ResetGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void BackToMenu()
	{
		SceneManager.LoadScene("Main");
	}
	
	private void SaveData()
	{
		Data data = new Data(){ scores = scores.ToArray() };
		string json = JsonUtility.ToJson(data);
		File.WriteAllText(dataFilepath, json);
	}
	
	private List<int> loadData()
	{
		if(File.Exists(dataFilepath) == false) return new List<int>();
		
		string json = File.ReadAllText(dataFilepath);
		Data data = JsonUtility.FromJson<Data>(json);
		if(data.scores == null) return new List<int>();
		return new List<int>(data.scores);
	}
}
