using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameManager gameManager;

    public GameObject EnemySphere;
    public Transform enemiesFolder;
    public float spawnRate;

    bool allEnemiesDead = false;
    int totalEnemies;
    int spawnAmount;

    int[] spawnAmounts;
    [HideInInspector]
    public int enemiesDestroyed;

    private void OnValidate()
    {
        spawnRate = Mathf.Clamp(spawnRate, 0, 9.9f);
    }

    // Use this for initialization
    void Start()
    {
        spawnAmounts = new int[] { 25, 1 };
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevel(int level)
    {
        enemiesDestroyed = 0;
        totalEnemies = spawnAmounts[level];
        allEnemiesDead = false;

        StartCoroutine(SpawnEnemies(level));
    }


    public IEnumerator SpawnEnemies(int level)
    {
        Debug.Log(totalEnemies);
        Debug.Log(enemiesDestroyed);



        spawnAmount = spawnAmounts[level];
        while (spawnAmount > 0)
        {
            Transform spawnPoint = transform.Find("SpawnPoint");

            GameObject enemy = Instantiate(EnemySphere, spawnPoint.transform.position, spawnPoint.transform.rotation);
            enemy.transform.position = new Vector3(Random.Range(spawnPoint.transform.position.x - 5, spawnPoint.transform.position.x + 5), spawnPoint.transform.position.y, spawnPoint.transform.position.z);
            enemy.transform.parent = enemiesFolder;
            enemy.GetComponent<Rigidbody>().AddForce(enemy.transform.forward * 20 , ForceMode.Impulse);

            spawnAmount--;
            gameManager.SetEnemiesRemaining(spawnAmount);
            yield return new WaitForSeconds(10 - spawnRate);
        }

        while (enemiesDestroyed < totalEnemies)
        {
            yield return new WaitForSeconds(1);
        }

        gameManager.LevelComplete();


    }


}
