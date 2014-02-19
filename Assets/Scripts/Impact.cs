using UnityEngine;
using System.Collections;

public class Impact : MonoBehaviour 
{
	public float timeToDecay = 2f;

	// Use this for initialization
	void Start () 
	{
		if(audio.clip)
		{
			audio.Play();
		}
		StartCoroutine(Timeout());
	}

	IEnumerator Timeout()
	{
		yield return new WaitForSeconds(timeToDecay);
		Destroy(this.gameObject);
	}
}