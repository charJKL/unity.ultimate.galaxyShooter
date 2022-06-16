using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayer : MonoBehaviour
{
	[SerializeField] private float speed = 8.0f;
	
	private void FixedUpdate() 
	{
		transform.Translate(Vector3.up * speed * Time.deltaTime);
		
		if(transform.position.y > SceneMetrics.spawnYRange.top)
		{
			Destroy(this.gameObject);
		}
	}
}
