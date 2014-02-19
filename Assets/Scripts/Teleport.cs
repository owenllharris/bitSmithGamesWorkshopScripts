using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
	public GameObject destination;

	// Use this for initialization
	void Start () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		DoTeleport(other.gameObject);
	}

	public void DoTeleport(GameObject go)
	{
		go.transform.position = destination.transform.position;
	}
}