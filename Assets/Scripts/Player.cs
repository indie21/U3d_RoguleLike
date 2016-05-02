using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int posX;
	private int posY;

	// 当前位置
	public float RestTimer  = 0;
	public float RestTime  = 0.1f;

	private Vector2 targetPos = new Vector2(1,1);
	private Rigidbody2D rigidbody2Dx;
	private BoxCollider2D selfCollider;
	private Animator selfAnimator;

	// Use this for initialization
	void Start () {
		rigidbody2Dx = GetComponent<Rigidbody2D> ();
		selfCollider = GetComponent<BoxCollider2D> ();
		selfAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.Instance.food <= 0) {
			return;
		}
		
		RestTimer += Time.deltaTime;
		if (RestTimer < RestTime) {
			return;
		}

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		// 水平大于优先
		if (h > 0) { 
			v = 0; 
		}

		if (h != 0 || v != 0) {

			GameManager.Instance.ReduceFood (1);

			// 判断碰撞
			selfCollider.enabled = false;
			
			RaycastHit2D hid2d = Physics2D.Linecast (targetPos, targetPos + new Vector2(h,v) );

			if (hid2d.transform == null) {
				targetPos += new Vector2 (h, v);

			} else {
				switch (hid2d.collider.tag) {
				case "Wall":
					hid2d.collider.SendMessage ("TakeDamage");
					selfAnimator.SetTrigger ("attack");
					break;
				case "OutWall":
					break;
				case "Suda":
					targetPos += new Vector2 (h, v);
					GameManager.Instance.AddFood (5);
					Destroy (hid2d.transform.gameObject);
					break;
				case "Food":
					targetPos += new Vector2 (h, v);
					GameManager.Instance.AddFood (10);
					Destroy (hid2d.transform.gameObject);
					break;				
				case "Enemy":
					break;
				}
			}

			RestTimer = 0;
			selfCollider.enabled = true;
			GameManager.Instance.OnPlayerMove ();
		}

		rigidbody2Dx.MovePosition(Vector2.Lerp(transform.position, 
			targetPos, 60*Time.deltaTime));



	}

	public void TakeDamage(int lossFood) {
		Debug.Log ("player take damage");
		GameManager.Instance.ReduceFood (lossFood);
		selfAnimator.SetTrigger ("damage");
	}
}
