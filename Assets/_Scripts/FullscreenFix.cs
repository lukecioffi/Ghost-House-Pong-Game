using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Screen.fullScreen && Screen.currentResolution.height != 1080)
		{
			Screen.SetResolution(1920, 1080, true);
		}
		else if(!Screen.fullScreen && Screen.currentResolution.height != 540)
			Screen.SetResolution(960, 540, false);
    }
}
