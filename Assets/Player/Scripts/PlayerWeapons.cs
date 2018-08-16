using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
	GameManager gameManager;
	int enemyLayer;

	bool leftWeaponAlreadyFiring;
	bool rightWeaponAlreadyFiring;

	public GameObject leftHand;
	public GameObject rightHand;

	GameObject[] Weapons;

	// Use this for initialization
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		enemyLayer = LayerMask.GetMask("Enemies");
		Weapons = new GameObject[] { leftHand, rightHand };
	}

	// Update is called once per frame
	void Update()
	{

		if (gameManager.paused == false)
		{
			//fire left
			if (Input.GetAxis("Fire1") > 0 || SteamVR_Controller.Input(1).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) == true)
			{
				if (leftWeaponAlreadyFiring == false)
				{
					StartCoroutine(FireWeapon(0));
					leftWeaponAlreadyFiring = true;
				}
			}

			//fire right
			if (Input.GetAxis("Fire2") > 0 || SteamVR_Controller.Input(2).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) == true)
			{

				if (rightWeaponAlreadyFiring == false)
				{
					StartCoroutine(FireWeapon(1));
					rightWeaponAlreadyFiring = true;
				}

			}

		}
	}

	IEnumerator FireWeapon(int index)
	{

		string axisName = ("Fire" + (index + 1));

		while ((Input.GetAxis(axisName) > 0 || SteamVR_Controller.Input(index + 1).GetHairTrigger() == true) && gameManager.paused == false && gameManager.playerIsDead == false)
		{
			Weapon weapon = Weapons[index].GetComponent<Weapon>();

			Transform barrelEnd = Weapons[index].transform.Find("Gun").Find("ModelCenter").Find("Model").Find("BarrelEnd").transform;
			float _damage = weapon.damage;
			float knockback = weapon.knockback;
			float fireRate = weapon.fireRate;
			GameObject _flare = weapon.flare;

			RaycastHit hit;
			Transform rayOrgin;

			if (gameManager.VRMode != true)
			{
				rayOrgin = transform.Find("NonVR").Find("Head").transform;
			}
			else
			{
				rayOrgin = barrelEnd.transform;
			}

			if (Physics.Raycast(rayOrgin.position, rayOrgin.forward, out hit, 2000f))
			{

				GameObject flare = Instantiate(_flare, barrelEnd.position, Quaternion.Euler(barrelEnd.forward));
				flare.GetComponent<TrailRenderer>().AddPosition(barrelEnd.position);
				flare.GetComponent<TrailRenderer>().AddPosition(hit.point);

				float damage = _damage * (1 - ((hit.distance - 17f) * .015f));
				damage = Mathf.Clamp(damage, 0, 100f);

				SteamVR_Controller.Input(index + 1).TriggerHapticPulse(5000);

				if (hit.transform.gameObject.GetComponent<EnemyBehavior>())
				{
					hit.transform.gameObject.GetComponent<EnemyBehavior>().health -= damage;
				}
				if (hit.transform.gameObject.GetComponent<Rigidbody>())
				{
					hit.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-hit.normal * 25 * knockback, hit.point, ForceMode.Impulse);
				}
				if (hit.transform.gameObject.GetComponent<Ricochet>() != null)
				{
					GameObject spark = hit.transform.gameObject.GetComponent<Ricochet>().spark;
					yield return new WaitForSeconds(.03f);
					Instantiate(spark, hit.point, Quaternion.Euler(hit.normal));
				}

			}
			yield return new WaitForSeconds(1f - fireRate);
		}

		if (index == 1)
		{
			rightWeaponAlreadyFiring = false;
		}
		if (index == 0)
		{
			leftWeaponAlreadyFiring = false;
		}

	}

}
