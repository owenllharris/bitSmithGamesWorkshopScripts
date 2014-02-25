using UnityEngine;
using System.Collections;

public class TriggerHit : MonoBehaviour 
{
	public GameObject impactParticle;
	public int damage = 1;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(impactParticle)
			Instantiate(impactParticle, other.transform.position, Quaternion.identity);
		other.gameObject.BroadcastMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
	}
}
