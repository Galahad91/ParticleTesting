using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeapon : MonoBehaviour
{
    public GameObject magic;
    public GameObject skin;

    private void Awake()
    {
        magic.GetComponent<MagicStance>().currentWeapon = transform;
    }
}
