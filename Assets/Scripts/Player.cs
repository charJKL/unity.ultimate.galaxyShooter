using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private Transform gunPosition;
	[SerializeField] private Transform gunPositionLeftWing;
	[SerializeField] private Transform gunPositionRightWing;
	[SerializeField] private GameObject prefabLaser;
	[SerializeField] private SpawnManager spawnManager;
	
	[SerializeField] private float speed = 3.5f;
	[SerializeField] private float fireRate = 0.5f;
	[SerializeField] private int lives = 3;
	[SerializeField] private bool hasTripleShot = false;
	
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
	
	public void Damage()
	{
		lives--;
		Debug.Log($"Your current lives: {lives}");
		
		if(lives <= 0)
		{
			spawnManager.StopSpawning();
			Destroy(this.gameObject);
		}
	}
		
	public void EnableTripleShot()
	{
		hasTripleShot = true;
		StartCoroutine(TripleShotTimeoutRoutine());
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
			if(hasTripleShot)
			{
				Instantiate(prefabLaser, gunPositionLeftWing.position, Quaternion.identity);
				Instantiate(prefabLaser, gunPositionRightWing.position, Quaternion.identity);
			}
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
	
	private IEnumerator TripleShotTimeoutRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		hasTripleShot = false;
	}
}
