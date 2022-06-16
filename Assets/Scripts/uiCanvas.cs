using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiCanvas : MonoBehaviour
{
	[HideInInspector] private GameObject gameOver;
	[HideInInspector] private GameObject restart;
	
	[SerializeField] private bool showGameOver = false;
	
	private void Awake()
	{
		gameOver = transform.Find("GameOver").gameObject;
		restart = transform.Find("Restart").gameObject;
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
