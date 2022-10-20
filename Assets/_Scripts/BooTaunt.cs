using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooTaunt : MonoBehaviour
{
	public InputMap input;
	public Animator anim;
	public RuntimeAnimatorController[] skins;
	public AudioClip[] clips;
	AudioSource audio;
	
	public bool random;
    // Start is called before the first frame update
    void Start()
    {
		audio = GetComponent<AudioSource>();
		
		int r = Random.Range(0, skins.Length);
        anim.runtimeAnimatorController = skins[r];
		audio.clip = clips[r];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(input.B))
		{
			StopAllCoroutines();
			StartCoroutine(Taunt());
		}
    }
	
	IEnumerator Taunt()
	{
		audio.Play();
		transform.rotation = Quaternion.Euler(0, 0, 0);
		for(int i = 0; i < 360; i += 15)
		{
			transform.Rotate(0, 0, 15);
			yield return new WaitForSeconds(0.01f);
		}
		
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}
