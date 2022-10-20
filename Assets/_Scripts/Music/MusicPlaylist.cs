using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicPlaylist : MonoBehaviour
{
	[SerializeField] AudioSource audio;
	[SerializeField] AudioClip[] tracks;


	[SerializeField] TextMeshPro currentSong_t;
	[SerializeField] Animator currentSong_a;
	
	public int ptr;
	
	public bool shuffle;

	int timer;
	
    // Start is called before the first frame update
    void Start()
    {
		timer = 0;

        if(shuffle)
		{
			ptr = Random.Range(0, tracks.Length);
		}
		
		Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 50 && !audio.isPlaying)
		{
			Next();
		}
    }
	
	void FixedUpdate()
	{
		timer++;
	}

	void Next()
	{
		if(shuffle)
		{
			int temp = ptr;
			while (ptr == temp) ptr = Random.Range(0, tracks.Length);
		}
		else
		{
			ptr++;
			if(ptr >= tracks.Length) ptr = 0;
		}
		
		Play();
	}
	
	void Skip()
	{
		
	}
	
	public void Stop()
	{
		audio.Stop();
		this.enabled = false;
	}

	void Play()
	{
		audio.clip = tracks[ptr];
		audio.Play();
		currentSong_t.SetText(audio.clip.name);
		currentSong_a.Play("Swoop", 0, 0f);
	}
}
