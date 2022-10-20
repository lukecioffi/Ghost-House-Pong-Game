using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel : MonoBehaviour
{
	int timer;
	int fliprate = 500;
	
	public float thrust;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //timer++;
		
		if(timer >= fliprate)
		{
			Flip();
			timer = 0;
		}
    }
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.transform.tag == "Ball")
			collider.GetComponent<Ball>().wind = thrust * transform.localScale.y;
	}
	
	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.transform.tag == "Ball")
			collider.GetComponent<Ball>().wind = 0;
	}
	
	public void Flip()
	{
		transform.Rotate(0, 0, 180);
		thrust *= -1;
	}
}
