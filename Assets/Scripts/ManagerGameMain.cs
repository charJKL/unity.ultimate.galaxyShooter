using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ManagerGameMain : MonoBehaviour
{
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			#if UNITY_EDITOR
				EditorApplication.ExitPlaymode();
			#else
				Application.Quit();
			#endif
		}
	}
	
	public void LoadSignleMode()
	{
		SceneManager.LoadScene("Single");
	}
	
	public void LoadMultiMode()
	{
		SceneManager.LoadScene("Multi");
	}
}
