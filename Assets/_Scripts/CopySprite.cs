using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopySprite : MonoBehaviour
{
	public SpriteRenderer myRend;
	public SpriteRenderer rendToCopy;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myRend.sprite = rendToCopy.sprite;
    }
}
