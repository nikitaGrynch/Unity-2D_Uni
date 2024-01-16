using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BirdScript : MonoBehaviour
{
    private float discreteForceFactor = 400f;
    private float continualForceFactor = 5f;
    [SerializeField]
    private TMP_Text timerText;
    private float gameTime = 0f;

    private Rigidbody2D body; // ссылка на компонент "нашего" объекта
 

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        timerText.text = ((int)gameTime).ToString("00:00");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(discreteForceFactor * Time.timeScale * Vector2.up);
        }
        if (GameState.isWkeyEnabled && Input.GetKey(KeyCode.W))
        {
            body.AddForce(continualForceFactor * Time.timeScale * Vector2.up);
        }
        this.transform.eulerAngles = new Vector3(0, 0, body.velocity.y);
        GameState.vitality -= Time.deltaTime / GameState.vitalityPeriod;
        if (GameState.vitality <= 0)
        {
            GameState.vitality = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision " + collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform parent = other.gameObject.transform.parent;
        if (parent != null && parent.gameObject.CompareTag("Pipe"))
        {
            GameState.isPipeHitted = true;
        }
        if (other.gameObject.CompareTag("Food")){
            GameState.vitality = 1f;
            GameObject.Destroy(other.gameObject);
        }
    }
}
