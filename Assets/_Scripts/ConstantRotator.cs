using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotator : MonoBehaviour
{	
	public float x, y, z;
	
    void FixedUpdate()
	{
		transform.Rotate(x, y, z);
	}
}
