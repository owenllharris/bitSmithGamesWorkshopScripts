using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour 
{
	private Transform myTransform;
	public float spinSpeed = 10f;
	public enum SpinAxis { X, Y, Z};
	public SpinAxis spinAixs;
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 temp = Vector3.zero;
		if(spinAixs == SpinAxis.Z)
			temp = new Vector3(myTransform.rotation.eulerAngles.x, myTransform.rotation.eulerAngles.y ,myTransform.rotation.eulerAngles.z + (Time.deltaTime * spinSpeed));
		else if(spinAixs == SpinAxis.Y)
			temp = new Vector3(myTransform.rotation.eulerAngles.x, myTransform.rotation.eulerAngles.y + (Time.deltaTime * spinSpeed) ,myTransform.rotation.eulerAngles.z);
		else if(spinAixs == SpinAxis.X)
			temp = new Vector3(myTransform.rotation.eulerAngles.x + (Time.deltaTime * spinSpeed), myTransform.rotation.eulerAngles.y ,myTransform.rotation.eulerAngles.z);
			
		myTransform.eulerAngles = temp;
	}

	void OnBecameVisible() 
	{
		enabled = true;
	}
	
	void OnBecameInvisible() 
	{
		enabled = false;
	}
}