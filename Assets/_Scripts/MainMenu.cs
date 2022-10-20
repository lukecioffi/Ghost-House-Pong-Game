using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
	public TextMeshPro pressStart;
	public PixelPerfectCamera ppc;
	public AudioSource audio;
	
	float t = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("A (P1)") || Input.GetButtonDown("B (P1)") || Input.GetButtonDown("Start (P1)"))
		{
			if(pressStart.text == "PRESS START")
			{
				StartCoroutine(CameraDive());
				return;
			}
		}
    }
	
	void FixedUpdate()
	{
		if(pressStart.text == "")
		{
			ppc.assetsPPU = (int)Mathf.Lerp(16, 120, t);
			Camera.main.transform.position = Vector3.Lerp(new Vector3(0, 0, -10), new Vector3(0, -5, -10), t);
			t += 0.02f;
		}
	}
		
	
	IEnumerator CameraDive()
	{
		pressStart.SetText("");
		audio.Play();
		t = 0;
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("SelectCourt");
	}
}
