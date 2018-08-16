using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour {

	private ParticleSystem particles;

	//-------------------------------------------------
	public void Awake()
	{
		particles = GetComponent<ParticleSystem>();

		InvokeRepeating("CheckParticleSystem", 0.1f, 0.1f);
	}


	//-------------------------------------------------
	private void CheckParticleSystem()
	{
		if (!particles.IsAlive())
		{
			Destroy(this.gameObject);
		}
	}

}
