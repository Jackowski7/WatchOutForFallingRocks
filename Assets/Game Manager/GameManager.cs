using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[HideInInspector]
	public int level;
	[HideInInspector]
	public float score;
	[HideInInspector]
	public int lives;
    [HideInInspector]
    public float health;
    float maxHealth = 1;

    Text levelText;
    Text scoreText;
    Text livesText;
    Text enemiesRemaining;
    GameObject healthBar;

    Spawner spawner;
    public Transform enemiesFolder;

    public GameObject overlay;
    public GameObject menuStartGame;
    public GameObject menuLevelSummary;
    public GameObject menuPlayerDied;
    public GameObject menuEndGame;

	public bool skipMenu;

	[HideInInspector]
	public bool playerIsDead;
	[HideInInspector]
	public bool paused;

	[HideInInspector]
    public bool VRMode;
	VRToggle vrToggle;

    private void Awake()
    {

		vrToggle = GetComponent<VRToggle>();
		VRMode = vrToggle.VRMode;

		spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        levelText = GameObject.Find("Level").transform.Find("Text").GetComponent<Text>();
        scoreText = GameObject.Find("Score").transform.Find("Text").GetComponent<Text>();
        livesText = GameObject.Find("Lives").transform.Find("Text").GetComponent<Text>();
        enemiesRemaining = GameObject.Find("EnemiesRemaining").transform.Find("Text").GetComponent<Text>();
        healthBar = GameObject.Find("Health").transform.Find("HP").gameObject;

        health = 1;
        score = 0;
        lives = 3;
        maxHealth = 1;
        playerIsDead = false;
    }

    void Start()
    {
		if (skipMenu == true) {
			StartLevel();
		}
		else
		{
			overlay.SetActive(true);
			menuStartGame.SetActive(true);
			paused = true;
		}
    }

    private void Update()
    {
        //UI Stuff:
        float healthPercent = (health / maxHealth);
        healthPercent = Mathf.Clamp(healthPercent, 0, 1);
        healthBar.transform.localScale = new Vector3(healthPercent, 1, 1);

        scoreText.text = Mathf.RoundToInt(score).ToString();
        livesText.text = lives.ToString();
        levelText.text = (level + 1).ToString();
    }


    public void StartGame()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        overlay.SetActive(false);
        menuStartGame.SetActive(false);
        menuLevelSummary.SetActive(false);
        menuPlayerDied.SetActive(false);
        menuEndGame.SetActive(false);

        foreach (Transform child in enemiesFolder)
        {
            GameObject.Destroy(child.gameObject);
        }

        health = 1;
        playerIsDead = false;
        paused = false;

        spawner.StartLevel(level);
    }

    public void PlayerDied()
    {
        // + player died animation
        playerIsDead = true;

        if (lives <= 0)
        {
            overlay.SetActive(true);
            menuEndGame.SetActive(true);
            paused = true;
        }
        else
        {
            overlay.SetActive(true);
            menuPlayerDied.SetActive(true);
            paused = true;
            lives--;
        }
    }

    public void NewGame()
    {
        level = 0;
        score = 0;
        lives = 3;
        health = 1;
        StartGame();
    }

    public void RestartLevel()
    {
        StartLevel();
    }

    public void LevelComplete()
    {
        level++;
        overlay.SetActive(true);
        menuLevelSummary.SetActive(true);
        paused = true;
        // + show some stats about finished level (accuracy, perfect bonus, score, etc)

    }

    public void SetEnemiesRemaining(int enemies)
    {
        enemiesRemaining.text = enemies.ToString();
    }


    public void CheckHealth()
    {
        if (health <= 0)
        {
            PlayerDied();
        }
    }

}
