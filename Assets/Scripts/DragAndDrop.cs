using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour 
{
	public enum MoveAxis { X, Y, Both};
	public MoveAxis moveAixs;
	private bool isDragging = false;
	private Transform myTransform;

	private Vector2 lastDir = Vector2.zero;
	public float force = 100f;

	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		this.tag = "Moveable";
	}

	private void OnDrop()
	{

	}

	public void OnDragStart()
	{
		isDragging = true;
	}

	public void Move(Vector3 pos)
	{
		lastDir = (transform.position - pos).normalized;
		myTransform.position  = new Vector3(pos.x, pos.y, 0f);
	}

	public void Drop()
	{
		rigidbody2D.AddForce(lastDir * force);
		isDragging = true;
		OnDrop ();
	}
}
