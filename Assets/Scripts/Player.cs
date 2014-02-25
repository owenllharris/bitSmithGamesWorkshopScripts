using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	private Transform myTransform;
	private Movement movement;
	private Animator animator;
	private GameObject draggingObject;

	public string jumpAnim = "";
	public string leftAnim = "";
	public string rightAnim = "";
	public string upAnim = "";
	public string downAnim = "";
	public string upLeftAnim = "";
	public string upRightAnim = "";
	public string downLeftAnim = "";
	public string downRightAnim = "";


	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		movement = GetComponent<Movement>();
		animator = GetComponent<Animator>();
		if(!animator)
			GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Controls();
	}

	void PlayAnimation(string anim)
	{
		//movement.FacingWhichWay;
		animator.Play(anim);
	}

	void Controls()
	{
		float y = Input.GetAxis("Vertical");
		float x = Input.GetAxis("Horizontal");
		Vector2 control = new Vector2(x,y);

		movement.Move(control);

		if(Input.GetButtonDown("Jump"))
		{
			movement.Jump();
		}

		if(Input.GetButtonDown("Fire1"))
		{
			BroadcastMessage("Shoot", movement.GetDirection(), SendMessageOptions.DontRequireReceiver);
		}
		if(Input.GetButtonDown("Fire2"))
		{
			Ray ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);
			Vector3 pos = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
			//RaycastHit hit;
			
			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.1f);	
			if(hit != null && hit.transform)
			{
				Debug.Log(hit.transform.name);
				if(hit.transform.tag == "Moveable")
				{
					hit.transform.BroadcastMessage("OnDragStart");

					draggingObject = hit.transform.gameObject;
					draggingObject.transform.position = hit.transform.position;
				}
			}
		}
		if(draggingObject && Input.GetButton("Fire2"))
		{
			Vector3 pos = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
			draggingObject.BroadcastMessage("Move", pos);
			//draggingObject.transform.position = new Vector3(pos.x, pos.y, 0f);
		}
		if(draggingObject && Input.GetButtonUp("Fire2"))
		{
			draggingObject.BroadcastMessage("Drop");
			draggingObject = null;
		}
	}

	public void Die()
	{
		Debug.Log("Do death animation");
		Invoke("DeathCountdown", 2f);
	}

	private void DeathCountdown()
	{
		Destroy(this.gameObject);
	}
}