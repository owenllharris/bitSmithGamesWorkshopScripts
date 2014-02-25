using UnityEngine;
using System.Collections;
using System.Reflection;

public class Movement : MonoBehaviour 
{
	private MethodInfo movement;
	public enum Directions {Movement8Way, Movement4Way, MovementUpDown, MovementLeftRight, TankMovement};
	public Directions direction;

	public enum Facing {left, right, up, down, upleft, upright, downleft, downright, free};
	private Facing facingWhichWay;
	
	public string jumpAnim = "";
	public string leftAnim = "";
	public string rightAnim = "";
	public string upAnim = "";
	public string downAnim = "";
	public string upLeftAnim = "";
	public string upRightAnim = "";
	public string downLeftAnim = "";
	public string downRightAnim = "";
	public string shootAnim = "";

	public Facing FacingWhichWay {
		get {
			return facingWhichWay;
		}
	}

	public Vector2 GetDirection()
	{
		switch(facingWhichWay)
		{
		case Facing.down:
			return -Vector2.up;
			break;
		case Facing.downleft:
			return new Vector2(-0.707f,-0.707f);
		break;
		case Facing.downright:
			return new Vector2(0.707f,-0.707f);
			break;
		case Facing.up:
			return Vector2.up;
			break;
		case Facing.upleft:
			return new Vector2(-0.707f,0.707f);
			break;
		case Facing.upright:
			return new Vector2(0.707f,0.707f);
			break;
		case Facing.left:
			return -Vector2.right;
			break;
		case Facing.right:
			return Vector2.right;
			break;
		case Facing.free:
			return myTransform.up;
			break;
		}
		return myTransform.up;
	}

	private Transform myTransform;
	private Animator animator;
	private Vector2 targetVelocity, 
				controlVector, 
				velocityChange;

	private float maxVelocityChange = 10f;
	private bool grounded = false;
	
