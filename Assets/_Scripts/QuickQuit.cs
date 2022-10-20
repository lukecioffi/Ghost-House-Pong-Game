using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickQuit : MonoBehaviour
{
	public Animator anim;
	public LineRenderer line;
	
	float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Escape))
		{
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Stay"))
			{
				anim.Play("Slide In");
			}
			
			if(timer >= 4.375f)
			{
				Application.Quit();
				return;
			}
			
			line.SetPosition(1, new Vector3(timer - 2.1875f, 0, 0));
			
			timer += 0.05f;
		}
		else
		{
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Stay Out"))
			{
				anim.Play("Slide Out");
			}
			timer = 0;
		}
    }
}
