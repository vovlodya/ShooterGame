using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField]private ProgressBar healthBar;
    [SerializeField] private WeaponUI weaponUI;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthBar.SetValues(currentHealth,maxHealth);
    }

    public void UpdateWeaponUI(WeaponManager weapon)
    {
        weaponUI.UpdateInfo(weapon.GetCurMagazine() ,weapon.GetCurAmmo());
    }
}
