using UnityEngine;
using System.Collections;
using System.Reflection;

public class Movement : MonoBehaviour 
{
	private MethodInfo movement;
	public enum Directions {Movement8Way, Movement4Way, MovementUpDown, MovementLeftRight, TankMovement};
	public Directions direction;

	private enum Facing {left, right, up, down, upleft, upright, downleft, downright, free};
	private Facing facingWhichWay;

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
			rigidbody2D.gravityScale = 0f;
		}
		else if(direction == Directions.MovementUpDown)
		{
			rigidbody2D.gravityScale = 0f;
		}
		else if(direction == Directions.TankMovement)
		{
			rigidbody2D.gravityScale = 0f;
			myTransform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
		}
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

	private void Movement8Way()
	{
		if(controlVector.x < 0 && controlVector.y > 0)
			facingWhichWay = Facing.upleft;
		else if(controlVector.y > 0 && controlVector.x > 0)
			facingWhichWay = Facing.upright;
		else if(controlVector.y < 0 && controlVector.x < 0)
			facingWhichWay = Facing.downleft;
		else if(controlVector.y < 0 && controlVector.x > 0)
			facingWhichWay = Facing.downright;
		// Calculate how fast we should be moving	
		else if(controlVector.x < 0)
			facingWhichWay = Facing.left;
		else if(controlVector.x > 0)
			facingWhichWay = Facing.right;
		else if(controlVector.y < 0)
			facingWhichWay = Facing.down;
		else if(controlVector.y > 0)
			facingWhichWay = Facing.up;



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
			facingWhichWay = Facing.left;
		else if(controlVector.x > 0)
			facingWhichWay = Facing.right;
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
		velocityChange.y = 0f;
		
		rigidbody2D.AddForce(velocityChange);
		if(grounded)
			rigidbody2D.velocity += velocityChange;
		else
			rigidbody2D.velocity += (velocityChange * 0.5f);
	}

	private void MovementUpDown()
	{
		if(controlVector.y < 0)
			facingWhichWay = Facing.down;
		else if(controlVector.y > 0)
			facingWhichWay = Facing.up;
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

		rigidbody2D.AddForce(velocityChange);
	}

	private void MovementLeftRight()
	{
		if(controlVector.x < 0)
			facingWhichWay = Facing.left;
		else if(controlVector.x > 0)
			facingWhichWay = Facing.right;

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

		rigidbody2D.AddForce(velocityChange);
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

		if(controlVector.x != 0)
		{
			myTransform.rotation = Quaternion.AngleAxis(-controlVector.x * turnSpeed * Time.deltaTime, myTransform.forward) * myTransform.rotation;
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
		}			
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
}