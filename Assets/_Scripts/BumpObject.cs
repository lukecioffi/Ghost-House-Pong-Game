using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpObject : MonoBehaviour
{
	public Animator anim;

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Ball")
		{
			anim.Play("Bump", 0, 0f);
		}
	}
}
