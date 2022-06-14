using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float speed = 4.0f;
	
	[HideInInspector] private Player player;
	
	private void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	private void FixedUpdate()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		
		if(transform.position.y < SceneMetrics.spawnYRange.bottom)
		{
			float randomInHorizontal = Random.Range(SceneMetrics.spawnXRange.left, SceneMetrics.spawnXRange.right);
			transform.position = new Vector3(randomInHorizontal, SceneMetrics.spawnYRange.top, 0);
		}
	}
	
	const string LASER = "Laser";
	const string PLAYER = "Player";
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case LASER:
				if(player != null) player.AddScore(12);
				Destroy(other.gameObject);
				Destroy(this.gameObject);
				break;
				
			case PLAYER:
				other.GetComponent<Player>().Damage();
				Destroy(this.gameObject);
				break;
		}
	}
	
}
