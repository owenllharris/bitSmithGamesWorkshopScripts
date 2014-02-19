using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	bool HasLineOfSight(GameObject from, GameObject to)
	{
		RaycastHit2D hit = Physics2D.Raycast(from.transform.position, (to.transform.position - from.transform.position).normalized, 10f);
		if(hit != null)
		{
			if(hit.transform.name == to.transform.name)
			{
				return true;
			}
		}
		return false;
	}
}