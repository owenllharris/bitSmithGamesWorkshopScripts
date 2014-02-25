using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform mainCamera;

	public float xoffSet;
	public float yOffset;
    public float Speed;
  

	private float myZ;
	
	private Transform myTransform;

	void Awake()
	{
		myTransform = this.transform;
   		myZ = transform.position.z;
		mainCamera = Camera.mainCamera.transform;
	}
       
       // Update is called once per frame
    void Update () 
	{
        myTransform.position = new Vector3((mainCamera.position.x) * xoffSet, ((mainCamera.position.y) * yOffset), myZ);
	}
}