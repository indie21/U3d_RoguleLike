using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public void AddFood(int food) {
		food += food;
	}

	public void ReduceFood(int food) {
		food -= food;
	}

	// Use this for initialization
	void Awake () {
		_instance = this;	
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
