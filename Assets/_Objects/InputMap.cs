using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Input", menuName = "Input")]
public class InputMap : ScriptableObject
{
	public string Horizontal;
	public string Vertical;
	public string A;
	public string B;
	public string Start;
	
	public bool CPU;
}
