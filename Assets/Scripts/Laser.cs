using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField] private float speed = 8.0f;
	
	void FixedUpdate()
	{
		transform.Translate(Vector3.up * speed * Time.deltaTime);
		
		if(transform.position.y > SceneMetrics.spawnYRange.top)
		{
			Destroy(this.gameObject);
		}
	}
}
