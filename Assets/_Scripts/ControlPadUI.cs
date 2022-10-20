using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlPadUI : MonoBehaviour
{
	public GameFile file;
	public Animator[] pads;
	public TextMeshPro[] names;
	
	public InputMap[] inputs;
	
	public string[] options = {"P1 Keys", "P2 Keys", "Joystick"};
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(file.type == GameType.DOUBLES)
		{
			pads[0].transform.position = new Vector2(-8.5f, -5.0f);
			pads[1].transform.position = new Vector2(8.5f, -5.0f);
			pads[2].transform.position = new Vector2(-3.5f, -6.5f);
			pads[3].transform.position = new Vector2(3.5f, -6.5f);
		}
		else
		{
			pads[0].transform.position = new Vector2(-3.5f, -6.5f);
			pads[1].transform.position = new Vector2(3.5f, -6.5f);
			pads[2].transform.position = new Vector2(-3.5f, -20);
			pads[3].transform.position = new Vector2(3.5f, -20);
		}			
		
		for(int i = 0; i < pads.Length; i++)
		{
			if(i < Input.GetJoystickNames().Length) pads[i].Play(options[4]);
			else pads[i].Play(options[i]);
			
			if(inputs[i].CPU) names[i].SetText("CPU");
			else names[i].SetText("P" + (i + 1));
		}
		
		
    }
}
