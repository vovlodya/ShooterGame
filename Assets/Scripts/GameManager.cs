using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int enemiesAlive = 0;
    public int enemiesKilled = 0;
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject ammunitionPrefab;
    public GameObject medkitPrefab;
    public GameObject[] itemSpawnPoints;
    public int round = 1;

    public Text roundNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesAlive == 0)
        {    
            round++;
            if (round%5 == 0)
            {
                CreateItems();
            }
            NextWave();
            roundNumber.text = "Round:" + round.ToString();
        }
    }

    void CreateItems()
    {
        Vector3 spawnPoint = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Length)].transform.position;
        Instantiate(ammunitionPrefab, spawnPoint, Quaternion.identity);
        spawnPoint = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Length)].transform.position;
        Instantiate(medkitPrefab, spawnPoint, Quaternion.identity);
    }
    void NextWave()
    {
        for (int i = 0; i < round+5; i++)
        {
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            GameObject enemySpawned = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            enemySpawned.GetComponent<EnemyManager>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }
    }
}
