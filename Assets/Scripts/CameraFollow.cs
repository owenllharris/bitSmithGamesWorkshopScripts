using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public GameObject targetToFollow;
	public float smoothTime = 0.3f;
	public Vector3 cameraOffset = new Vector3(0,0,-10f);
	private Transform myTransform;
	private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		if(targetToFollow == null)
		{
			Debug.LogError("Didn't set targetToFollow in Editor");
		}
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(targetToFollow)
		{
			//Vector3 newPos = new Vector3(targetToFollow.transform.position.x + , targetToFollow.transform.position.y, targetToFollow.transform.position.z + cameraOffset.z);
			Vector3 newPos = targetToFollow.transform.position + cameraOffset;
			myTransform.position = Vector3.SmoothDamp(myTransform.position, newPos , ref velocity, smoothTime);
		}
	}
}