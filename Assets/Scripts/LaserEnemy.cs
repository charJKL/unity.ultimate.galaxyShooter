using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
	[SerializeField] private float speed = 8.0f;

	private void FixedUpdate()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		
		if(transform.position.y < SceneMetrics.spawnYRange.bottom)
		{
			Destroy(this.gameObject);
		}
	}
}
