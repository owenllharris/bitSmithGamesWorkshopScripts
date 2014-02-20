using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public Projectile projectile;
	public float RateOfFire;

	public GameObject Target;

	public bool FireOnSite = false;

	public enum Direction { AtTarget, left, right, up, down };
	public Direction WhichWayToFire;

	Vector2 direction;

	void Start()
	{
		switch( WhichWayToFire )
		{
		case Direction.AtTarget:
			direction = (transform.position - Target.transform.position).normalized;
			break;

		case Direction.down:
			direction = new Vector2(0, -1);
			break;
		case Direction.up:
			direction = new Vector2(0, 1);
			break;
		case Direction.right:
			direction = new Vector2(1, 0);
			break;
		case Direction.left:
			direction = new Vector2(-1,0);
			break;
		}

		InvokeRepeating( "Fire", RateOfFire, RateOfFire );
	}

	void Update()
	{

	}

	void Fire()
	{
		if( WhichWayToFire == Direction.AtTarget )
			direction = (Target.transform.position - transform.position).normalized;

		Projectile newProjectile = Instantiate( projectile, transform.position , transform.rotation ) as Projectile;
		newProjectile.Fire(direction);
	}
}
