using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BoxType { PLAYAGAIN, PAUSE }
public class PlayAgain : MonoBehaviour
{
	public BoxType type;
	public TextMeshPro[] buttons;
	public int ctr;
	
	bool axisDown;
	
	AudioSource audio;
	
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Vertical (P1)")) > 0.5f)
		{
			if(!axisDown)
			{
				if(Input.GetAxisRaw("Vertical (P1)") > 0) ctr--;
				if(Input.GetAxisRaw("Vertical (P1)") < 0) ctr++;
				if(ctr < 0) ctr = 2;
				ctr %= 3;
				audio.Play();
			}
			axisDown = true;
		}
		else axisDown = false;
		
		foreach(TextMeshPro t in buttons)
		{
			t.color = Color.gray;
		}
		buttons[ctr].color = Color.yellow;
		
		if(Input.GetButtonDown("A (P1)"))
		{
			if(type == BoxType.PLAYAGAIN)
				PongRunner.instance.Quit(ctr);
			if(type == BoxType.PAUSE)
				PongRunner.instance.Pause(ctr);
		}
    }
}
