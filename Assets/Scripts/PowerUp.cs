using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	const string PLAYER = "Player";
	
	enum PowerUpType { Shot, Speed };
	
	[SerializeField] private float speed = 3.0f;
	[SerializeField] private PowerUpType type;
	
	
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
					case PowerUpType.Shot:
						other.GetComponent<Player>().EnableTripleShot();
						break;
						
					case PowerUpType.Speed:
						other.GetComponent<Player>().EnableSpeedup();
						break;
				}
				
				Destroy(this.gameObject);
				break;
		}
	}
}
