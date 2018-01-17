using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject player;
    public GameObject[] enemyList;
    public int currentEnemyNumber;
    public int maxEnemy;
	
	void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        currentEnemyNumber = enemyList.Length;
        maxEnemy += currentEnemyNumber;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
