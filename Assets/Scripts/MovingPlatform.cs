using UnityEngine;
using System.Collections;
using System.Reflection;

public class MovingPlatform : MonoBehaviour 
{
	private Transform myTransform;
	public enum MovingDirections {Left, Right, Up, Down, LeftRight, UpDown};
	public MovingDirections movingDirection;
	
	private enum State {MovingToStart, MovingToEnd};
	private State state;
	private MethodInfo mInfo;
	public float speed = 5f;
	private Vector2 moveTo;
	public GameObject start, 
				end;
	private Vector2 startPos, endPos;

	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		rigidbody2D.gravityScale = 0;

		if(movingDirection == MovingDirections.LeftRight || movingDirection == MovingDirections.UpDown)
		{
			if(!start)
			{
				Debug.LogError("No start positon for platform");
			}
			if(!end)
			{
				Debug.LogError("No end positon for platform");
			}
			if(start && end)
			{
				start.transform.parent  = null;
				end.transform.parent  = null;
				this.transform.position = start.transform.position;

				if(movingDirection == MovingDirections.LeftRight)
				{
					startPos = new Vector2(start.transform.position.x, myTransform.position.y);
					endPos = new Vector2(end.transform.position.x, myTransform.position.y);
				}
				else if(movingDirection == MovingDirections.UpDown)
				{
					startPos = new Vector2(myTransform.position.x, start.transform.position.y);
					endPos = new Vector2(myTransform.position.x, end.transform.position.y);
				}
				state = State.MovingToEnd;
				moveTo = endPos;
				mInfo = this.GetType().GetMethod(state.ToString(), (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
				Destroy(start);
				Destroy(end);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		switch(movingDirection)
		{
		case MovingDirections.Left:
			Move (-Vector2.right);
			break;

		case MovingDirections.Right:
			Move (Vector2.right);
			break;

		case MovingDirections.Down:
			Move (-Vector2.up);
			break;

		case MovingDirections.Up:
			Move (Vector2.up);
			break;

		case MovingDirections.LeftRight:
			mInfo.Invoke(this, null);
			break;

		case MovingDirections.UpDown:
			mInfo.Invoke(this, null);
			break;
		}
	}

	void Move(Vector2 controlVector)
	{
		myTransform.Translate(controlVector * speed * Time.deltaTime);
	}
	
	void MovingToStart()
	{
		Move ((moveTo - new Vector2(myTransform.position.x, myTransform.position.y)).normalized);
		if(Vector2.Distance(new Vector2(myTransform.position.x, myTransform.position.y), moveTo) < 0.2f)
		{
			moveTo = endPos;
			state = State.MovingToEnd;
			mInfo = this.GetType().GetMethod(state.ToString(), (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
		}
	}
	
	void MovingToEnd()
	{
		Move ((moveTo - new Vector2(myTransform.position.x, myTransform.position.y)).normalized);
		if(Vector2.Distance(new Vector2(myTransform.position.x, myTransform.position.y), moveTo) < 0.2f)
		{
			moveTo = startPos;
			state = State.MovingToStart;
			mInfo = this.GetType().GetMethod(state.ToString(), (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
		}
	}
}