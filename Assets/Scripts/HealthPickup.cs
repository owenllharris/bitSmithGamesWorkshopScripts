using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour 
{
	public int health = 5;
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && other.gameObject.GetComponent<Health>().AddHealth(health))
		{
			Destroy(gameObject);
		}
	}
}