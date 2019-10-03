using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
	private Rigidbody2D rb;
	
	void Awake() {
		rb = this.GetComponent<Rigidbody2D>();
		Physics.IgnoreLayerCollision(8,8);
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Projectile") {
			Destroy(gameObject);
		}
	}
}
