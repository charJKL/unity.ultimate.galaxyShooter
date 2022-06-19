using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct ScoreRecord
{
	public int score;
	public Color? color;
	
	public ScoreRecord(int score, Color? color = null)
	{
		this.score = score;
		this.color = color;
	}
}

public class uiHighScores : MonoBehaviour
{
	[HideInInspector] private Transform list; 
	[HideInInspector] private Color defaultColor;
	
	private void Awake()
	{
		list = transform.Find("List");
		defaultColor = list.GetChild(0).GetComponent<TextMeshProUGUI>().color;
	}
	
	public void RefreshScores(List<ScoreRecord> scores)
	{
		int i = 0;
		foreach(ScoreRecord record in scores)
		{
			if(i >= list.childCount) break;
			TextMeshProUGUI position = list.GetChild(i).GetComponent<TextMeshProUGUI>();
			position.text = $"{i+1}. {record.score}";
			position.color = record.color ?? defaultColor;
			i++;
		}
	}
}
