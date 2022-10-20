using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooMovement : MonoBehaviour
{
	public InputMap input;
	public float racketSpeed;
	float speed;
	public Rigidbody2D rb;
	public Animator anim;
	public ParticleSystem ringPS;
	public AudioSource audio;
	
	public Transform shieldSize;
	
	Vector2 movement;
	
    // Start is called before the first frame update
    void Start()
    {
		if(PongRunner.instance.file.type == GameType.DOUBLES)
		{
			shieldSize.localScale = new Vector2(1, 0.75f);
			GetComponentInChildren<CapsuleCollider2D>().size = new Vector2(1f, 5f);
			racketSpeed = 7.0f;
		}
		else // SINGLES
		{
			shieldSize.localScale = new Vector2(1, 1f);
			GetComponentInChildren<CapsuleCollider2D>().size = new Vector2(1f, 6.5f);
			racketSpeed = 8.5f;
		}
        speed = racketSpeed;
    }

    // Update is called once per frame
    void Update()
    {	
        movement.y = Input.GetAxisRaw(input.Vertical);
		
		if(!input.CPU && Input.GetButtonDown(input.A))
		{
			speed = racketSpeed * 0.5f;
			anim.Play("Pull", 0, 0f);
		}
		
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Pulling") && !Input.GetButton(input.A))
		{
			Swing();
			
		}
		
		transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -6.375f, 6.375f));
    }
	
	void FixedUpdate()
	{
		if(input.CPU)
		{
			if(PongRunner.instance.ball != null)
			{
				if(PongRunner.instance.ball.transform.position.y > transform.position.y + 1f)
					movement.y = 1;
				else if(PongRunner.instance.ball.transform.position.y < transform.position.y - 1f)
					movement.y = -1;
				else movement.y = 0;
				
				if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
					if(Mathf.Abs(transform.position.x - PongRunner.instance.ball.transform.position.x) < 4f)
						if(Mathf.Abs(PongRunner.instance.ball.transform.position.y - transform.position.y) < 2.5f)
							anim.Play("Pull", 0, 0f);
			}
			else movement.y = 0;
		}
		
		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Push"))
		{
			rb.drag = 0;
			rb.velocity = new Vector2(0, movement.y * speed);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.transform.tag == "Ball")
		{
			collider.GetComponent<Ball>().rb.velocity = (collider.transform.position - transform.position).normalized * collider.GetComponent<Ball>().speed;
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Push") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0f)
				collider.GetComponent<Ball>().Knock(transform.localScale.x, rb.velocity.y * 0.09f);
			else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Pulling") && !Input.GetButton(input.A))
				collider.GetComponent<Ball>().Knock(transform.localScale.x, rb.velocity.y * 0.09f);
			else collider.GetComponent<Ball>().Bounce();
		}
	}
	
	void Swing()
	{
		if(input.CPU && PongRunner.instance.ball != null)
		{
			if(PongRunner.instance.ball.transform.position.y - transform.position.y < -1.25f)
				movement.y = -1;
			else if(PongRunner.instance.ball.transform.position.y - transform.position.y > 1.25f)
				movement.y = 1;
		}
		ringPS.Play();
		audio.Play();
		speed = racketSpeed;
		anim.Play("Push", 0, 0f);
		rb.drag = 6;
		rb.velocity = new Vector2(0, movement.y * 32f);
	}
}
