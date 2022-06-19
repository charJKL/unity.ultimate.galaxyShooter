using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiPause : MonoBehaviour
{
	public enum Status { GameIsPaused, GameIsOver };
	
	[SerializeField] private ManagerGame managerGame;
	[HideInInspector] private Animator animator;
	[HideInInspector] private GameObject uiGameOver;
	[HideInInspector] private GameObject uiGamePaused;
	[HideInInspector] private Button restart;
	[HideInInspector] private Button resume;
	
	const string ANIMATION_SHOW_PAUSE_MENU_BOOL = "showPauseMenu";
	
	private void Awake()
	{
		animator = GetComponent<Animator>();
		uiGameOver = transform.Find("GameOver").gameObject;
		uiGamePaused = transform.Find("GamePaused").gameObject;
		restart = transform.Find("Restart").GetComponent<Button>();
		resume = transform.Find("Resume").GetComponent<Button>();
	}
	
	public void Show(Status status)
	{
		gameObject.SetActive(true);
		animator.SetBool(ANIMATION_SHOW_PAUSE_MENU_BOOL, true);
		switch(status)
		{
			case Status.GameIsOver:
				uiGameOver.SetActive(true);
				uiGamePaused.SetActive(false);
				resume.interactable = false;
				break;
				
			case Status.GameIsPaused:
				uiGameOver.SetActive(false);
				uiGamePaused.SetActive(true);
				resume.interactable = true;
				break;
		}
	}
	
	public void Hide()
	{
		animator.SetBool(ANIMATION_SHOW_PAUSE_MENU_BOOL, false);
		gameObject.SetActive(false);
	}
	
	public void ClickResume()
	{
		managerGame.ResumeGame();
	}
	
	public void ClickBackToMenu()
	{
		managerGame.BackToMenu();
	}
}
