using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void Entered (GameObject go) 
	{
		go.BroadcastMessage("Die");
	}
}
