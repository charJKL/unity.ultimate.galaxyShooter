using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private GameObject prefabLaser;
	[SerializeField] private SpawnManager spawnManager;

	[SerializeField] private float speed = BASE_SPEED;
	[SerializeField] private float fireRate = 0.15f;
	[SerializeField] private int lives = 3;
	[SerializeField] private bool hasTripleShot = false;
	[SerializeField] private bool hasShield = false;
	
	[HideInInspector] private Transform gunPosition;
	[HideInInspector] private Transform gunPositionLeftWing;
	[HideInInspector] private Transform gunPositionRightWing;
	[HideInInspector] private GameObject shield;
	[HideInInspector] private float fireTimeout = 0;
	
	const float BASE_SPEED = 5.0f;
	
	void Awake()
	{
		gunPosition = transform.Find("GunPosition");
		gunPositionLeftWing = transform.Find("GunPositionLeftWing");
		gunPositionRightWing = transform.Find("GunPositionRightWing");
		shield = transform.Find("Shield").gameObject;
	}
	
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
		if(hasShield)
		{
			DisableShield();
			return;
		}
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
	
	public void EnableSpeedBoost()
	{
		speed = 8.0f;
		StartCoroutine(SpeedupTimeoutRoutine());
	}
	
	public void EnableShield()
	{
		shield.SetActive(true);
		hasShield = true;
	}
	
	public void DisableShield()
	{
		shield.SetActive(false);
		hasShield = false;
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
		float boundInX = Mathf.Clamp(transform.position.x, SceneMetrics.boundXRange.left, SceneMetrics.boundXRange.right);
		float boundInY = Mathf.Clamp(transform.position.y, SceneMetrics.boundYRange.bottom, SceneMetrics.boundYRange.top);
		
		transform.position = new Vector3(boundInX, boundInY, transform.position.z);
	}
	
	private IEnumerator TripleShotTimeoutRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		hasTripleShot = false;
	}
	
	private IEnumerator SpeedupTimeoutRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		speed = BASE_SPEED;
	}
}
