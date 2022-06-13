using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private Transform enemyContainer;
	[SerializeField] private GameObject prefabEnemy;
	
	[HideInInspector] private bool keepSpawing = true;
	
	private void Start()
	{
		StartCoroutine(SpawnEnemyRoutine());
	}
	
	public void StopSpawning()
	{
		keepSpawing = false;
	}
	
	IEnumerator SpawnEnemyRoutine()
	{
		(float left, float right) rangeX = (-10.0f, 10.0f);
		(float top, float down) rangeY = (7.0f, -6.0f);
		
		while(keepSpawing)
		{
			float randomInHorizontal = Random.Range(rangeX.left, rangeX.right);
			Vector3 position = new Vector3(randomInHorizontal, rangeY.top, 0);
			Instantiate(prefabEnemy, position, Quaternion.identity, enemyContainer);
			yield return new WaitForSeconds(5.0f);
		}
	}
}
