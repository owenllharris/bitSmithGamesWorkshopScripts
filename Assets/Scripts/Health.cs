using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public int startHealth = 10;
	private int health = 0;

	public GameObject hurtParticle;
	// Use this for initialization
	void Start () 
	{
		health = startHealth;
	}
	
	public void Hit(int damage)
	{
		if(health > 0)
		{
			health -= damage;
			if(hurtParticle)
			{
				Instantiate(hurtParticle, this.transform.position, Quaternion.identity);
			}
			if(health <= 0)
			{
				BroadcastMessage("Dead");
			}
		}
	}
}