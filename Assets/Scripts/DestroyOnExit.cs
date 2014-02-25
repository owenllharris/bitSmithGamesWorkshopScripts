using UnityEngine;
using System.Collections;

public class DestroyOnExit : MonoBehaviour 
{
	/*void OnBecameInvisible() 
	{
		BroadcastMessage("Dead", SendMessageOptions.DontRequireReceiver);
	}*/
	bool wasVisible = false;
	void OnBecameVisible()
	{
		wasVisible = true;
	}
	
	void OnBecameInvisible()
	{
		if(wasVisible)
			Destroy(gameObject);
	}
}