using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiScore : MonoBehaviour
{
	[SerializeField] private Sprite[] spriteLive;
	
	[HideInInspector] private Image lives;
	[HideInInspector] private TextMeshProUGUI score;
	
	private void Awake()
	{
		lives = transform.Find("Lives").GetComponent<Image>();
		score = transform.Find("Score").GetComponent<TextMeshProUGUI>();
	}
	
	public void RefreshScore(int current)
	{
		score.text = $"Score: {current}";
	}

	public void RefreshLive(int live)
	{
		int livesIndex = Mathf.Clamp(live, 0, spriteLive.Length);
		lives.sprite = spriteLive[livesIndex];
	}
}