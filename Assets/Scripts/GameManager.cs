using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static int score;
	public bool restartLevel = false;
	private bool newGame = true;
	public GUISkin skin;
	public Bird player;
	public GameObject wall;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartMenu()
	{
		restartLevel = true;
	}

	public void StartNewGame()
	{
		if (GameManager.score > PlayerPrefs.GetInt("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore", GameManager.score);
		}
		StopAllCoroutines ();
		StartCoroutine (IncScore ());
		CreateNewWalls();
		score = 0;
		Time.timeScale = 1f;
		player.transform.position = new Vector3 (0, 1.5f, 0);
		player.transform.rotation = player.normalRotation;
		player.rigidbody.velocity = new Vector3 (0, 0, 0);
	}

	IEnumerator IncScore()
	{
		yield return new WaitForSeconds (4f);
		while (true) 
		{
			GameManager.score++;
			audio.Play ();
			yield return new WaitForSeconds(1.5f);
		}
	}

	void CreateNewWalls()
	{
		Wall[] walls = Object.FindObjectsOfType<Wall> ();
		foreach( Wall w in walls)
		{
			w.DestroyWall();
		}
		for (int i = 0; i < 5; ++i)
		{
			Instantiate (wall, new Vector3(4 + i*1.5f, Random.Range(0f,2f),0), Quaternion.identity);
		}
		StartCoroutine (WallCreating ());
	}

	IEnumerator WallCreating()
	{
		while(true)
		{
			yield return new WaitForSeconds(1.5f);
			Instantiate(wall, new Vector3(10, Random.Range(0f,2f),0), Quaternion.identity);
		}
	}

	void OnGUI()
	{
		GUI.skin = skin;
		GUI.Label (new Rect (Screen.width / 2 - 30, 20, 60, 80), GameManager.score.ToString());
		if (newGame)
		{
			Time.timeScale = 0f;
			GUI.Box(new Rect(Screen.width/4,Screen.height/4, Screen.width/2, Screen.height/2), "Triangle bird");
			if (GUI.Button(new Rect(Screen.width/2 - 70, Screen.height/2, 140,30), "Start"))
			{
				newGame = false;
				StartNewGame();
			}
		}
		if (restartLevel)
		{
			Time.timeScale = 0f;
			GUI.Box(new Rect(Screen.width/4,Screen.height/4, Screen.width/2, Screen.height/2), "Triangle bird");
			if (GUI.Button(new Rect(Screen.width/2 - 70, Screen.height/2, 140,30), "Restart"))
			{
				restartLevel = false;
				StartNewGame();
			}
			if (GameManager.score > PlayerPrefs.GetInt("HighScore"))
			{
				GUI.Label(new Rect(Screen.width/4,Screen.height/2 - 80, Screen.width/2, 60), string.Format("You've beat the record!\nYour score: {0}", GameManager.score), skin.GetStyle("Lose"));
			}
			else
			{
				GUI.Label(new Rect(Screen.width/4,Screen.height/2 - 80, Screen.width/2, 60), string.Format("Your score: {0}", GameManager.score), skin.GetStyle("Lose"));
			}
		}
	}
}
