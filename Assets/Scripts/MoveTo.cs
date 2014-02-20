using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

	private NavMeshAgent nma;

	public GameObject Target;
	public float Speed = 1f;

	public bool MoveToClick;

	// Use this for initialization
	void Start () 
	{
		nma = gameObject.AddComponent<NavMeshAgent>();
		nma.speed = Speed;

		if ( Target != null )
			nma.destination = Target.transform.position;

		if(MoveToClick)
			StartCoroutine(DetectClicks());
	}

	IEnumerator DetectClicks()
	{
		while(true)
		{
			if( Input.GetMouseButtonDown(0) )
			{
				RaycastHit hit;
				if( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f ) )
				{
					if(hit.collider.gameObject.isStatic)
						nma.destination = hit.point;
				}
			}


			yield return new WaitForFixedUpdate();
		}

	}




	void SetTarget(GameObject target)
	{
		Target = target;
		nma.destination = Target.transform.position;
	}
}
