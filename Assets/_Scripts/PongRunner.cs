using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class PongRunner : MonoBehaviour
{
	public GameFile file;
	
	public static PongRunner instance;
	public Rigidbody2D servingRB;
	public Ball ball;
	public BooMovement[] boos;
	public GameObject ballPrefab;
	
	public AudioSource audio;
	public AudioClip enterSFX;
	public AudioSource applaudio;
	public Transform scoreboard;
	public TextMeshPro scoreText;
	
	public MusicPlaylist playlist;
	public MusicSet victoryMusic;
	
	public PlayAgain paBox;
	public PlayAgain pauseBox;
	public Animator curtain;
	
	bool handsUp = false;
	float t, t2;
	
	public int[] scores = {0, 0};
	
	public WindTunnel tunnel;
	public UnityEvent OnNewServeEvent;
	
	void Awake()
	{
		instance = this;
	}
	
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }
	
	void Update()
	{
		if(ball != null && ball.rb.velocity.magnitude > 1 && (Input.GetButtonDown("Start (P1)") || Input.GetButtonDown("Start (P2)")))
		{
			if(Physics2D.autoSimulation)
			{
				Physics2D.autoSimulation = false;
				pauseBox.enabled = true;
				t2 = 0;
				return;
			}
			else
			{
				pauseBox.enabled = false;
				Physics2D.autoSimulation = true;
				t2 = 0;
				return;
			}
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(audio.isPlaying)
		{
			audio.pitch = servingRB.angularVelocity * 0.001f + 0.2f;
			if(audio.pitch < 0.25f) audio.pitch = 0.25f;
		}
		
		if(handsUp)
		{
			applaudio.transform.position = Vector2.Lerp(applaudio.transform.position, new Vector2(-8.75f, -6.5f), t);
			scoreboard.position = Vector2.Lerp(scoreboard.position, new Vector2(0, 6.5f), t);
		}
		else 
		{
			applaudio.transform.position = Vector2.Lerp(applaudio.transform.position, new Vector2(-8.75f, -10f), t);
			scoreboard.position = Vector2.Lerp(scoreboard.position, new Vector2(0, 10f), t);
		}
		
		if(paBox.enabled)
		{
			paBox.transform.position = Vector2.Lerp(paBox.transform.position, new Vector2(0, 1f), t2);
		}
		else
		{
			paBox.transform.position = Vector2.Lerp(paBox.transform.position, new Vector2(0, 12f), t2);	
		}
		
		if(pauseBox.enabled)
		{
			pauseBox.transform.position = Vector2.Lerp(pauseBox.transform.position, new Vector2(0, 1f), t2);
		}
		else
		{
			pauseBox.transform.position = Vector2.Lerp(pauseBox.transform.position, new Vector2(0, 12f), t2);	
		}
		
		t += 0.01f;
		t2 += 0.01f;
    }
	
	public void Score(int id)
	{
		scores[id]++;
		StartCoroutine(ScorePoint());
	}
	
	IEnumerator StartGame()
	{
		if(!file.hazardsON)
		{
			GameObject[] allHazards = GameObject.FindGameObjectsWithTag("Hazard");
			foreach(GameObject g in allHazards)
				g.SetActive(false);
		}
		else
		{
			GameObject[] allCovers = GameObject.FindGameObjectsWithTag("Hazard Cover");
			foreach(GameObject g in allCovers)
				g.SetActive(false);
		}
		
		if(file.type == GameType.SINGLES)
		{
			boos[2].gameObject.SetActive(false);
			boos[3].gameObject.SetActive(false);
		}
		
		scoreText.SetText(scores[0] + " - " + scores[1] + "\nGame to " + file.scoreToWin + " - Win by " + file.mustWinBy);
		
		yield return new WaitForSeconds(2.0f);
		
		StartCoroutine(Serve());
	}
	
	IEnumerator Serve()
	{
		OnNewServeEvent.Invoke();
		if(tunnel != null) tunnel.Flip();
		ball = Instantiate(ballPrefab, transform).GetComponent<Ball>();
		
		yield return new WaitForSeconds(0.05f);
		servingRB.gameObject.SetActive(true);
		servingRB.angularVelocity = 2160.0f + Random.Range(-720, 720);
		audio.Play();
		yield return new WaitUntil(() => servingRB.angularVelocity < 10f);
		audio.Stop();
		yield return new WaitUntil(() => servingRB.angularVelocity < 1f);
		servingRB.angularVelocity = 0f;
		yield return new WaitForSeconds(0.5f);
		
		ball.audio[0].clip = ball.hitSFX;
		ball.audio[0].Play();
		
		ball.rb.velocity = servingRB.transform.up * ball.speed;
		
		servingRB.gameObject.SetActive(false);
	}
	
	IEnumerator ScorePoint()
	{
		ball = null;
		Physics2D.autoSimulation = true;
		pauseBox.enabled = false;
		
		scoreText.SetText(scores[0] + " - " + scores[1] + "\nGame to " + file.scoreToWin + " - Win by " + file.mustWinBy);
		yield return new WaitForSeconds(0.2f);
		applaudio.Play();
		handsUp = true;
		t = 0;
		
		if((scores[0] >= file.scoreToWin && scores[0] - scores[1] >= file.mustWinBy) || (scores[1] >= file.scoreToWin && scores[1] - scores[0] >= file.mustWinBy))
		{
			StartCoroutine(Win());
			yield break;
		}
		
		yield return new WaitUntil(() => !applaudio.isPlaying);
		yield return new WaitForSeconds(1.0f);
		
		handsUp = false;
		t = 0;
		
		StartCoroutine(Serve());
	}
	
	IEnumerator Win()
	{
		playlist.Stop();
		victoryMusic.Play();
		foreach(BooMovement b in boos)
		{
			b.enabled = false;
			b.rb.velocity = Vector2.zero;
		}
		
		if(scores[0] >= file.scoreToWin)
		{
			if(file.type == GameType.SINGLES) 
				scoreText.SetText(scores[0] + " - " + scores[1] + "\nPlayer 1 wins!");
			else
				scoreText.SetText(scores[0] + " - " + scores[1] + "\nBlue Team wins!");
			scoreText.color = new Color(0.5f, 1f, 1f, 1f);
		}
		
		if(scores[1] >= file.scoreToWin)
		{
			if(file.type == GameType.SINGLES) 
				scoreText.SetText(scores[0] + " - " + scores[1] + "\nPlayer 2 wins!");
			else
				scoreText.SetText(scores[0] + " - " + scores[1] + "\nRed Team wins!");
			scoreText.color = new Color(1, 0.5f, 0.5f, 1f);
		}
		
		yield return new WaitForSeconds(1.0f);
		paBox.enabled = true;
		t2 = 0;
	}
	
	IEnumerator QuitGame(int next)
	{
		applaudio.PlayOneShot(enterSFX);
		curtain.Play("CurtainComeDown", 0, 0f);
		yield return new WaitForSeconds(1.0f);
		Physics2D.autoSimulation = true;
		if(next == 0)
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		else if(next == 1)
			SceneManager.LoadScene("SelectCourt");
		else if(next == 2)
			SceneManager.LoadScene("Title");
	}
	
	public void Quit(int next)
	{
		StartCoroutine(QuitGame(next));
	}
	
	public void Pause(int next)
	{
		if(next == 0) // RESUME
		{
			Physics2D.autoSimulation = true;
			pauseBox.enabled = false;
		}
		else StartCoroutine(QuitGame(next));
	}
}
