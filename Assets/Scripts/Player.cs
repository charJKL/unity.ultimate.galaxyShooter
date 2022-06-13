using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed = 3.5f;
	[SerializeField] private float fireRate = 0.5f;
	
	[SerializeField] private Transform gunPosition;
	[SerializeField] private GameObject prefabLaser;
	
	[HideInInspector] private float fireTimeout = 0;
	
	void Start()
	{
		transform.position = new Vector3(0,0,0);
	}
	
	void FixedUpdate()
	{
		ReadInputMovement();
	}
	
	void Update()
	{
		ReadInputShoot();
	}
	
	void LateUpdate()
	{
		CheckBounds();
	}
	
	private void ReadInputMovement()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		Vector3 direction = new Vector3(horizontal, vertical, 0);
		transform.Translate(direction * speed * Time.deltaTime);
	}
	
	private void ReadInputShoot()
	{
		bool canFire = Time.time > fireTimeout;
		if(Input.GetKeyDown(KeyCode.Space) && canFire)
		{
			Instantiate(prefabLaser, gunPosition.position, Quaternion.identity);
			fireTimeout = Time.time + fireRate;
		}
	}
	
	private void CheckBounds()
	{
		(float lower, float upper) rangeX = (-9.0f, 9.0f);
		(float lower, float upper) rangeY = (-4.0f, 6.0f);

		float boundInX = Mathf.Clamp(transform.position.x, rangeX.lower, rangeX.upper);
		float boundInY = Mathf.Clamp(transform.position.y, rangeY.lower, rangeY.upper);
		
		transform.position = new Vector3(boundInX, boundInY, transform.position.z);
	}
}
