using UnityEngine;

public class FallTriggerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pointEffect;
    
    private void OnTriggerEnter(Collider other)
    {
        //if it`s sphere spawn vfx, add point and destroy that
        if (other.CompareTag("Sphere"))
        {
            Instantiate(pointEffect, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            
            //Increase points
            LevelController.points++;
            
            //call Actions
            ActionManager.SendOnSphereFallForUI(LevelController.points);
            ActionManager.SendOnSphereFall();
        }
        //if it`s bomb just destroy that
        if (other.CompareTag("Bomb"))
        {
            Destroy(other.gameObject);
        }
    }
    
}
