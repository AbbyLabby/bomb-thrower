using UnityEngine;

public class BombScript : MonoBehaviour
{
	[SerializeField]
	private float radius;
	[SerializeField]
	private float force;
	[SerializeField]
	private GameObject explosionEffect;
	
	//check if bomb collide with another object besides plane
	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.collider.CompareTag("Plane"))
			Explode();
	}

	//Explosion function
	private void Explode()
	{
		//check the objects which are located in radius
		Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, radius);

		foreach (var t in overlappedColliders)
		{
			//check what this object has a rigidbody
			var rigidbody = t.attachedRigidbody;
			if (rigidbody)
			{
				//add explosion force to the object
				rigidbody.AddExplosionForce(force, transform.position, radius);
			}
		}

		Destroy(gameObject);
		//visual effect of explosion
		Instantiate(explosionEffect, transform.position, Quaternion.identity);

	}
}
