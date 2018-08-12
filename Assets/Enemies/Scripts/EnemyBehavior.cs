using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    
    GameManager gameManager;
    Spawner spawner;

    public float damage;
    private bool damageCounted = false;

    public float health;
    float maxHealth;
    float healthPercent;
    GameObject healthSphere;

    public float scoreValue;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        healthSphere = transform.Find("HPSphere").gameObject;
        maxHealth = health;
    }

    // Update is called once per frame
    void Update () {

        healthPercent = (health / maxHealth);
        healthSphere.transform.localScale = (Vector3.one * healthPercent);

        if (health <= 0)
        {
            spawner.enemiesDestroyed++;
            Destroy(gameObject);
            gameManager.score += scoreValue;
        }
    }

    private void OnTriggerEnter(Collider trig)
    {
        if (trig.tag == "Destroyer")
        {
            Destroy(gameObject);
        }

        if (trig.tag == "HitCounter" && damageCounted == false && gameManager.playerIsDead == false)
        {
            damageCounted = true;
            gameManager.health -= damage;
            gameManager.CheckHealth();
            spawner.enemiesDestroyed++;
        }

    }
}
