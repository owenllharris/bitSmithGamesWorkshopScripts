using UnityEngine;
using System.Collections;
using System.Reflection;

public class EnemyTank : MonoBehaviour {

	private Transform myTransform;
	private enum State {Idle, Aggro};
	private State state;
	
	private MethodInfo mInfo;	
	private Movement movement;
	public float aggroDistance = 5f, attackCooldown = 0.75f;
	GameObject player;
	
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		player = GameObject.FindGameObjectWithTag("Player");
		state = State.Idle;
		movement = GetComponent<Movement>();
		ChangeMethodInfo(state.ToString());
	}
	
	// Update is called once per frame
	void Update () 
	{
		mInfo.Invoke(this, null);
	}
	
	void Idle()
	{
		if(Vector3.Distance(player.transform.position, myTransform.position) < aggroDistance)
		{
			state = State.Aggro;
			ChangeMethodInfo(state.ToString());
		}
	}

	void Aggro()
	{
		//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(),Time.deltaTime * tur;


		movement.MoveTowards(player.transform.position);
		Fire();
		if(Vector3.Distance(player.transform.position, myTransform.position) > aggroDistance + 1f)
		{
			state = State.Idle;
			ChangeMethodInfo(state.ToString());
			movement.Stop();
		}
	}
	
	// set the behaviour and find the method for it
	private void ChangeMethodInfo(string theState)
	{
		mInfo = this.GetType().GetMethod(theState, (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
		
		if(mInfo == null)
		{
			Debug.Log("MethodInfo null on " + this.name + " no method called " + mInfo);
		}
	}
	private bool canFire = true;
	private void Fire()
	{
		if(canFire)
		{
			canFire = false;
			Invoke("AttackCooldown", attackCooldown);
			BroadcastMessage("Shoot", movement.GetDirection());
		}
	}
	
	private void AttackCooldown()
	{
		canFire = true;
	}
}
