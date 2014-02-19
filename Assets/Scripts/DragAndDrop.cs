using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour 
{
	public enum MoveAxis { X, Y, Both};
	public MoveAxis moveAixs;
	private bool isDragging = false;

	// Use this for initialization
	void Start () 
	{
		this.tag = "Moveable";
	}

	private void OnDrop()
	{

	}

	public void OnDragStart()
	{
		isDragging = true;
	}

	public void Drop()
	{
		isDragging = true;
		OnDrop ();
	}
}
