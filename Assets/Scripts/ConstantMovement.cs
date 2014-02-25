using UnityEngine;
using System.Collections;
using System.Reflection;

public class ConstantMovement : MonoBehaviour 
{
	public enum ConstantMovementDirection {None, Right, Left, Up, Down};
	public ConstantMovementDirection constantMovementDirection;
	private MethodInfo methodInfo;

	public float speed = 4f;
	// Use this for initialization
	void Start () 
	{
		methodInfo = this.GetType().GetMethod(constantMovementDirection.ToString(), (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		methodInfo.Invoke(this, null);
	}

	void None()
	{

	}
	void Right()
	{
		// Calculate how fast we should be moving	
		Vector2 targetVelocity = Vector2.right;
		targetVelocity *= speed;
		Vector2 velocityChange = (targetVelocity - rigidbody2D.velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -10f, 10f);
		velocityChange.y = 0;

		rigidbody2D.velocity += velocityChange;
	}
	void Left()
	{
		Vector2 targetVelocity = -Vector2.right;
		targetVelocity *= speed;
		Vector2 velocityChange = (targetVelocity - rigidbody2D.velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -10f, 10f);
		velocityChange.y = 0;
		
		rigidbody2D.velocity += velocityChange;
	}
	void Up()
	{
		Vector2 targetVelocity = Vector2.up;	
		targetVelocity *= speed;
		Vector2 velocityChange = (targetVelocity - rigidbody2D.velocity);
		velocityChange.x = 0;
		velocityChange.y = Mathf.Clamp(velocityChange.y, -10f, 10f);
		
		rigidbody2D.velocity += velocityChange;
	}
	void Down()
	{
		Vector2 targetVelocity = -Vector2.up;
		targetVelocity *= speed;
		Vector2 velocityChange = (targetVelocity - rigidbody2D.velocity);
		velocityChange.x = 0;
		velocityChange.y = Mathf.Clamp(velocityChange.y, -10f, 10f);
		
		rigidbody2D.velocity += velocityChange;
	}
}
