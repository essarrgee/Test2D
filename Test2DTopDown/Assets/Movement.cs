using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float speed;
	public Rigidbody2D rb;
	Vector2 inputdir;
	
    // Start is called before the first frame update
    void Start()
    {
		speed = 5f;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputdir.x = Input.GetAxisRaw("Horizontal");
		inputdir.y = Input.GetAxisRaw("Vertical");
		inputdir = inputdir.normalized;
    }
	
	void FixedUpdate()
	{
		//rb.transform.position = rb.transform.position+inputdir*speed;
		rb.MovePosition(rb.position+inputdir*speed*Time.fixedDeltaTime);
	}
}
