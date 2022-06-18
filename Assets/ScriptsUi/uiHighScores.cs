using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiHighScores : MonoBehaviour
{
	[HideInInspector] private Transform list; 
	
	private void Awake()
	{
		list = transform.Find("List");
	}
	
	public void RefreshScores(List<int> scores, int? current)
	{
		for(int i=0; i < list.childCount; i++)
		{
			if(i >= scores.Count) break;
			TextMeshProUGUI position = list.GetChild(i).GetComponent<TextMeshProUGUI>();
			string number = (i + 1).ToString();
			position.text = $"{number}. {scores[i]}";
		}
	}
}
