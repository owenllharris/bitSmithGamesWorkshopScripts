using UnityEngine;
using System.Collections;
using System.Reflection;

public class CameraFollow : MonoBehaviour 
{
	public enum CameraType{Free, FixedHeight, FixedWidth, Fixed};
	public CameraType cameraType;
	private MethodInfo methodInfo;
	public GameObject targetToFollow;
	public float smoothTime = 0.3f;
	public Vector3 cameraOffset = new Vector3(0,0,-10f);
	private Transform myTransform;
	private Vector3 velocity = Vector3.zero;

	Vector3 defaultPos = Vector3.zero;
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		if(targetToFollow == null)
		{
			Debug.LogError("Didn't set targetToFollow in Editor");
		}
		ChangeMethodInfo(cameraType.ToString());

		defaultPos = myTransform.position;
	}

	// set the behaviour and find the method for it
	private void ChangeMethodInfo(string theMethodInfo)
	{
		methodInfo = this.GetType().GetMethod(theMethodInfo, (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
		
		if(methodInfo == null)
		{
			Debug.Log("MethodInfo null on " + this.name + " no method called " + theMethodInfo);
		}
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		methodInfo.Invoke(this, null);

	}

	void Free()
	{
		if(targetToFollow)
		{
			//Vector3 newPos = new Vector3(targetToFollow.transform.position.x + , targetToFollow.transform.position.y, targetToFollow.transform.position.z + cameraOffset.z);
			Vector3 newPos = targetToFollow.transform.position + cameraOffset;
			myTransform.position = Vector3.SmoothDamp(myTransform.position, newPos , ref velocity, smoothTime);
		}
	}
	void FixedHeight()
	{
		if(targetToFollow)
		{
			//Vector3 newPos = new Vector3(targetToFollow.transform.position.x + , targetToFollow.transform.position.y, targetToFollow.transform.position.z + cameraOffset.z);
			Vector3 newPos = new Vector3(targetToFollow.transform.position.x + cameraOffset.x,defaultPos.y, targetToFollow.transform.position.z + cameraOffset.z) ;
			myTransform.position = Vector3.SmoothDamp(myTransform.position, newPos , ref velocity, smoothTime);
		}
	}
	void FixedWidth()
	{
		if(targetToFollow)
		{
			//Vector3 newPos = new Vector3(targetToFollow.transform.position.x + , targetToFollow.transform.position.y, targetToFollow.transform.position.z + cameraOffset.z);
			//Vector3 newPos = targetToFollow.transform.position + cameraOffset;
			Vector3 newPos = new Vector3(defaultPos.x, targetToFollow.transform.position.y + cameraOffset.y, targetToFollow.transform.position.z + cameraOffset.z) ;
			myTransform.position = Vector3.SmoothDamp(myTransform.position, newPos , ref velocity, smoothTime);
		}
	}
	void Fixed()
	{
		if(targetToFollow)
		{

		}
	}
}