using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public int startHealth = 10;
	private int health = 0;

	public GameObject hurtParticle, getHealthParticle;
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
				BroadcastMessage("Die");
			}
		}
	}

	public bool AddHealth(int addHealth)
	{
		if(health < startHealth)
		{
			health += addHealth;
			if(getHealthParticle)
			{
				Instantiate(getHealthParticle, this.transform.position, Quaternion.identity);
			}
			if(health > startHealth)
				health = startHealth;
			return true;
		}
		return false;
	}
}