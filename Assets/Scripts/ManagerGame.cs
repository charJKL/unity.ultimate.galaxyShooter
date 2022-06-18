using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class ManagerGame : MonoBehaviour
{
	[SerializeField] private ManagerSpawn managerSpawn;
	[SerializeField] private Asteroid asteroid;
	[SerializeField] private uiCanvas uiCanvas;
	[SerializeField] private uiHighScores uiHighScores;
	[SerializeField] private uiPause uiPause;
	[SerializeField] public bool isGameOver = false;
	[SerializeField] private bool isGamePaused = false;
	
	private string dataFilepath;
	[HideInInspector] private Player[] players;
	[HideInInspector] private List<int> scores;
	
	private struct Data 
	{
		public int[] scores;
	}
	
	private void Awake()
	{
		dataFilepath = Application.dataPath + "/data.json";
		players = FindAllPlayersObjects();
		scores = loadData();
		
		asteroid.OnDestroyed += StartGame;
		Array.ForEach(players, AssingPlayerListeners);
	}
	
	private void Start()
	{
		uiHighScores.RefreshScores(scores, 0);
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
		scores.Add(player.Score);
		scores.Sort((x, y) => y - x); // sort scores descending
		uiHighScores.RefreshScores(scores, player.Score);
		SaveData();
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
