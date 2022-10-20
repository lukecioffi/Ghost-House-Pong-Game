using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public ConstantRotator r;
	public ParticleSystem ps;
	public SpriteRenderer rend;
	public Collider2D col;
	public AudioSource audio;
	
    // Start is called before the first frame update
    void Start()
    {
        r.z = Random.Range(-10.0f, 10.0f);
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
		{
			StartCoroutine(Break());
		}
    }
	
	IEnumerator Break()
	{
		audio.Play();
		r.z = 0;
		ps.Play();
		rend.enabled = false;
		col.enabled = false;
		yield return new WaitForSeconds(20.0f);
		
        r.z = Random.Range(-6, 6);
		
		for(int i = 0; i < 50; i++)
		{
			rend.enabled = !rend.enabled;
			yield return new WaitForSeconds(0.04f);
		}
		col.enabled = true;
		rend.enabled = true;
	}
}