	public float speed = 3f, 
				jumpHeight = 0.75f,
				turnSpeed = 3f;
	
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		ChangeMovement(direction.ToString());
		if(direction == Directions.Movement8Way)
		{
			rigidbody2D.gravityScale = 0f;
		}
		else if(direction == Directions.Movement4Way)
		{
			StartCoroutine(CheckGrounded());
		}
		else if(direction == Directions.MovementLeftRight)
		{
			//rigidbody2D.gravityScale = 0f;
		}
		else if(direction == Directions.MovementUpDown)
		{
			//rigidbody2D.gravityScale = 0f;
			StartCoroutine(CheckGrounded());
		}
		else if(direction == Directions.TankMovement)
		{
			rigidbody2D.gravityScale = 0f;
			myTransform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
		}
		animator = GetComponent<Animator>();
		if(!animator)
			GetComponentInChildren<Animator>();
	}

	public void MoveTowards(Vector2 vector)
	{
		controlVector = (vector - new Vector2(myTransform.position.x, myTransform.position.y)).normalized;
		movement.Invoke(this, null);
	}

	public void Move(Vector2 vector)
	{
		controlVector = vector;
		movement.Invoke(this, null);
	}

	public void Stop()
	{
		rigidbody2D.velocity = Vector2.zero;
	}

	void PlayAnimation(string anim)
	{
		if(anim != "")
		{
			animator.Play(anim);
			//Debug.Log(anim);
		}
	}

	private void Movement8Way()
	{
		if(controlVector.x < 0 && controlVector.y > 0)
		{
			facingWhichWay = Facing.upleft;
			PlayAnimation(upLeftAnim);
		}
		else if(controlVector.y > 0 && controlVector.x > 0)
		{
			facingWhichWay = Facing.upright;
			PlayAnimation(upRightAnim);
		}
		else if(controlVector.y < 0 && controlVector.x < 0)
		{
			facingWhichWay = Facing.downleft;
			PlayAnimation(downLeftAnim);
		}
		else if(controlVector.y < 0 && controlVector.x > 0)
		{
			facingWhichWay = Facing.downright;
			PlayAnimation(downRightAnim);
		}
		else if(controlVector.x < 0)
		{
			facingWhichWay = Facing.left;
			PlayAnimation(leftAnim);
		}
		else if(controlVector.x > 0)
		{
			facingWhichWay = Facing.right;
			PlayAnimation(rightAnim);
		}
		else if(controlVector.y < 0)
		{
			facingWhichWay = Facing.down;
			PlayAnimation(downAnim);
		}
		else if(controlVector.y > 0)
		{
			facingWhichWay = Facing.up;
			PlayAnimation(upAnim);
		}

		targetVelocity = controlVector;
		if(targetVelocity.magnitude > 1)
		{
			targetVelocity = targetVelocity.normalized;
		}	
		targetVelocity *= speed;
		velocityChange = (targetVelocity - rigidbody2D.velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);

		rigidbody2D.velocity += velocityChange;
		//rigidbody2D.AddForce(velocityChange);
	}

	private void Movement4Way()
	{
		// Calculate how fast we should be moving	
		if(controlVector.x < 0)
		{
			facingWhichWay = Facing.left;
			if(grounded)
				PlayAnimation(leftAnim);
		}
		else if(controlVector.x > 0)
		{
			facingWhichWay = Facing.right;
			if(grounded)
				PlayAnimation(rightAnim);
		}
		/*else if(controlVector.y < 0)
			facingWhichWay = Facing.down;
		else if(controlVector.y > 0)
			facingWhichWay = Facing.up;*/

		targetVelocity = controlVector;
		if(targetVelocity.magnitude > 1)
		{
			targetVelocity = targetVelocity.normalized;
		}	
		targetVelocity *= speed;
		velocityChange = (targetVelocity - rigidbody2D.velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		if(onLadder)
			velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
		else
			velocityChange.y = 0f;
		
		rigidbody2D.AddForce(velocityChange);
		if(grounded)
			rigidbody2D.velocity += velocityChange;
		else
			rigidbody2D.velocity += (velocityChange * 0.75f);
	}

	private void MovementUpDown()
	{
		if(controlVector.y < 0)
		{
			facingWhichWay = Facing.down;
			//if(grounded && rigidbody2D.gravityScale > 0)
				PlayAnimation(downAnim);
		}
		else if(controlVector.y > 0)
		{
			facingWhichWay = Facing.up;
			//if(grounded && rigidbody2D.gravityScale > 0)
				PlayAnimation(upAnim);
		}
		// Calculate how fast we should be moving	
		targetVelocity = controlVector;
		if(targetVelocity.magnitude > 1)
		{
			targetVelocity = targetVelocity.normalized;
		}	
		targetVelocity *= speed;
		velocityChange = (targetVelocity - rigidbody2D.velocity);
		velocityChange.x = 0f;
		velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);

		//rigidbody2D.AddForce(velocityChange);
		rigidbody2D.velocity += velocityChange;
	}

	private void MovementLeftRight()
	{
		if(controlVector.x < 0)
		{
			facingWhichWay = Facing.left;
			PlayAnimation(leftAnim);
		}
		else if(controlVector.x > 0)
		{
			facingWhichWay = Facing.right;
			PlayAnimation(rightAnim);
		}

		// Calculate how fast we should be moving	
		targetVelocity = controlVector;
		if(targetVelocity.magnitude > 1)
		{
			targetVelocity = targetVelocity.normalized;
		}	
		targetVelocity *= speed;
		velocityChange = (targetVelocity - rigidbody2D.velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;

		//rigidbody2D.AddForce(velocityChange);
		rigidbody2D.velocity += velocityChange;
	}

	private void TankMovement()
	{
		facingWhichWay = Facing.free;
		// Calculate how fast we should be moving
		targetVelocity = myTransform.up * controlVector.y;
		if(targetVelocity.magnitude > 1)
		{
			targetVelocity = targetVelocity.normalized;
		}	
		targetVelocity *= speed;
		velocityChange = (targetVelocity - rigidbody2D.velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
		rigidbody2D.velocity += velocityChange;

		if(this.tag == "Player")
		{
			if(controlVector.x != 0)
			{
				myTransform.rotation = Quaternion.AngleAxis(-controlVector.x * turnSpeed * Time.deltaTime, myTransform.forward) * myTransform.rotation;
			}
		}
		else if(this.tag == "Enemy")
		{
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation( controlVector ), Time.deltaTime * turnSpeed );
		}
	}

	// set the behaviour and find the method for it
	private void ChangeMovement(string theMovement)
	{
		movement = this.GetType().GetMethod(theMovement, (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
		
		if(movement == null)
		{
			Debug.Log("MethodInfo null on " + this.name + " no method called " + theMovement);
		}
	}

	public void Jump()
	{
		if(grounded && direction == Directions.Movement4Way)  
		{	
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, CalculateJumpVerticalSpeed());
			PlayAnimation(jumpAnim);
		}
		if(grounded && direction == Directions.MovementUpDown)  
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, CalculateJumpVerticalSpeed());
			PlayAnimation(jumpAnim);
		}
	}

	public void Shoot()
	{
		PlayAnimation(shootAnim);
	}
	
	private float CalculateJumpVerticalSpeed ()
	{
		return Mathf.Sqrt(2f * jumpHeight * (Physics2D.gravity.y * -rigidbody2D.gravityScale));
	}
	
	private IEnumerator CheckGrounded()
	{		
		while(true)
		{
			Vector2 start = new Vector3(myTransform.position.x, myTransform.position.y - gameObject.renderer.bounds.extents.y - 0.005f);

			if(Physics2D.Raycast(start, -Vector2.up, 0.1f))
			{
				grounded = true;	
			}
			else
			{
				grounded = false;
			}
			yield return new WaitForFixedUpdate();
		}
	}

	bool onLadder = false;
	float grav;
	public void OnLadderEnter()
	{
		onLadder = true;
		grav = rigidbody2D.gravityScale;
		rigidbody2D.gravityScale = 0;
	}

	public void OnLadderExit()
	{
		onLadder = false;
		rigidbody2D.gravityScale = grav;
	}
}