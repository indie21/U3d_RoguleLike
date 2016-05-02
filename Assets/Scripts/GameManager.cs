using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	// singleton
	private static GameManager _instance;
	public static GameManager Instance {
		get{ 
			return _instance;
		}
	}

	// 当前关卡
	public int level = 1; 
	public int food = 10;
	public List<Enemy> enemyList = new List<Enemy> ();
	private bool sleepStep = true;
	private Text foodText;

	private MapManager mapManager;

	public void AddFood(int foodCount) {
		food += foodCount;
		UpdateFoodText (foodCount);
	}

	public void ReduceFood(int foodCount) {
		food -= foodCount;
		UpdateFoodText (-foodCount);

		if (food <= 0) {
			foodText.text = "YouDie!";
		}
	}

	// Use this for initialization
	void Awake () {
		_instance = this;	
		DontDestroyOnLoad (gameObject);
		initGame ();
	}

	void initGame() {
		foodText = GameObject.Find ("FoodText").GetComponent<Text> ();
		UpdateFoodText (0);

		mapManager = GetComponent<MapManager> ();
		mapManager.InitMap();
		enemyList.Clear ();
	}

	void UpdateFoodText(int chx) {
		if (chx == 0) {
			foodText.text = "Food:" + food;
		} else {
			foodText.text = "Food:" + food + ":" + chx;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPlayerMove() {
		if (sleepStep == true) {
			sleepStep = false;
		} else {
			sleepStep = true;
			foreach (var enemy in enemyList) {
				enemy.Move ();
			}
		}
	}

	public void OnLevelWasLoaded(int scence_level) {
		Debug.Log ("level:" + scence_level);
		level++;
		initGame ();
	}
}
