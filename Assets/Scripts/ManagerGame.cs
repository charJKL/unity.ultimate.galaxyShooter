using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour
{
	[SerializeField] public bool isGameOver = false;
	
	private void Update()
	{
		ReadRestartKeystroke();
	}
	
	private void ReadRestartKeystroke()
	{
		if(Input.GetKeyDown(KeyCode.R) && isGameOver)
		{
			SceneManager.LoadScene("Game");
		}
	}
}
