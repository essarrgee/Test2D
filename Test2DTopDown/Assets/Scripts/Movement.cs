using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public bool mouseAim; //if true, character faces mouse
	public float speed;
	
	private Rigidbody2D rb;
	private Camera cam;
	
	private Vector2 inputdir;
	private Vector2 mouse2WorldPoint;
	
    // Start is called before the first frame update
    void Start()
    {
		cam = Camera.main;
		speed = 5f;
        rb = this.GetComponent<Rigidbody2D>();
		mouseAim = true;
		
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
}
