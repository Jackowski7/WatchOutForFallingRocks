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
	Transform leftHandBarrelEnd;
	public float leftHandFireRate;
	public float leftHandDamage;
	public float leftHandKnockback;
	public GameObject leftHandFlare;
	public GameObject leftHandSpark;

	public GameObject rightHand;
	Transform rightHandBarrelEnd;
	public float rightHandFireRate;
	public float rightHandDamage;
	public float rightHandKnockback;
	public GameObject rightHandFlare;
	public GameObject rightHandSpark;

	private void OnValidate()
	{
		leftHandFireRate = Mathf.Clamp(leftHandFireRate, 0, .95f);
		rightHandFireRate = Mathf.Clamp(rightHandFireRate, 0, .95f);
	}

	// Use this for initialization
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		enemyLayer = LayerMask.GetMask("Enemies");
	}

	// Update is called once per frame
	void Update()
	{

		if (gameManager.paused == false)
		{
			//fire left
			if (Input.GetAxis("Fire1") > 0)
			{
				if (leftWeaponAlreadyFiring == false)
				{
					StartCoroutine(FireLeftWeapon());
					leftWeaponAlreadyFiring = true;
				}
			}

			//fire right
			if (Input.GetAxis("Fire2") > 0)
			{

				if (rightWeaponAlreadyFiring == false)
				{
					StartCoroutine(FireRightWeapon());
					rightWeaponAlreadyFiring = true;
				}

			}

		}
	}

	IEnumerator FireLeftWeapon()
	{
		while (Input.GetAxis("Fire1") > 0 && gameManager.paused == false && gameManager.playerIsDead == false)
		{

			leftHandBarrelEnd = leftHand.transform.Find("Gun").Find("ModelCenter").Find("Model").Find("BarrelEnd").transform;

			RaycastHit hit;
			Transform rayOrgin;

			if (gameManager.VRMode != true)
			{
				rayOrgin = transform.Find("NonVR").Find("Head").transform;
			}
			else
			{
				rayOrgin = leftHandBarrelEnd.transform;
			}

			if (Physics.Raycast(rayOrgin.position, rayOrgin.forward, out hit))
			{

				GameObject flare = Instantiate(leftHandFlare, leftHandBarrelEnd.position, Quaternion.Euler(leftHandBarrelEnd.forward));
				Instantiate(leftHandSpark, hit.point, Quaternion.Euler(hit.normal));

				flare.GetComponent<TrailRenderer>().AddPosition(leftHandBarrelEnd.position);
				flare.GetComponent<TrailRenderer>().AddPosition(hit.point);

				float dmg = leftHandDamage * (1 - ((hit.distance - 17f) * .015f));
				dmg = Mathf.Clamp(dmg, 0, 100f);

				Debug.Log("Damage:" + dmg + " / Distance:" + hit.distance);

				if (hit.transform.tag == "Enemy")
				{
					hit.transform.gameObject.GetComponent<EnemyBehavior>().health -= dmg;
					hit.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-hit.normal * 25 * leftHandKnockback, hit.point, ForceMode.Impulse);
				}
			}
			yield return new WaitForSeconds(1f - leftHandFireRate);

		}

		leftWeaponAlreadyFiring = false;

	}

	IEnumerator FireRightWeapon()
	{
		while (Input.GetAxis("Fire2") > 0 && gameManager.paused == false && gameManager.playerIsDead == false)
		{

			rightHandBarrelEnd = rightHand.transform.Find("Gun").Find("ModelCenter").Find("Model").Find("BarrelEnd").transform;

			RaycastHit hit;
			Transform rayOrgin;

			if (gameManager.VRMode != true)
			{
				rayOrgin = transform.Find("NonVR").Find("Head").transform;
			}
			else
			{
				rayOrgin = rightHandBarrelEnd.transform;
			}

			if (Physics.Raycast(rayOrgin.position, rayOrgin.forward, out hit))
			{

				GameObject flare = Instantiate(rightHandFlare, rightHandBarrelEnd.position, Quaternion.Euler(rightHandBarrelEnd.forward));
				Instantiate(rightHandSpark, hit.point, Quaternion.Euler(hit.normal));

				flare.GetComponent<TrailRenderer>().AddPosition(rightHandBarrelEnd.position);
				flare.GetComponent<TrailRenderer>().AddPosition(hit.point);

				float dmg = leftHandDamage * (1 - ((hit.distance - 17f) * .015f));
				dmg = Mathf.Clamp(dmg, 0, 100f);

				if (hit.transform.tag == "Enemy")
				{
					hit.transform.gameObject.GetComponent<EnemyBehavior>().health -= rightHandDamage;
					hit.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-hit.normal * 25 * rightHandKnockback, hit.point, ForceMode.Impulse);
				}

			}
			yield return new WaitForSeconds(1f - rightHandFireRate);
		}
		rightWeaponAlreadyFiring = false;

	}

}
