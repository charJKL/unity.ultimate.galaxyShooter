using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiHighScores : MonoBehaviour
{
	[HideInInspector] private Transform list; 
	[HideInInspector] private Color defaultColor;
	private void Awake()
	{
		list = transform.Find("List");
		defaultColor = list.GetChild(0).GetComponent<TextMeshProUGUI>().color;
	}
	
	public void RefreshScores(List<int> scores, int current)
	{
		List<int> liveScores = new List<int>(scores);
		liveScores.Add(current);
		liveScores.Sort((x, y) => y - x);
		
		bool alreadyMarked = false; // flag to indicate ex aequo place.
		for(int i=0; i < list.childCount; i++)
		{
			if(i >= scores.Count) break;
			TextMeshProUGUI position = list.GetChild(i).GetComponent<TextMeshProUGUI>();
			string number = (i + 1).ToString();
			position.text = $"{number}. {liveScores[i]}";
			position.color = liveScores[i] == current && alreadyMarked == false ? Color.red : this.defaultColor;
			if(liveScores[i] == current) alreadyMarked = true;
		}
	}
}
