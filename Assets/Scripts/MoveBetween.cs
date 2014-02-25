using UnityEngine;
using System.Collections;
using System.Reflection;

[RequireComponent(typeof(Movement))]
public class MoveBetween : MonoBehaviour 
{
	private Transform myTransform;
	private enum State {MovingToStart, MovingToEnd, AtStart, AtEnd};
	private State state;

	private MethodInfo mInfo;

	public GameObject start, 
					end;
	private Vector2 moveTo;

	public bool moveBackAndForth;
	private Vector2 startPos, endPos;
	private Movement movement;

	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		state = State.AtStart;

		start.transform.parent = null;
		end.transform.parent  = null;
		this.transform.position = start.transform.position;
		startPos = new Vector2(start.transform.position.x, myTransform.position.y);
		endPos = new Vector2(end.transform.position.x, myTransform.position.y);

		moveTo = startPos;

		movement = GetComponent<Movement>();
		ChangeMethodInfo(state.ToString());

		Destroy(start);
		Destroy(end);
	}
	
	// Update is called once per frame
	void Update () 
	{
		mInfo.Invoke(this, null);
	}

	void AtStart()
	{
		moveTo = endPos;
		state = State.MovingToEnd;
		ChangeMethodInfo(state.ToString());
	}

	void MovingToStart()
	{
		movement.MoveTowards(startPos);
		if(Vector2.Distance(new Vector2(myTransform.position.x, myTransform.position.y), moveTo) < 0.2f)
		{
			state = State.AtStart;
			ChangeMethodInfo(state.ToString());
		}
	}

	void AtEnd()
	{
		moveTo = startPos;
		state = State.MovingToStart;
		ChangeMethodInfo(state.ToString());
	}

	void MovingToEnd()
	{
		movement.MoveTowards(endPos);
		if(Vector2.Distance(new Vector2(myTransform.position.x, myTransform.position.y), moveTo) < 0.2f)
		{
			state = State.AtEnd;
			ChangeMethodInfo(state.ToString());
		}
	}

	// set the behaviour and find the method for it
	private void ChangeMethodInfo(string theMovement)
	{
		mInfo = this.GetType().GetMethod(theMovement, (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
		
		if(mInfo == null)
		{
			Debug.Log("MethodInfo null on " + this.name + " no method called " + mInfo);
		}
	}
}