using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    // [SerializeField]
    private float forceFactor = 300f;
    private Rigidbody2D body; // ссылка на компонент "нашего" объекта
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * forceFactor);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            body.AddTorque(1000f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            body.AddForce(Vector2.left * 100f);
            body.AddTorque(300f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            body.AddForce(Vector2.right * 100f);
            body.AddTorque(-300f);

        }
    }
}
