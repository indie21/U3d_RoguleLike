using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private BoxCollider2D selfCollider;
	private Transform player;
	private Vector2 targetPos;
	private Rigidbody2D rigidbody2Dx;
	private Animator animator;
	public int lossFood = 10;


	// 每次主角移动两部，敌人移动一步.
	public void Move() {
		Vector2 offset = player.position - transform.position;
		Debug.Log (offset.magnitude);

		if (offset.magnitude <= 1) {
			animator.SetTrigger ("attack");
			player.SendMessage ("TakeDamage", lossFood);
			// 攻击
		} else {
			float x=0, y=0;
			// 追
			if (Mathf.Abs (offset.x) > Mathf.Abs (offset.y)) {
				// 按照x轴移动

				if (offset.x < 0) {
					x = -1;
				} else {
					x = 1; 	
				}

			} else {
				// 按照y轴移动
				if (offset.y < 0) {
					y = -1;
				} else {
					y = 1;
				}
			}


			// 运动检测.
			selfCollider.enabled = false;
			RaycastHit2D hid2d = Physics2D.Linecast (targetPos, targetPos + new Vector2(x,y) );
			selfCollider.enabled = true;

			if (hid2d.transform == null) {
				targetPos += new Vector2 (x, y);
			} else {
				switch (hid2d.collider.tag) {
				case "Player":
					break;
				case "Food":
				case "Suda":
					targetPos += new Vector2 (x, y);
					Destroy (hid2d.collider.gameObject);
					break;
				}
			}

		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("enemy start");
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		targetPos = transform.position;
		rigidbody2Dx = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		selfCollider = GetComponent<BoxCollider2D> ();


		GameManager.Instance.enemyList.Add (this);
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2Dx.MovePosition(Vector2.Lerp(transform.position, 
			targetPos, 60*Time.deltaTime));
		
	}
}
