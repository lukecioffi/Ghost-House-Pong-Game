using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpBox : MonoBehaviour
{
	public Transform exit;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Ball")
		{
			Warp(collider.GetComponent<Ball>());
		}
	}
	
	void Warp(Ball ball)
	{
		ball.transform.position = new Vector2(ball.transform.position.x, exit.position.y);
	}
}
