using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	public static GameControl instance = null;

	[SerializeField]
	GameObject restartButton;

    [SerializeField]
	Text highScoreText;

	[SerializeField]
	Text yourScoreText;
	
	[SerializeField]
	GameObject[] obstacles;

	[SerializeField]
	Transform spawnPoint;

	[SerializeField]
	float spawnRate = 2f;
	float nextSpawn;

	[SerializeField]
	float timeToBoost = 5f;
	float nextBoost;

	[SerializeField]
	GameObject upButton;
	
	int highScore = 0, yourScore = 0;

	public static bool gameStopped;

	float nextScoreIncrease = 0f;
	 
	 
	 
	// Use this for initialization
	void Start () {
		  
		if (instance == null) 
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		restartButton.SetActive (false);
		upButton.SetActive(true);
		yourScore = 0;
		gameStopped = false;
		Time.timeScale = 1f;
		highScore = PlayerPrefs.GetInt ("highScore");
		nextSpawn = Time.time + spawnRate;
		nextBoost = Time.unscaledTime + timeToBoost;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStopped)
		{	
		IncreaseYourScore ();
		highScoreText.text = "HI " + highScore;
		yourScoreText.text = ""+yourScore;
        }

        if (Time.time > nextSpawn)
			SpawnObstacle ();

		if (Time.unscaledTime > nextBoost && !gameStopped)
			BoostTime ();
		
		
		 if (Input.GetKeyUp(KeyCode.Escape))
         {
            SceneManager.LoadScene (0);
         }
	}

	public void trexHit()
	{
		if (yourScore > highScore)
			PlayerPrefs.SetInt("highScore", yourScore);
		Time.timeScale = 0;
		gameStopped = true;
		restartButton.SetActive (true);
		upButton.SetActive(false);	
		FindObjectOfType<AudioManager>().Play("hit");
		PlayerMovement.anim.Play("hit");
		Debug.Log("here at hit");
	}

	void SpawnObstacle()
	{
		nextSpawn = Time.time + spawnRate;
   		int randomObstacle = UnityEngine.Random.Range (0, yourScore>100? obstacles.Length:(obstacles.Length-1));
		Instantiate (obstacles [randomObstacle], spawnPoint.position, Quaternion.identity);
	}

	void BoostTime()
	{
		nextBoost = Time.unscaledTime + timeToBoost;
		if(Time.timeScale==3f)
			Time.timeScale=1.25f;
		else
		 Time.timeScale += 0.25f;
   	}

	void IncreaseYourScore()
	{
		if (Time.unscaledTime > nextScoreIncrease) {
			yourScore += 1;
			nextScoreIncrease = Time.unscaledTime + 1;
		}
	}

	public void RestartGame()
	{
		SceneManager.LoadScene (0);
	}
	
}