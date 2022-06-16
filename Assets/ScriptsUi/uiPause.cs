using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
	public class uiPause : MonoBehaviour
	{
		[SerializeField] private ManagerGame managerGame;
		
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