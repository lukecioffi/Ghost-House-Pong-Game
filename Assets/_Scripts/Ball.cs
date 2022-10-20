using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public Rigidbody2D rb;
	public float speed;
	
	public ParticleSystem fadePS;
	public ParticleSystem starPS;
	
	public AudioSource[] audio;
	public AudioClip bounceSFX;
	public AudioClip hitSFX;
	public AudioClip starSFX;
	
	int knockTime;
	
	float spin;
	public float wind;
	
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.velocity.magnitude < speed)
			rb.velocity = rb.velocity.normalized * speed;
		
		if(rb.velocity.magnitude > speed)
			rb.drag = 0.15f;
		else rb.drag = 0f;
		
		if(knockTime > 0)
		{
			knockTime--;
			rb.AddForce(new Vector2(0, spin * 15));
		}
		else
		{
			knockTime = 0;
			starPS.Stop();
			var em = fadePS.emission;
			em.rateOverTime = 0;
		}
		
		if(rb.velocity.magnitude > 1) rb.AddForce(new Vector2(0, wind));
		
		if(Mathf.Abs(rb.velocity.x) < 2f) rb.velocity = new Vector2(rb.velocity.x * 2, rb.velocity.y);
    }
	
	public void Bounce()
	{
		audio[0].clip = bounceSFX;
		audio[0].Play();
	}
	
	public void Knock(float x, float y)
	{
		rb.velocity *= 2.2f;
		starPS.Play();
		var em = fadePS.emission;
		em.rateOverTime = 2;
		audio[0].clip = hitSFX;
		audio[0].Play();
		audio[1].clip = starSFX;
		audio[1].Play();
		
		knockTime = 50;
		spin = y;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(knockTime < 30) Bounce();
	}
}
