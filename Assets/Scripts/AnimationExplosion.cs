using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExplosion : MonoBehaviour
{
	public void OnExplosionEnd()
	{
		Destroy(this.gameObject);
	}
}
