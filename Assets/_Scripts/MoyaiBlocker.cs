using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoyaiBlocker : MonoBehaviour
{
	public SpriteRenderer[] faces;
	public Sprite[] sprites;
	public PlatformEffector2D[] effectors;
	
	public ParticleSystem[] lines;
	
	float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PongRunner.instance.ball != null)
		{
			if(PongRunner.instance.scores[0] == PongRunner.instance.scores[1])
			{
				faces[0].color = Color.Lerp(faces[0].color, new Color(1, 1, 1, 0.5f), t);
				faces[1].color = Color.Lerp(faces[1].color, new Color(1, 1, 1, 0.5f), t);
			}
			else
			{
				faces[0].color = Color.Lerp(faces[0].color, Color.white, t);
				faces[1].color = Color.Lerp(faces[1].color, Color.white, t);
			}
			t += 0.005f;
		}
		else t = 0;
    }
	
	public void CheckScore()
	{
		if(PongRunner.instance.scores[0] > PongRunner.instance.scores[1]) // IF BLUE TEAM IS WINNING, POINT TOWARDS BLUE TEAM
		{
			faces[0].sprite = sprites[1];
			faces[1].sprite = sprites[1];
			effectors[0].GetComponent<Collider2D>().enabled = true;
			effectors[1].GetComponent<Collider2D>().enabled = true;
			effectors[0].rotationalOffset = 90;
			effectors[1].rotationalOffset = 90;
			var main = lines[0].main;
			main.startColor = Color.red;
			main.startSpeed = -2;
			main = lines[1].main;
			main.startColor = Color.red;
			main.startSpeed = -2;
			lines[0].Play();
			lines[1].Play();
		}
		if(PongRunner.instance.scores[0] < PongRunner.instance.scores[1]) // IF RED TEAM IS WINNING, POINT TOWARDS RED TEAM
		{
			faces[0].sprite = sprites[0];
			faces[1].sprite = sprites[0];
			effectors[0].GetComponent<Collider2D>().enabled = true;
			effectors[1].GetComponent<Collider2D>().enabled = true;
			effectors[0].rotationalOffset = -90;
			effectors[1].rotationalOffset = -90;
			var main = lines[0].main;
			main.startColor = Color.blue;
			main.startSpeed = 2;
			main = lines[1].main;
			main.startColor = Color.blue;
			main.startSpeed = 2;
			lines[0].Play();
			lines[1].Play();
		}
		if(PongRunner.instance.scores[0] == PongRunner.instance.scores[1]) // IF EQUAL TEAM IS WINNING, DISAPPEAR
		{
			faces[0].sprite = sprites[2];
			faces[1].sprite = sprites[2];
			effectors[0].GetComponent<Collider2D>().enabled = false;
			effectors[1].GetComponent<Collider2D>().enabled = false;
		}
	}
}
