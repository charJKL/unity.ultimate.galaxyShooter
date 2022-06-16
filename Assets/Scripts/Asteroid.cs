using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField] private GameObject animationExplosion;
	[SerializeField] private ManagerSpawn managerSpawn;
	[SerializeField] private float rotation = 20.0f;
	
	[HideInInspector] private bool isDestroyed = false;
	
	private void FixedUpdate()
	{
		transform.Rotate(Vector3.forward, rotation * Time.deltaTime);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case SceneMetrics.TAG_LASER_PLAYER:
				Destroy(other.gameObject);
				DestroySelf();
				break;
		}
	}
	
	private void DestroySelf()
	{
		if(isDestroyed) return;
		Instantiate(animationExplosion, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
		managerSpawn.StartSpawning();
		isDestroyed = true;
	}
}
