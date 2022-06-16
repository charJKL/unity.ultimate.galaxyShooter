using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
	public class uiPause : MonoBehaviour
	{
		[SerializeField] private ManagerGame managerGame;
		
		[HideInInspector] private Animator animator;
		
		const string ANIMATION_SHOW_PAUSE_MENU_BOOL = "showPauseMenu";
		
		private void Awake()
		{
			animator = GetComponent<Animator>();
		}
		
		public void Show()
		{
			gameObject.SetActive(true);
			animator.SetBool(ANIMATION_SHOW_PAUSE_MENU_BOOL, true);
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
}