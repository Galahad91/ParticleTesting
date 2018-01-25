using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int maxEnemy;
    public int currentEnemyNumber;
    public GameObject[] Weapons;
    public Transform magicDepot;
    public Transform InventoryDepot;

    public GameObject player;
    [HideInInspector] public GameObject cameraMain;
    [HideInInspector] public Transform cameraPin;
    [HideInInspector] public GameObject[] enemyList;

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
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        cameraPin = GameObject.FindGameObjectWithTag("CameraPin").transform;
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        currentEnemyNumber = enemyList.Length;
        maxEnemy += currentEnemyNumber;
    }

    public void DealDamage(GameObject target, float dmg)
    {
        if(target.transform.tag == "Enemy")
        {
            if (target.GetComponent<Enemy>().health > dmg)
            {
                target.GetComponent<Enemy>().health -= dmg;
            }
            else
            {
                Destroy(target);
                currentEnemyNumber--;
            }
        }
        else if(target.transform.tag == "Player")
        {

            if (player.GetComponent<MageController>().health > dmg)
            {
                player.GetComponent<MageController>().health -= dmg;
            }
            else
            {
                Debug.Log("GAMEOVER");
            }
        }
    }
    
}
