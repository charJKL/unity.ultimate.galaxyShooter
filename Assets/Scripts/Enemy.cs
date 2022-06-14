using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float speed = 4.0f;
	
	private void FixedUpdate()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);
	}
	
	private void LateUpdate()
	{
		(float left, float right) rangeX = (-10.0f, 10.0f);
		(float top, float down) rangeY = (7.0f, -6.0f);
		
		if(transform.position.y < rangeY.down)
		{
			float randomInHorizontal = Random.Range(rangeX.left, rangeX.right);
			transform.position = new Vector3(randomInHorizontal, rangeY.top, 0);
		}
	}
	
	const string LASER = "Laser";
	const string PLAYER = "Player";
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case LASER:
				Destroy(other.gameObject);
				Destroy(this.gameObject);
				break;
				
			case PLAYER:
				Player player = other.GetComponent<Player>();
				player.Damage(); 
				Destroy(this.gameObject);
				break;
		}
	}
	
}
