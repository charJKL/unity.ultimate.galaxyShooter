using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public static class SceneMetrics
{
	public static RangeInHorizontal boundXRange = new RangeInHorizontal(-9.0f, 9.0f);
	public static RangeInVertical boundYRange = new RangeInVertical(-4.0f, 6.0f);
	
	public static RangeInHorizontal spawnXRange = new RangeInHorizontal(-9.0f, 9.0f);
	public static RangeInVertical spawnYRange = new RangeInVertical(-7.0f, 7.0f);
	
	public const string TAG_PLAYER = "Player";
	public const string TAG_LASER_PLAYER = "LaserPlayer";
	public const string TAG_LASER_ENEMY = "LaserEnemy";
}
