using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float speed = 4.0f;
	
	[HideInInspector] private Player player;
	[HideInInspector] private Animator animator;
	
	const string EXPLODE_ANIMATION_TRIGGER = "explode";
	const string LASER = "Laser";
	const string PLAYER = "Player";
	
	private void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		animator = this.GetComponent<Animator>();
		
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
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case LASER:
				if(player != null) player.AddScore(12);
				Destroy(other.gameObject);
				DestroySelf();
				break;
				
			case PLAYER:
				other.GetComponent<Player>().Damage();
				DestroySelf();
				break;
		}
	}
	
	private void DestroySelf()
	{
		animator.SetTrigger(EXPLODE_ANIMATION_TRIGGER);
		Destroy(this.gameObject, 2.0f);
	}
}
