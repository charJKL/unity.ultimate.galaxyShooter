using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField] private float speed = 8.0f;
	
	void FixedUpdate()
	{
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
	
	void LateUpdate()
	{
		(float lower, float upper) rangeY = (0.0f, 8.0f);
		
		if(transform.position.y > rangeY.upper) Destroy(gameObject);
	}
}
