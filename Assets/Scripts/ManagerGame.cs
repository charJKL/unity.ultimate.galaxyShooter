using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class ManagerGame : MonoBehaviour
{
	[Serializable]
	private struct PlayerInstance
	{
		public Player player;
		public uiInfo uiInfo;
		public uiHighScores uiHighScores;
		public Color color;
	}
	
	[SerializeField] private ManagerSpawn managerSpawn;
	[SerializeField] private Asteroid asteroid;
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
		Time.timeScale = 1; // Time.timeScale is transferable between play sessions, and may be in `Pause` from previous session, that's why is important to reset it on start play.
		
		asteroid.OnDestroyed += StartGame;
		Array.ForEach(players, AssingPlayerListeners);
	}
	
	private void Start()
	{
		// Assign listeners for events:
		uiPause.Resume.onClick.AddListener(ResumeGame);
		uiPause.Restart.onClick.AddListener(RestartGame);
		uiPause.Back.onClick.AddListener(BackToMenu);
	}
	
	private void AssingPlayerListeners(PlayerInstance p)
	{
		p.player.OnLiveChanged += (int lives) => RefreshUiPlayerLiveCount(p.uiInfo, lives);
		p.player.OnScoreChanged += (int score) => RefreshUiPlayerScoreCount(p.uiInfo, score);
		p.player.OnScoreChanged += (int score) => RefreshUiHighScoreRank();
		p.player.OnDestroyed += () => SavePlayerScore(p.player);
		p.player.OnDestroyed += CheckIfGameIsOver;
	}
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
		{
			PauseGame();
			return;
		}
		if(Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
		{
			BackToMenu();
			return;
		}
		if(Input.GetKeyDown(KeyCode.R) && isGamePaused)
		{
			RestartGame();
			return;
		}
		if(Input.GetKeyDown(KeyCode.P) && isGamePaused)
		{
			ResumeGame();
			return;
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
	
	private void RefreshUiHighScoreRank()
	{
		// Build live score by adding current gameplay score to saved one: 
		List<ScoreRecord> ranking = scores.ConvertAll<ScoreRecord>((score) => new ScoreRecord(score));
		foreach(PlayerInstance p in players) ranking.Add(new ScoreRecord(p.player.Score, p.color));
		ranking.Sort((x , y) => y.score - x.score);
		
		foreach(PlayerInstance p in players) p.uiHighScores.RefreshScores(ranking);
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
			uiPause.Show(uiPause.Status.GameIsOver);
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
		uiPause.Show(uiPause.Status.GameIsPaused);
		Time.timeScale = 0;
		isGamePaused = true;
	}
	
	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void BackToMenu()
	{
		SceneManager.LoadScene("Main");
	}
	
	[DllImport("__Internal")] private static extern void StoreScores(string data);
	[DllImport("__Internal")] private static extern string LoadScores();
		
	private void SaveData()
	{
		Data data = new Data(){ scores = scores.ToArray() };
		string json = JsonUtility.ToJson(data);
		#if UNITY_WEBGL
			StoreScores(json);
		#else
			File.WriteAllText(dataFilepath, json);
		#endif
	}
	
	private List<int> loadData()
	{
		try
		{
			string json = "";
			#if UNITY_WEBGL
				json = LoadScores();
			#else
				if(File.Exists(dataFilepath) == false) return new List<int>();
				json = File.ReadAllText(dataFilepath);
			#endif
			Data data = JsonUtility.FromJson<Data>(json);
			if(data.scores == null) return new List<int>();
			return new List<int>(data.scores);
		}
		catch(Exception)
		{
			return new List<int>();
		}
	}
}
