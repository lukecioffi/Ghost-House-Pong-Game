using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBell : MonoBehaviour
{
    AudioSource audio;
	Animator anim;
	ParticleSystem ps;
	
	public Animator darkness;
	
	int timer = 0;
	
	// Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		ps = GetComponentInChildren<ParticleSystem>();
		ps.Stop();
    }
	
	void FixedUpdate()
	{
		if(timer > 0) timer--;
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Ball" && timer == 0)
		{
			Shine();
		}
	}
	
	void Shine()
	{
		anim.Play("Toll", 0, 0f);
		audio.Play();
		ps.Play();
		timer = 60;
		Invoke("Darken", 0.5f);
	}
	
	void Darken()
	{
		darkness.Play("Darken", 0, 0f);
	}
}
