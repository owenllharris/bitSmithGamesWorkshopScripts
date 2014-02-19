using UnityEngine;
using System.Collections;

public class DestroyOnExit : MonoBehaviour 
{
	void OnBecameInvisible() 
	{
		BroadcastMessage("Dead", SendMessageOptions.DontRequireReceiver);
	}
}