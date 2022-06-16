using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField] private GameObject animationExplosion;
	[SerializeField] private ManagerSpawn managerSpawn;
	
	[SerializeField] private float rotation = 20.0f;
	
	private void FixedUpdate()
	{
		transform.Rotate(Vector3.forward, rotation * Time.deltaTime);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case SceneMetrics.TAG_LASER_PLAYER:
				Instantiate(animationExplosion, transform.position, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(this.gameObject, 0.25f);
				managerSpawn.StartSpawning();
				break;
		}
	}
}
