using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnEnemyType{ enemy1 = 0, enemy2 = 1};

public class EnemySpawn : MonoBehaviour
{
    public float SpawnTime = 5;
    public float timer;
    public SpawnEnemyType Choose;
    public GameObject[] enemyTypes;

    void Awake ()
    {
        timer = SpawnTime;
	}
	
    void SpawnEnemy(SpawnEnemyType enemyType)
    {
        Vector3 pos = gameObject.transform.position;
        Quaternion rot = gameObject.transform.rotation;
        Instantiate(enemyTypes[(int)enemyType],pos,rot);
        GameManager.instance.currentEnemyNumber++;
    }

	// Update is called once per frame
	void Update ()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (GameManager.instance.currentEnemyNumber < GameManager.instance.maxEnemy)
            {
                SpawnEnemy(Choose);
                timer = SpawnTime;
            }
        }

	}
}
