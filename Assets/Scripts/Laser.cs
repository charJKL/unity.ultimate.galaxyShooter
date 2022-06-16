using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField] private float speed = 8.0f;
	[SerializeField] private Vector3 direction;
	
	[HideInInspector] public GameObject owner;
	
	private void FixedUpdate()
	{
		transform.Translate(direction * speed * Time.deltaTime);

		if(OutOfRange(transform.position.y, SceneMetrics.spawnYRange))
		{
			Destroy(this.gameObject);
		}
	}
	
	private bool OutOfRange(float value, (float min, float max) range)
	{
		return value < range.min || range.max < value;
	}
	
	private bool InRange(float value, (float min, float max) range)
	{
		return range.min > value && value < range.max;
	}
}
