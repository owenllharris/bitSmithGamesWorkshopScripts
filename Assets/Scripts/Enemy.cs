using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	private Movement movement;
	private Transform myTransform;

	public float distanceToSee = 10f, attackCooldown = 2f;

	private bool canFire = true;
	
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		movement = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(CanSeePlayer())
		{
			Fire();
		}
	}

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

	private bool CanSeePlayer()
	{
		//Debug.Log(movement.GetDirection().ToString());
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(myTransform.position.x , myTransform.position.y) + movement.GetDirection(), movement.GetDirection(), distanceToSee);
		if(hit != null && hit.transform != null)
		{
			if(hit.transform.tag == "Player")
			{
				return true;
			}
		}
		return false;
	}

	public void Die()
	{
		Invoke("DeathCountdown", 2f);
	}

	private void DeathCountdown()
	{
		Destroy(this.gameObject);
	}
}