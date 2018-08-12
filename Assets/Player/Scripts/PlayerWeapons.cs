using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{

    public GameObject leftHand;
    public Transform leftHandBarrelEnd;
    public float leftHandFireRate;


    public GameObject rightHand;
    public Transform rightHandBarrelEnd;
    public float rightHandFireRate;

    public GameObject bulletPref;

    GameManager gameManager;

    bool leftWeaponAlreadyFiring;
    bool rightWeaponAlreadyFiring;


    private void OnValidate()
    {
        leftHandFireRate = Mathf.Clamp(leftHandFireRate, 0, .95f);
        rightHandFireRate = Mathf.Clamp(rightHandFireRate, 0, .95f);
    }

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.paused == false)
        {
            //fire left
            if (Input.GetMouseButton(0) == true) //or left trigger is held)
            {
                if (Input.GetMouseButtonDown(0) == true) //yes im calling this again on purpose, else = controller trigger
                {
                    if (leftWeaponAlreadyFiring == false)
                    {
                        StartCoroutine(FireLeftWeapon());
                        leftWeaponAlreadyFiring = true;
                    }
                }
                else
                {
                    // instantiate from gun itself forward
                }


            }

            //fire right
            if (Input.GetMouseButton(1) == true) //or left trigger is held)
            {
                if (Input.GetMouseButtonDown(1) == true) //yes im calling this again on purpose, else = controller trigger
                {
                    if (rightWeaponAlreadyFiring == false)
                    {
                        StartCoroutine(FireRightWeapon());
                        rightWeaponAlreadyFiring = true;
                    }
                }
                else
                {
                    // instantiate from gun itself forward
                }


            }
        }

    }


    IEnumerator FireLeftWeapon()
    {
        while (Input.GetMouseButton(0) == true && gameManager.paused == false && gameManager.playerIsDead == false)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit);
            GameObject bullet = Instantiate(bulletPref, leftHandBarrelEnd.position, Quaternion.LookRotation((leftHandBarrelEnd.position - hit.point), Vector3.up));
            bullet.GetComponent<Rigidbody>().AddForce((hit.point - leftHandBarrelEnd.position).normalized * 100f /*bullet speed*/, ForceMode.Impulse);

            yield return new WaitForSeconds(1f - leftHandFireRate);
        }
        leftWeaponAlreadyFiring = false;

    }

    IEnumerator FireRightWeapon()
    {
        while (Input.GetMouseButton(1) == true && gameManager.paused == false && gameManager.playerIsDead == false)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit);
            GameObject bullet = Instantiate(bulletPref, rightHandBarrelEnd.position, Quaternion.LookRotation((rightHandBarrelEnd.position - hit.point), Vector3.up));
            bullet.GetComponent<Rigidbody>().AddForce((hit.point - rightHandBarrelEnd.position).normalized * 100f /*bullet speed*/, ForceMode.Impulse);

            yield return new WaitForSeconds(1f - rightHandFireRate);
        }
        rightWeaponAlreadyFiring = false;

    }

}
