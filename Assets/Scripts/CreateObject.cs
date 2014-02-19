using UnityEngine;
using System.Collections;

public class CreateObject : MonoBehaviour 
{
	public GameObject objectToCreate;
	public bool createOnAwake = false;

	// Use this for initialization
	void Start () 
	{
		if(renderer)
			renderer.enabled = false;
		if(createOnAwake)
			Create();
	}

	public void Create()
	{
		Instantiate(objectToCreate, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}