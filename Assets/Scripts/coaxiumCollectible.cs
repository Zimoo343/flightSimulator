using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoaxiumCollectible : MonoBehaviour
{
   public GameManager gameManager;
    public Nave Nave;
    
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
        if(!hasCollapsed && gameObject.CompareTag("coaxium") && HasGasCollapsed()){
            Debug.Log("Punto recogido");
          gameManager.IncrementScore();
          Nave.moveSpeed = Nave.moveSpeed + 3;
          Nave.rotationMultiplier++;
          Destroy(gameObject);
        }
    }

bool HasGasCollapsed(){
    return Quaternion.Angle(transform.rotation, initialRotation) >
    angleThreshold;
}

}
