using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneMetrics
{
	static public (float left, float right) boundXRange = (-9.0f, 9.0f);
	static public (float bottom, float top) boundYRange = (-4.0f, 6.0f);
	
	static public (float left, float right) spawnXRange = (-9.0f, 9.0f);
	static public (float bottom, float top) spawnYRange = (-7.0f, 7.0f);
	
	public const string TAG_PLAYER = "Player";
	public const string TAG_LASER = "Laser";
}
