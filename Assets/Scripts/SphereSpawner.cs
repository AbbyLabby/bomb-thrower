using System.Collections;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private Transform leftSpawner;
    [SerializeField] private Transform rightSpawner;

    [SerializeField] private GameObject spherePrefab;

    [SerializeField] private int spawnLimit;

    [SerializeField] private float delay;
    
    private int _spheresOnScene;
    
    private void Start()
    {
        //assign action
        ActionManager.OnSphereFall += RemoveSphere;
        
        //starting the coroutines of spawning spheres
        StartCoroutine(SpawnLeft());
        StartCoroutine(SpawnRight());
    }
    
    private IEnumerator SpawnLeft()
    {
        while (true)
        {
            if (_spheresOnScene > spawnLimit) yield return null; //just skip here
            else
            {
                Instantiate(spherePrefab, leftSpawner.position, Quaternion.identity);
                _spheresOnScene++;
                
                //delay
                yield return new WaitForSecondsRealtime(delay); 
            }
        }
    }

    private IEnumerator SpawnRight()
    {
        while (true)
        {
            if (_spheresOnScene > spawnLimit) yield return null; //just skip here
            else
            {
                Instantiate(spherePrefab, rightSpawner.position, Quaternion.identity);
                _spheresOnScene++;
                
                //delay
                yield return new WaitForSecondsRealtime(delay);
            }
        }
    }

    private void RemoveSphere()
    {
        _spheresOnScene--;
    }
    
}
