using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	const string PLAYER = "Player";
	
	[SerializeField] private float speed = 3.0f;
	
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
				other.GetComponent<Player>().EnableTripleShot();
				Destroy(this.gameObject);
				break;
		}
	}
}
