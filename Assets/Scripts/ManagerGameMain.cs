using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGameMain : MonoBehaviour
{
	public void LoadLevel()
	{
		SceneManager.LoadScene("Game");
	}
}
