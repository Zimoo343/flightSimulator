using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditCollectible : MonoBehaviour
{
    public GameManager gameManager;
    
    public float angleThreshold = 1;

    private Quaternion initialRotation; 

    private bool hasCollapsed = false;



    void Start()
    {
     initialRotation = transform.rotation;   
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasCollapsed && gameObject.CompareTag("credit") && HasCollectibleCollapsed()){
            Debug.Log("Punto recogido");
          gameManager.IncrementScore();
          Destroy(gameObject);
        }
    }

bool HasCollectibleCollapsed(){
    return Quaternion.Angle(transform.rotation, initialRotation) >
    angleThreshold;
}

}
