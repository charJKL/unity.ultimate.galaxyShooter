using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGameMain : MonoBehaviour
{
	public void LoadSignleMode()
	{
		SceneManager.LoadScene("Single");
	}
	
	public void LoadMultiMode()
	{
		SceneManager.LoadScene("Multi");
	}
}
