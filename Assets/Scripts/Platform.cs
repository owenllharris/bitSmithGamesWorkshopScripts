using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour 
{
	public enum PlatformBehaviour { Solid, JumpThrough};
	public PlatformBehaviour platformBehaviour;

	private enum Where { Above, Below};
	private Where where;

	GameObject player;
	Transform myTransform;
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		player = GameObject.FindGameObjectWithTag("Player");
		if(platformBehaviour == PlatformBehaviour.JumpThrough)
		{
			StartCoroutine(CheckPlayer());
			this.gameObject.layer = LayerMask.NameToLayer("Platform");
			Physics2D.IgnoreLayerCollision(player.layer, LayerMask.NameToLayer("JumpThrough"), true);
			
		}
		else if(platformBehaviour == PlatformBehaviour.Solid)
		{
			this.gameObject.layer = LayerMask.NameToLayer("Solid");
			Physics2D.IgnoreLayerCollision(player.layer, this.gameObject.layer, false);
		}
	}
	
	// Update is called once per frame
	IEnumerator CheckPlayer() 
	{
		while(true)
		{
			if(player)
			{
				where = FindObjectRelative(player.transform.position);
				if(where == Where.Above)
				{
					//Physics2D.
					//Physics2D.IgnoreLayerCollision(player.layer, this.gameObject.layer, false);
					this.gameObject.layer = LayerMask.NameToLayer("Platform");
				}
				else if(where == Where.Below)
				{
					//Physics2D.IgnoreLayerCollision(player.layer, this.gameObject.layer, true);
					this.gameObject.layer = LayerMask.NameToLayer("JumpThrough");
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}

	// find player relative to the platform
	Where FindObjectRelative(Vector3 target)
	{
		Vector3 relativePosition = myTransform.InverseTransformPoint(target);

		// if the relative position is greater then  the top of the sprite renderer
		if (relativePosition.y > renderer.bounds.max.y - transform.position.y) 
		{		
			//The other object is above		
			return Where.Above;
		} 
		else 
		{		
			//The other object is below		
			return Where.Below;
		}
	}

	void OnBecameVisible() 
	{
		enabled = true;
	}
	// disable when we can't see it so were not always testing against the players position
	void OnBecameInvisible() 
	{
		enabled = false;
	}
}