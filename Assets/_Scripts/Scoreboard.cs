using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
	public SpriteRenderer[] r_1 = new SpriteRenderer[2];
	public SpriteRenderer[] r_2 = new SpriteRenderer[2];
	
	public Sprite[] numbers = new Sprite[10];

    // Update is called once per frame
    void Update()
    {
        r_1[0].sprite = numbers[PongRunner.instance.scores[0] / 10];
        r_1[1].sprite = numbers[PongRunner.instance.scores[0] % 10];
        r_2[0].sprite = numbers[PongRunner.instance.scores[1] / 10];
        r_2[1].sprite = numbers[PongRunner.instance.scores[1] % 10];
    }
}
