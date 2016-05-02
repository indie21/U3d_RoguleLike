using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public int hp = 2;

	// 墙体受损图片.
	public Sprite damageSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 自身受到攻击.
	public void TakeDamage() {
		hp -= 1 ;	
		GetComponent<SpriteRenderer> ().sprite = damageSprite;

		// 销毁自己.
		if (hp <= 0) {
			Destroy (this.gameObject);
		}
	}
}
