using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiCanvas : MonoBehaviour
{
	[SerializeField] private Sprite[] spriteLive;
	
	[HideInInspector] private Image lives;
	[HideInInspector] private TextMeshProUGUI score;
	[HideInInspector] private GameObject gameOver;
	[HideInInspector] private GameObject restart;
	
	[SerializeField] private bool showGameOver = false;
	
	private void Awake()
	{
		lives = transform.Find("Lives").GetComponent<Image>();
		score = transform.Find("Score").GetComponent<TextMeshProUGUI>();
		gameOver = transform.Find("GameOver").gameObject;
		restart = transform.Find("Restart").gameObject;
	}
	
	public void RefreshScore(int current)
	{
		score.text = $"Score: {current}";
	}
	
	public void RefreshLive(int live)
	{
		int livesIndex = Mathf.Clamp(live, 0, spriteLive.Length);
		lives.sprite = spriteLive[livesIndex];
	}
	
	public void RefreshGameOverStatus(bool status)
	{
		showGameOver = status;
		restart.SetActive(status);
		StartCoroutine(FlikerGameOverRoutine());
	}
	
	private IEnumerator FlikerGameOverRoutine()
	{
		while(showGameOver)
		{
			gameOver.SetActive(!gameOver.activeSelf);
			yield return new WaitForSeconds(0.3f);
		}
	}
}
