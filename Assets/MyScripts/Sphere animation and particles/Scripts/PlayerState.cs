using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

 public enum Stance
 {
    Default,
    Mobility,
    Defense
 }
 public enum Weapon
 {
    Staff = 0,
    Shield = 1,
    Sword = 2
 }

public class PlayerState : MonoBehaviour
{

    public Stance currentStance;
    public Weapon currentWeapon;
    public Transform weaponHolder;

    

    [HideInInspector] public float stance;
    [HideInInspector] public float cap;
    [HideInInspector] public int weaponIndex;

    [HideInInspector] public GameObject currentSkin;
    [HideInInspector] public GameObject currentMagic;
    [HideInInspector] public MagicStance currentMagicStance;
    [HideInInspector] public GameObject weaponEquipped;
    [HideInInspector] public MageController controller;

    private void Awake()
    {
        controller = gameObject.GetComponent<MageController>();
    }
    private void Start()
    {
        weaponIndex = (int)currentWeapon;
        weaponEquipped = GameManager.instance.Weapons[weaponIndex];
        EquipMagic();
        
    }

    void EquipMagic()
    {
        weaponEquipped.tag = "CurrentWeapon";
        controller.weapon = weaponEquipped.transform;

        currentMagic = weaponEquipped.GetComponent<MagicWeapon>().magic;
        currentSkin = weaponEquipped.GetComponent<MagicWeapon>().skin;

        currentMagicStance = currentMagic.GetComponent<MagicStance>();

        controller.currentMagic = currentMagicStance;

        currentSkin.transform.parent = GameManager.instance.player.transform;
        currentSkin.transform.position = GameManager.instance.player.transform.position;
        currentSkin.transform.rotation = GameManager.instance.player.transform.rotation;

        weaponEquipped.transform.parent = weaponHolder;
        weaponEquipped.transform.position = weaponHolder.position;

        currentMagic.transform.parent = GameManager.instance.player.transform;
        currentMagic.transform.position = GameManager.instance.player.transform.position;
        currentMagic.transform.rotation = GameManager.instance.player.transform.rotation;

        currentMagicStance.InitializeMagic();
    }

    void UnequipMagic()
    {
        weaponEquipped.tag = "Untagged";

        currentSkin.transform.parent = GameManager.instance.InventoryDepot.transform;
        currentSkin.transform.position = GameManager.instance.InventoryDepot.transform.position;

        currentMagic.transform.parent = GameManager.instance.InventoryDepot.transform;
        currentMagic.transform.position = GameManager.instance.InventoryDepot.transform.position;

        weaponEquipped.transform.parent = GameManager.instance.InventoryDepot.transform;
        weaponEquipped.transform.position = GameManager.instance.InventoryDepot.transform.position;
    }

    void SwitchMagic()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnequipMagic();
           
            if(weaponIndex < GameManager.instance.Weapons.Length -1)
            {
                weaponIndex++;
            }
            else if (weaponIndex == GameManager.instance.Weapons.Length -1)
            {
                weaponIndex = 0;
            }
  
            weaponEquipped = GameManager.instance.Weapons[weaponIndex];
            currentWeapon = (Weapon)weaponIndex;
            EquipMagic();
            
        }
    }

    void SwitchStance()
    {
            if (cap == 1)
            {
                cap = 0;
                currentMagicStance.cRange = cap;
                currentStance = Stance.Default;

                currentMagicStance.ChangeStartColor();
            }
            else if (cap == 0)
            {
                cap += 0.5f;
                currentMagicStance.cRange = cap;
                currentStance = Stance.Mobility;

                currentMagicStance.ChangeStartColor();
            }
            else if (cap == 0.5f)
            {
                cap += 0.5f;
                currentMagicStance.cRange = cap;
                currentStance = Stance.Defense;

                currentMagicStance.ChangeStartColor();
            }
        
    }

    private void Update()
    {
        SwitchMagic();

        if (Input.GetMouseButtonDown(1))
        {
            SwitchStance();
        }
        //For Animator
        if (stance != cap)
        {
            if (stance > cap)
            {
                stance -= Time.deltaTime;
            }
            else if (stance < cap)
            {
                stance += Time.deltaTime;
            }

        }
        currentMagicStance.anim.SetFloat("Stance", stance);
    }
}
