using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	public int id;
	private ParticleSystem[] ps;
	private AudioSource audio;
	
	void Start()
	{
		ps = GetComponentsInChildren<ParticleSystem>();
		audio = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.transform.tag == "Ball")
		{
			Destroy(collider.gameObject, 1.0f);
			collider.enabled = false;
			foreach(ParticleSystem p in ps)
				p.Play();
			audio.Play();
			PongRunner.instance.Score(id);
		}
	}
}
