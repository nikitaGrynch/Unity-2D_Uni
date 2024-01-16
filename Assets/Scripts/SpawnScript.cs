using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
   [SerializeField]
    private GameObject[] pipePrefabs;
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private GameObject food2Prefab;

    // private float pipeSpawnPeriod = 4f;   // кожні 4 секунди
    private float pipeCountdown;
    private float foodCountdown;

    void Start()
    {
        pipeCountdown = GameState.pipeSpawnPeriod;
        SpawnPipe();
        foodCountdown = pipeCountdown / 2f;
    }
    void Update()
    {
        pipeCountdown -= Time.deltaTime;
        if(pipeCountdown <= 0)
        {
            pipeCountdown = GameState.pipeSpawnPeriod;
            SpawnPipe();
            foodCountdown = pipeCountdown / 2f;
        }

        
        if (foodCountdown > 0)
        {
            if(foodCountdown - Time.deltaTime <= 0)
            {
                SpawnFood();
            }
            foodCountdown -= Time.deltaTime;
        }
    }
    private void SpawnFood()
    {
        var food = GameObject.Instantiate(
            Random.value < 0.5f ? foodPrefab : food2Prefab
        );
        food.transform.position =
            this.transform.position +
                Vector3.up * Random.Range(-3.7f, 3.7f);
    }
    private void SpawnPipe()
    {
        int pipeIndex = Random.Range(0, pipePrefabs.Length);
        var pipe = GameObject.Instantiate(pipePrefabs[pipeIndex]);  // ~ new pipePrefab
        pipe.transform.position = 
            this.transform.position + 
                Vector3.up * Random.Range(-1.7f, 1.7f);
    }
}
