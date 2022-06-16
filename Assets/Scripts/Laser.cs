using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Laser : MonoBehaviour
{
	[SerializeField] private float speed = 8.0f;
	[SerializeField] private Vector3 direction;
	
	[HideInInspector] public GameObject owner;
	
	private void FixedUpdate()
	{
		transform.Translate(direction * speed * Time.deltaTime);

		if(Range.OutOfRange(transform.position.y, SceneMetrics.spawnYRange))
		{
			Destroy(this.gameObject);
		}
	}
}
