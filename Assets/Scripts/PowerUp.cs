using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	const string PLAYER = "Player";
	
	enum PowerUpType { TripleShoot, SpeedBoost, Shield };
	
	[SerializeField] private float speed = 3.0f;
	[SerializeField] private PowerUpType type;
	[SerializeField] private AudioClip audioCollect;
	
	private void FixedUpdate()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		
		if(transform.position.y < SceneMetrics.spawnYRange.bottom)
		{
			Destroy(this.gameObject);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case PLAYER:
				switch(this.type)
				{
					case PowerUpType.TripleShoot:
						other.GetComponent<Player>().EnableTripleShot();
						break;
						
					case PowerUpType.SpeedBoost:
						other.GetComponent<Player>().EnableSpeedBoost();
						break;
						
					case PowerUpType.Shield:
						other.GetComponent<Player>().EnableShield();
						break;
				}
				
				AudioSource.PlayClipAtPoint(audioCollect, transform.position);
				Destroy(this.gameObject);
				break;
		}
	}
}
