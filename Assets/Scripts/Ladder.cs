﻿using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		other.BroadcastMessage("OnLadderEnter", SendMessageOptions.DontRequireReceiver);
	}
	void OnTriggerExit2D(Collider2D other)
	{
		other.BroadcastMessage("OnLadderExit", SendMessageOptions.DontRequireReceiver);
	}
}
