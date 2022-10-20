using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantScaler : MonoBehaviour
{
	public float x;
	public float y;
	public float z;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.localScale = new Vector3
		(
			transform.localScale.x + x,
			transform.localScale.y + y,
			transform.localScale.z + z
		);
    }
}
