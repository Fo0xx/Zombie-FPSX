using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;

    public Sprite emptySlot;

    public GameObject middleDot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon = GetUnactiveWeaponSlot().GetComponentInChildren<Weapon>();

        if(activeWeapon)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeftFor(activeWeapon.currentWeaponModel)}";

            Weapon.WeaponModel activeWeaponModel = activeWeapon.currentWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(activeWeaponModel);

            activeWeaponUI.sprite = GetWeaponSprite(activeWeaponModel);

            if(unActiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.currentWeaponModel);
            }
        } else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";

            ammoTypeUI.sprite = emptySlot;

            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;
        }
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel activeWeaponModel)
    {
        switch (activeWeaponModel)
        {
            case Weapon.WeaponModel.Glock:
                return Resources.Load<Sprite>("Glock_Weapon");
            case Weapon.WeaponModel.AK47:
                return Resources.Load<Sprite>("AK47_Weapon");
            default:
                return null;
        }
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel activeWeaponModel)
    {
        switch (activeWeaponModel)
        {
            case Weapon.WeaponModel.Glock:
                return Resources.Load<Sprite>("Pistol_Ammo");
            case Weapon.WeaponModel.AK47:
                return Resources.Load<Sprite>("Rifle_Ammo");
            default:
                return null;
        }
    }

    private GameObject GetUnactiveWeaponSlot()
    {
        foreach (GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if (weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }
        // this will never happen, but we need to return something
        return null;
    }
}
