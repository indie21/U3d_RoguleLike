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
	public int food = 100;
	public List<Enemy> enemyList = new List<Enemy> ();
	private bool sleepStep = true;
	private Text foodText;

	public void AddFood(int foodCount) {
		food += foodCount;
		UpdateFoodText (foodCount);
	}

	public void ReduceFood(int foodCount) {
		food -= foodCount;
		UpdateFoodText (-foodCount);
	}

	// Use this for initialization
	void Awake () {
		_instance = this;	
		initGame ();
	}

	void initGame() {
		foodText = GameObject.Find ("FoodText").GetComponent<Text> ();
		UpdateFoodText (0);
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
}
