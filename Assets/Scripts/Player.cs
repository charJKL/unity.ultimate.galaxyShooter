using System.Collections;
using UnityEngine;
using Utils;
using System;

[System.Serializable]
public struct ControlSchema
{
	public string horizontal;
	public string vertical;
	public string shoot;
}

public class Player : MonoBehaviour
{
	[SerializeField] private GameObject prefabLaser;
	[SerializeField] private AudioClip audioLaser;
	
	const float BASE_SPEED = 5.0f;
	[SerializeField] private ControlSchema controlSchema;
	[SerializeField] private float speed = BASE_SPEED;
	[SerializeField] private float fireRate = 0.15f;
	[SerializeField] private int lives = 3;
	[SerializeField] private int score = 0;
	[SerializeField] private bool hasTripleShot = false;
	[SerializeField] private bool hasShield = false;
	
	[HideInInspector] private Transform gunPosition;
	[HideInInspector] private Transform gunPositionLeftWing;
	[HideInInspector] private Transform gunPositionRightWing;
	[HideInInspector] private GameObject engineLeftMalfunction;
	[HideInInspector] private GameObject engineRightMalfunction;
	[HideInInspector] private GameObject shield;
	[HideInInspector] private AudioSource audioSource;
	[HideInInspector] private float fireTimeout = 0;
	
	public event Action OnDestroyed;
	public event Action<int> OnScoreChanged;
	public event Action<int> OnLiveChanged;
	public bool isDestroyed { get { return lives <= 0; } }
	public int Score{ get { return score; } }
	public int Lives{ get { return lives; } }
	
	private void Awake()
	{
		gunPosition = transform.Find("GunPosition");
		gunPositionLeftWing = transform.Find("GunPositionLeftWing");
		gunPositionRightWing = transform.Find("GunPositionRightWing");
		engineLeftMalfunction = transform.Find("EngineLeftMalfunction").gameObject;
		engineRightMalfunction = transform.Find("EngineRightMalfunction").gameObject;
		shield = transform.Find("Shield").gameObject;
		audioSource = GetComponent<AudioSource>();
	}
	
	private void Start()
	{
		OnScoreChanged?.Invoke(score);
		OnLiveChanged?.Invoke(lives);
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
		OnLiveChanged?.Invoke(lives);
		
		switch(lives)
		{
			case 2: engineLeftMalfunction.SetActive(true); break;
			case 1: engineRightMalfunction.SetActive(true); break;
		}
		
		if(lives <= 0)
		{
			DestroySelf();
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
	
	public void AddScore(int amount)
	{
		score += amount;
		OnScoreChanged?.Invoke(score);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case SceneMetrics.TAG_LASER_ENEMY:
				Destroy(other.gameObject); // destory laser
				this.Damage();
				break;
		}
	}
	
	private void ReadInputMovement()
	{
		float horizontal = Input.GetAxis(controlSchema.horizontal);
		float vertical = Input.GetAxis(controlSchema.vertical);
		
		Vector3 direction = new Vector3(horizontal, vertical, 0);
		transform.Translate(direction * speed * Time.deltaTime);
	}
	
	private void ReadInputShoot()
	{
		bool shoot = Input.GetAxis(controlSchema.shoot) > 0;
		bool canFire = Time.time > fireTimeout;
		if(shoot && canFire)
		{
			InstantiateLaserBeam(gunPosition.position);
			if(hasTripleShot)
			{
				InstantiateLaserBeam(gunPositionLeftWing.position);
				InstantiateLaserBeam(gunPositionRightWing.position);
			}
			audioSource.clip = audioLaser;
			audioSource.Play();
			fireTimeout = Time.time + fireRate;
		}
	}
	
	private void CheckBounds()
	{
		float boundInX = Mathf.Clamp(transform.position.x, SceneMetrics.boundXRange.left, SceneMetrics.boundXRange.right);
		float boundInY = Mathf.Clamp(transform.position.y, SceneMetrics.boundYRange.bottom, SceneMetrics.boundYRange.top);
		
		transform.position = new Vector3(boundInX, boundInY, transform.position.z);
	}
	
	private void InstantiateLaserBeam(Vector3 position)
	{
		GameObject laser = Instantiate(prefabLaser, position, Quaternion.identity);
							 laser.GetComponent<Laser>().owner = this.gameObject;
	}
	
	private void DestroySelf()
	{
		OnDestroyed?.Invoke();
		Destroy(this.gameObject);
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
