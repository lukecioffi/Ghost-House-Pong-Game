using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType { SINGLES, DOUBLES }

[CreateAssetMenu(fileName = "New Game File", menuName = "Game")]
public class GameFile : ScriptableObject
{
	public int scoreToWin;
	public int mustWinBy = 1;
	public bool hazardsON = true;
	
	public GameType type = GameType.SINGLES;
	public InputMap[] inputs;
}
