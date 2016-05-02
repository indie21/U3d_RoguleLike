using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {
	
	public GameObject[] OutWallArray; 
	public GameObject[] FloorArray; 
	public GameObject[] WallArray; 
	public GameObject[] FoodArray;
	public GameObject[] EnemyArray;


	private Transform mapHolder;
	private List<Vector2> positionList;
	private GameManager gameManager;

	// 最小的墙数量。
	public int MinCountWall = 2;
	public int MaxCountWall = 8;


	public const int ROWS = 10;
	public const int CLOUMS = 10;
	

	// Use this for initialization
	void Awake () {
		gameManager = this.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	// 随机返回一个可用的点.
	private Vector2 randomPosition() {
		int positionIndex = Random.Range (0, positionList.Count);
		Vector2 pos = positionList [positionIndex];
		positionList.RemoveAt (positionIndex);
		return pos;
	}

	// 随机返回一个prefeb
	private GameObject randomPrefeb(GameObject[] prefebs) {
		int index = Random.Range (0, prefebs.Length);
		return prefebs [index];
	}

	// 从数组中随机生成Count个Item.
	private void instantialItems(int count, GameObject[] Prefebs) {
		for (int i = 0; i < count; i++) {
			Vector2 pos = randomPosition ();
			GameObject obj = randomPrefeb (Prefebs);
			GameObject go = GameObject.Instantiate (obj, 
				pos, Quaternion.identity) as GameObject;
			go.transform.SetParent (mapHolder);		
		}
	}

	// 初始化地图
	public void InitMap() {
		positionList = new List<Vector2> ();
		mapHolder = new GameObject("map").transform;
		GameObject go;

		// 创建基本地图
		for (int i = 0; i < ROWS; i++) {
			for (int j = 0; j < CLOUMS; j++) {
				if (i == 0 || j == 0 || i == (ROWS - 1) || j == (CLOUMS - 1)) {
					GameObject obj = randomPrefeb (OutWallArray);
					go = GameObject.Instantiate (obj, 
						new Vector3 (i, j, 0), Quaternion.identity) as GameObject;
				} else {
					GameObject obj = randomPrefeb (FloorArray);
					go =  GameObject.Instantiate (obj, 
						new Vector3 (i, j, 0), Quaternion.identity) as GameObject;
				}
				go.transform.SetParent (mapHolder);
			}
		}

		// 获取到动态点范围。
		positionList.Clear();
		for (int i = 2; i < ROWS - 2; i++) {
			for (int j = 2; j < CLOUMS - 2; j++) {
				positionList.Add (new Vector2 (i, j));
			}
		}


		// 创建障碍物
		int wallCount = Random.Range(MinCountWall, MaxCountWall+1);
		instantialItems (wallCount, WallArray);

		// 生成食物和敌人 2 -> level*2
		int foodCount = Random.Range(2, gameManager.level*2+1);
		instantialItems (foodCount, FoodArray);

		// 敌人
		int enemyCount = gameManager.level / 2;
		instantialItems (enemyCount, EnemyArray);

	}
}
