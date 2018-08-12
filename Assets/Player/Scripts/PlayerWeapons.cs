using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{

    public GameObject leftHand;
    public Transform leftHandBarrelEnd;
    public float leftHandFireRate;
    public float leftHandDamage;

    public GameObject rightHand;
    public Transform rightHandBarrelEnd;
    public float rightHandFireRate;
    public float rightHandDamage;

    GameManager gameManager;
    int enemyLayer;

    bool leftWeaponAlreadyFiring;
    bool rightWeaponAlreadyFiring;

    ParticleSystem leftHandParticles;
    ParticleSystem rightHandParticles;
    public GameObject spark;

    private void OnValidate()
    {
        leftHandFireRate = Mathf.Clamp(leftHandFireRate, 0, .95f);
        rightHandFireRate = Mathf.Clamp(rightHandFireRate, 0, .95f);

        leftHandParticles = leftHandBarrelEnd.GetComponent<ParticleSystem>();
        rightHandParticles = rightHandBarrelEnd.GetComponent<ParticleSystem>();
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
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<EnemyBehavior>().health -= leftHandDamage;
                    Vector3 reflectVec = Vector3.Reflect(transform.forward, hit.normal);
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-reflectVec * 100f, hit.point, ForceMode.Impulse);
                }

                leftHandParticles.Play();
                Instantiate(spark, hit.point, Quaternion.Euler(hit.normal));
            }
            yield return new WaitForSeconds(1f - leftHandFireRate);

        }

        leftWeaponAlreadyFiring = false;

    }

    IEnumerator FireRightWeapon()
    {
        while (Input.GetMouseButton(1) == true && gameManager.paused == false && gameManager.playerIsDead == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<EnemyBehavior>().health -= rightHandDamage;
                    Vector3 reflectVec = Vector3.Reflect(transform.forward, -hit.normal);
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-reflectVec * 100f, hit.point, ForceMode.Impulse);
                }

                rightHandParticles.Play();
                Instantiate(spark, hit.point, Quaternion.Euler(hit.normal));
            }
            yield return new WaitForSeconds(1f - rightHandFireRate);
        }
        rightWeaponAlreadyFiring = false;

    }

}
