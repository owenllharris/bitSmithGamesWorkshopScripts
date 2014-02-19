using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour 
{
	private Transform myTransform;
	private Vector2	direction;

	public float speed, 
				damage, 
				gravityScale = 0.1f;

	public bool bounce,
				gravity;

	public GameObject explosion;

	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		if(!gravity)
			rigidbody2D.gravityScale = 0;
		else
			rigidbody2D.gravityScale = gravityScale;

		StartCoroutine(Timeout());
	}

	public void Fire(Vector2 direction)
	{
		if(audio.clip)
		{
			audio.Play();
		}
		rigidbody2D.AddForce(direction * speed);
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		collision.gameObject.BroadcastMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
		// create explosion if we have one
		if(explosion)
		{
			Instantiate(explosion, collision.contacts[0].point, Quaternion.identity); 
		}
		Destroy(this.gameObject);
	}

	IEnumerator Timeout()
	{
		yield return new WaitForSeconds(3f);
		Destroy(this.gameObject);
	}
}