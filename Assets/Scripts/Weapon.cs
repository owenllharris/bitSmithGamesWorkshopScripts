using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	public GameObject projectile;

	// Use this for initialization
	void Start () {
	
	}

	public void Shoot(Vector2 dir)
	{
		GameObject go = Instantiate(projectile, this.transform.position + new Vector3(dir.x, dir.y, 0f), Quaternion.identity) as GameObject;
		go.BroadcastMessage("Fire", dir);
	}
}
