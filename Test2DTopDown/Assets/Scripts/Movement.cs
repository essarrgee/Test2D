using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public GameObject projectilePrefab;
	public bool mouseAim; //if true, character faces mouse
	public float speed;
	public float projectileSpeed;
	public float projectileRate;
	
	private Rigidbody2D rb;
	private Camera cam;
	private Transform firePoint;
	
	private Vector2 inputdir;
	private Vector2 mouse2WorldPoint;
	private float currentTick;
	
    // Start is called before the first frame update
    void Awake()
    {
		mouseAim = true;
		//mouseAim = false;
		speed = 5f;
		projectileSpeed = 25f;
		projectileRate = 0.1f;
		
		currentTick = 0f;
		
		rb = this.GetComponent<Rigidbody2D>();
		cam = Camera.main;
		firePoint = GameObject.FindWithTag("FirePoint").transform; //Finds firePoint object, which is tagged by "FirePoint"
		
		rb.freezeRotation = true; //rotation is not affected by physics
		rb.gravityScale = 0; //amount that object is affected by gravity
		rb.drag = 0; //how much movement is affected by air/water friction
    }

    // Update is called once per frame
    void Update()
    {
		//Move based on -1, 0, or 1 for directional controls
        inputdir.x = Input.GetAxisRaw("Horizontal");
		inputdir.y = Input.GetAxisRaw("Vertical");
		inputdir = inputdir.normalized; //normalize input direction
		//Get Mouse Position
		if (mouseAim) {
			mouse2WorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
		}
		//Mouse input
		//if (Input.GetButtonDown("Fire1")) { //Default MouseButton1
		//	FireProjectile();
		//}
		if (Input.GetKey(KeyCode.Mouse0) && currentTick >= projectileRate) {
			currentTick = 0f;
			FireProjectile();
		}
		else {
			currentTick += Time.deltaTime;
		}
    }
	
	void FixedUpdate()
	{
		//rb.transform.position = rb.transform.position+inputdir*speed;
		rb.MovePosition(rb.position+inputdir*speed*Time.fixedDeltaTime);
		if (mouseAim) {
			Vector2 lookVector = mouse2WorldPoint - rb.position;
			float faceAngle = Mathf.Atan2(lookVector.y, lookVector.x) * Mathf.Rad2Deg + 90f;
			//float faceAngle = Vector2.SignedAngle(transform.position, worldPoint); //get angle between player and mousePos
			//faceAngle = faceAngle * Mathf.Deg2Rad; //to convert to radians
			//rb.MoveRotation(faceAngle); //built-in lerp rotation
			rb.rotation = faceAngle;
		}
	}
	
	void FireProjectile() {
		GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D rbp = projectile.GetComponent<Rigidbody2D>();
		rbp.AddForce(firePoint.up*projectileSpeed, ForceMode2D.Impulse);
		
		Destroy(projectile, 1);
	}
}
