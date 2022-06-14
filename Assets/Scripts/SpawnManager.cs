using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private Transform enemyContainer;
	[SerializeField] private GameObject prefabEnemy;
	[SerializeField] private GameObject prefabPowerUp;
	
	[HideInInspector] private bool keepSpawing = true;
	
	private void Start()
	{
		StartCoroutine(SpawnEnemyRoutine());
		StartCoroutine(SpawnPowerUpRoutine());
	}
	
	public void StopSpawning()
	{
		keepSpawing = false;
	}
	
	private IEnumerator SpawnEnemyRoutine()
	{
		while(keepSpawing)
		{
			float randomInHorizontal = Random.Range(SceneMetrics.spawnXRange.left, SceneMetrics.spawnXRange.right);
			Vector3 position = new Vector3(randomInHorizontal, SceneMetrics.spawnYRange.top, 0);
			Instantiate(prefabEnemy, position, Quaternion.identity, enemyContainer);
			yield return new WaitForSeconds(5.0f);
		}
	}
	
	private IEnumerator SpawnPowerUpRoutine()
	{
		(float from, float to) timeout = (5.0f, 10.0f);
		
		while(keepSpawing)
		{
			float randomInHorizontal = Random.Range(SceneMetrics.spawnXRange.left, SceneMetrics.spawnXRange.right);
			float randomTimeout = Random.Range(timeout.from, timeout.to);
			
			Vector3 position = new Vector3(randomInHorizontal, SceneMetrics.spawnYRange.top, 0);
			Instantiate(prefabPowerUp, position, Quaternion.identity);
			yield return new WaitForSeconds(randomTimeout);
		}
	}
	
}
