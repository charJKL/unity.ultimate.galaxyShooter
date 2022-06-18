using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Asteroid : MonoBehaviour
{
	[SerializeField] private GameObject animationExplosion;
	[SerializeField] private float rotation = 20.0f;
	
	[HideInInspector] private bool isDestroyed = false;
	public event Action OnDestroyed;
	public bool IsDestroyed { get { return isDestroyed; } }
	
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
		OnDestroyed?.Invoke();
		Destroy(this.gameObject);
		isDestroyed = true;
	}
}
