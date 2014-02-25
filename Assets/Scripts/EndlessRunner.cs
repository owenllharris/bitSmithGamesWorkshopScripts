using UnityEngine;
using System.Collections;

public class EndlessRunner : MonoBehaviour 
{
	public GameObject[] objectsToCreate;

	public float timeDifferenceBetweenObjects = 2f;

	// Use this for initialization
	void Start () 
	{
		if(renderer)
			renderer.enabled = false;
		Invoke("Create", timeDifferenceBetweenObjects);	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void Create()
	{
		float top = this.renderer.bounds.max.y;
		float bottom = this.renderer.bounds.min.y;
		GameObject go = (GameObject)Instantiate(objectsToCreate[Random.Range(0, objectsToCreate.Length)], 
		                                        new Vector3(renderer.bounds.center.x, Random.Range(bottom, top), 0f),
		                                        Quaternion.identity);
		Invoke("Create", timeDifferenceBetweenObjects);	
		
	}
}
