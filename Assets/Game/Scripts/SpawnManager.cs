using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerUps;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyspawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }
    // Create a Couratine to Spawn Enemy Every 5 Second   

    IEnumerator EnemyspawnRoutine()
    {
        while(true)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-6.34f, 6.34f), 6.34f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.8f);
           
        }

    }
    IEnumerator PowerUpSpawnRoutine()
    {
        while (true)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(-6.34f, 6.34f), 6.34f, 0), Quaternion.identity);
            yield return new WaitForSeconds(15.0f);
         

        }

    }

}

