using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate;
	public float damage;
	public float knockback;
	public GameObject flare;

	private void OnValidate()
	{
		fireRate = Mathf.Clamp(fireRate, 0, .95f);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
