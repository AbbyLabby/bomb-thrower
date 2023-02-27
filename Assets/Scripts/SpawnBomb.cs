using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBomb : MonoBehaviour
{
	[SerializeField]
	private GameObject target;
	[SerializeField]
	private Slider _sliderForce;
	
	private float _holdTimeStart;
	private const float MAX_FORCE = 20f;
	private bool _isSpawned = false;
	private Rigidbody _spawnedBombRB;
	private GameObject _spawnedBomb;
	private GameObject _spawnedBombArrow;

	private const float Z_Pos = -4.5f;

	// Update is called once per frame
	private void Update()
	{
		if(_isSpawned) return;
		if (Input.GetMouseButtonDown(0))
		{
			//if mouse button 0 is down, start the hold time timer
			_holdTimeStart = Time.time;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit))
				//object can only spawn on plane
				if (hit.collider != null && hit.collider.CompareTag("Plane"))
				{

					_spawnedBomb = Instantiate(target, new Vector3(hit.point.x, hit.point.y + 0.5f, Z_Pos), Quaternion.identity);
					_spawnedBombArrow = _spawnedBomb.transform.GetChild(1).gameObject;
					_spawnedBombRB = _spawnedBomb.GetComponent<Rigidbody>();
					_spawnedBombRB.isKinematic = true;

				}
		}
		if(!_spawnedBomb) return;
		//aim the bomb while holding mouse button
		if (Input.GetMouseButton(0))
		{
			var holdDownTime = Time.time - _holdTimeStart;
			ShowForce(CalculateForce(holdDownTime));
			
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit))
				//object can only spawn on plane
				if (hit.collider != null && hit.collider.CompareTag("Plane"))
				{

					if(_spawnedBomb != null) _spawnedBomb.transform.position = new Vector3(hit.point.x, hit.point.y + 0.5f, Z_Pos);

				}
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			//calculate how much time has passed
			var holdDownTime = Time.time - _holdTimeStart;
			//spawn countdown
			StartCoroutine(SpawnCountDown());
			//add force to the spawned object
			if (_spawnedBombRB != null)
			{
				ShowForce(0);
				_spawnedBombArrow.SetActive(false);
				_spawnedBombRB.isKinematic = false;
				_spawnedBombRB.AddForce(new Vector3(0, 0, CalculateForce(holdDownTime)), ForceMode.Impulse);
			}
		}

	}

	private IEnumerator SpawnCountDown()
	{
		_isSpawned = true;
		yield return new WaitForSeconds(1);
		_isSpawned = false;
	}

	private static float CalculateForce(float holdTime)
	{
		var maxForceDownTime = 2f;
		var holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceDownTime);
		var force = holdTimeNormalized * MAX_FORCE;
		return force;
	}

	private void ShowForce(float currentForce)
	{
		_sliderForce.value = currentForce;
		if(currentForce < 20) _spawnedBombArrow.transform.localScale = new Vector3(_spawnedBombArrow.transform.localScale.x + (currentForce * .00015f), _spawnedBombArrow.transform.localScale.y, _spawnedBombArrow.transform.localScale.z);
	}
}
