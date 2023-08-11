using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text magazineSizeText;
    [SerializeField] private Text magazineCountText;

    public void UpdateInfo(int magazineSize, int magazineCount)
    {
        magazineSizeText.text = magazineSize.ToString();
        magazineCountText.text = magazineCount.ToString();
    }
}
