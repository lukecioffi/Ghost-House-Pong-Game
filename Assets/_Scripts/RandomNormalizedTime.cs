using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNormalizedTime : MonoBehaviour
{
	public Animator anim;
	public string animation;
    // Start is called before the first frame update
    void Start()
    {
        anim.Play(animation, 0, Random.Range(0f,1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
