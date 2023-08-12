using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    private int holding;
    
    private void Update()
    {
        GetScroll();
    }

    void HideWeapon()
    {
        foreach (var t in weapons)
        {
            t.SetActive(false);
        }

        weapons[holding].SetActive(true);
        
    }
    private void GetScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            
            if (scroll > 0)
            {
                holding++;
                if (holding >= weapons.Length) holding = 0;
            }
            else if (scroll < 0)
            {

                holding--;
                if (holding < 0) holding = weapons.Length - 1;
            }
            HideWeapon();
        }
    }
}
