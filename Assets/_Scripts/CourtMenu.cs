using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CourtMenu : MonoBehaviour
{
	public GameFile file;
	public Animator curtain;
	public TextMeshPro text;
	
	public int ctr;
	public int points = 3;
	public int courtCtr;
	public int cpuCtr = 0;
	
	public string[] courts = {"Basic", "Bell", "Stormy"}; 
	public GameObject[] courtObjs;
	
	string[] textpieces = {"Singles", "1 Player 1 CPU", "3 Point Game", "Win by 1", " Court", "Hazards ON", "Go!", "Main Menu"};
	string allText;
	
	bool axisDownX, axisDownY;
	
	AudioSource audio;
	public AudioClip enterSFX;
	
	public InputMap p2Input;
	
    // Start is called before the first frame update
    void Start()
    {
		for(int i = 0; i < file.inputs.Length; i++)
		{
			if(file.inputs[i].CPU) cpuCtr = i;
		}
		points = file.scoreToWin;
		textpieces[3] = "Win by " + file.mustWinBy;
		audio = GetComponent<AudioSource>();
        ctr = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if(file.type == GameType.SINGLES) textpieces[0] = "Singles";
		else textpieces[0] = "Doubles";
		
		textpieces[1] = (4 - cpuCtr) + " Player, " + (cpuCtr) + " CPU";
			
		textpieces[2] = points + " Point Game";
		textpieces[3] = "Win by " + file.mustWinBy;
		textpieces[4] = courts[courtCtr] + " Court";
		
		if(file.hazardsON) textpieces[5] = "Hazards ON";
		else textpieces[5] = "Hazards OFF";
		
		allText = "";
		for(int i = 0; i < textpieces.Length; i++)
		{
			if(i != ctr) allText += "<color=#888888FF>";
			else allText += "<color=yellow>";
			
			if(i < 6) allText += "< ";
			
			allText += textpieces[i];
			
			if(i < 6) allText += " >";
			
			allText += "</color>\n\n";
			
		}
        text.SetText(allText);
		
		//----------------------------//
		
		// MENU UP AND DOWN
		if(Mathf.Abs(Input.GetAxisRaw("Vertical (P1)")) > 0.8f)
		{
			if(!axisDownY)
			{
				if(Input.GetAxisRaw("Vertical (P1)") > 0) ctr--;
				if(Input.GetAxisRaw("Vertical (P1)") < 0) ctr++;
				if(ctr < 0) ctr = textpieces.Length - 1;
				ctr %= textpieces.Length;
				audio.Play();
			}
			axisDownY = true;
		}
		else axisDownY = false;
		
		// MENU LEFT AND RIGHT
		
		if(ctr == 0) // SINGLES OR DOUBLES
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal (P1)")) > 0.8f)
			{
				if(!axisDownX)
				{
					if(file.type == GameType.SINGLES) file.type = GameType.DOUBLES;
					else file.type = GameType.SINGLES;
					audio.Play();
				}
				axisDownX = true;
			}
			else axisDownX = false;
		}
		
		if(ctr == 1) // PLAYER OR CPU
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal (P1)")) > 0.8f)
			{
				if(!axisDownX)
				{
					if(Input.GetAxisRaw("Horizontal (P1)") > 0) cpuCtr++;
					if(Input.GetAxisRaw("Horizontal (P1)") < 0) cpuCtr--;
					if(cpuCtr < 0) cpuCtr = 3;
					if(cpuCtr > 3) cpuCtr = 0;
					
					for(int i = 1; i <= 3; i++)
						if(i >= 4 - cpuCtr) file.inputs[i].CPU = true;
						else file.inputs[i].CPU = false;
						
					audio.Play();
				}
				axisDownX = true;
			}
			else axisDownX = false;
		}
		
		if(ctr == 2) // SET POINTS
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal (P1)")) > 0.8f)
			{
				if(!axisDownX)
				{
					if(Input.GetAxisRaw("Horizontal (P1)") > 0) points++;
					if(Input.GetAxisRaw("Horizontal (P1)") < 0) points--;
					if(points < 2) points = 50;
					if(points > 50) points = 2;
					audio.Play();
				}
				axisDownX = true;
			}
			else axisDownX = false;
		}
		
		if(ctr == 3) // WIN BY
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal (P1)")) > 0.8f)
			{
				if(!axisDownX)
				{
					if(Input.GetAxisRaw("Horizontal (P1)") > 0) file.mustWinBy++;
					if(Input.GetAxisRaw("Horizontal (P1)") < 0) file.mustWinBy--;
					if(file.mustWinBy < 1) file.mustWinBy = 5;
					if(file.mustWinBy > 5) file.mustWinBy = 1;
					audio.Play();
				}
				axisDownX = true;
			}
			else axisDownX = false;
		}
		
		if(ctr == 4) // SET COURT
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal (P1)")) > 0.8f)
			{
				if(!axisDownX)
				{
					if(Input.GetAxisRaw("Horizontal (P1)") > 0) courtCtr++;
					if(Input.GetAxisRaw("Horizontal (P1)") < 0) courtCtr--;
					if(courtCtr < 0) courtCtr = courts.Length - 1;
					courtCtr %= courts.Length;
					audio.Play();
					
					foreach(GameObject g in courtObjs)
						g.SetActive(false);
					courtObjs[courtCtr].SetActive(true);
				}
				axisDownX = true;
			}
			else axisDownX = false;
		}
		
		if(ctr == 5) // HAZARDS
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal (P1)")) > 0.8f)
			{
				if(!axisDownX)
				{
					file.hazardsON = !file.hazardsON;
					audio.Play();
				}
				axisDownX = true;
			}
			else axisDownX = false;
		}
		
		if(ctr == textpieces.Length - 2)
		{
			if(Input.GetButtonDown("A (P1)"))
			{
				StartCoroutine(GetGoing());
			}
		}
		
		
		if(ctr == textpieces.Length - 1)
		{
			if(Input.GetButtonDown("A (P1)"))
			{
				StartCoroutine(ReturnToMenu());
			}
		}
		
		
    }
	
	IEnumerator GetGoing()
	{
		audio.PlayOneShot(enterSFX);
		this.enabled = false;
		file.scoreToWin = points;
		curtain.Play("CurtainComeDown", 0, 0f);
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene("Court_" + courts[courtCtr]);
	}
	
	IEnumerator ReturnToMenu()
	{
		audio.PlayOneShot(enterSFX);
		this.enabled = false;
		curtain.Play("CurtainComeDown", 0, 0f);
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene("Title");
	}
}
