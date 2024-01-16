using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    // [SerializeField]
    private float pipeVelocity = 1.5f;

    void Start()
    {

    }

    void Update()
    {
        this                       
            .transform             
            .Translate(          
                pipeVelocity     
                * Time.deltaTime    
                * Vector2.left);  
    }

}
