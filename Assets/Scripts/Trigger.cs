using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour 
{
	public string messageToSend = "Entered";
	// Use this for initialization
	void Start () 
	{
		if(renderer)
			renderer.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Entered Trigger");
		BroadcastMessage(messageToSend, other.gameObject, SendMessageOptions.DontRequireReceiver);
	}
}
