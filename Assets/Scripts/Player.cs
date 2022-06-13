using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed = 3.5f;
	
	void Start()
	{
		transform.position = new Vector3(0,0,0);
	}
	
	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		Vector3 direction = new Vector3(horizontal, vertical, 0);
		transform.Translate(direction * speed * Time.deltaTime);
	}
	
	void LateUpdate()
	{
		CheckBounds();
	}
	
	void CheckBounds()
	{
		float[] rangeX = {-9.0f, 9.0f};
		float[] rangeY = {-4.0f, 6.0f};

		float boundInX = Mathf.Clamp(transform.position.x, rangeX[0], rangeX[1]);
		float boundInY = Mathf.Clamp(transform.position.y, rangeY[0], rangeY[1]);
		
		transform.position = new Vector3(boundInX, boundInY, transform.position.z);
	}
}
