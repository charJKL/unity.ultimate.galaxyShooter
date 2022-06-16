using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private GameObject prefabLaser;
	[SerializeField] private float speed = 4.0f;
	[SerializeField] private (float min, float max) shootRate = (0.5f, 3.0f);
	
	[HideInInspector] private Transform gunPosition;
	[HideInInspector] private Player player;
	[HideInInspector] private Animator animator;
	[HideInInspector] private AudioSource audioSource;
	[HideInInspector] private bool isDestroyed = false;
	
	const string EXPLODE_ANIMATION_TRIGGER = "explode";
	
	private void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		gunPosition = transform.Find("GunPosition");
		animator = this.GetComponent<Animator>();
		audioSource = this.GetComponent<AudioSource>();
	}
	
	private void Start()
	{
		StartCoroutine(ShootingRoutine());
	}
	
	private void FixedUpdate()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		
		if(transform.position.y < SceneMetrics.spawnYRange.bottom && isDestroyed == false)
		{
			float randomInHorizontal = Random.Range(SceneMetrics.spawnXRange.left, SceneMetrics.spawnXRange.right);
			transform.position = new Vector3(randomInHorizontal, SceneMetrics.spawnYRange.top, 0);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case SceneMetrics.TAG_LASER_PLAYER:
				if(player != null) player.AddScore(12);
				Destroy(other.gameObject);
				DestroySelf();
				break;
				
			case SceneMetrics.TAG_PLAYER:
				other.GetComponent<Player>().Damage();
				DestroySelf();
				break;
		}
	}
	
	private IEnumerator ShootingRoutine()
	{
		while(true)
		{
			if(isDestroyed == true) break;
			float wait = Random.Range(shootRate.min, shootRate.max);
			yield return new WaitForSeconds(wait);
			Instantiate(prefabLaser, gunPosition.position, Quaternion.identity);
		}
	}

	private void DestroySelf()
	{
		if(isDestroyed == false)
		{
			animator.SetTrigger(EXPLODE_ANIMATION_TRIGGER);
			audioSource.Play();
			isDestroyed = true;
		}
	}
	
	// Event handler for event animation
	public void OnExplosionDispersion()
	{
		Destroy(gameObject.GetComponent<Collider2D>());
	}
	
	// Event handler for event animation
	public void OnExplosionEnd()
	{
		Destroy(this.gameObject);
	}
}
