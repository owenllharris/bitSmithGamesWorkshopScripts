using UnityEngine;
using System.Collections;

public class ColliderHit : MonoBehaviour 
{
	public GameObject impactParticle;
	public int damage = 1;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(impactParticle)
			Instantiate(impactParticle, collision.contacts[0].point,Quaternion.identity);
		collision.gameObject.BroadcastMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
	}
}
