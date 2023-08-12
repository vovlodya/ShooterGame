using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WeaponManager : MonoBehaviour
{
    public GameObject playerCam;
    private PlayerStats stats;
    private HUDManager hud;
    public float range = 50f;
    public int magazine = 30;
    public int ammo = 90;
    public int damage = 25;
    public float fireRate = 0.1f;
    private bool canShoot;
    private int currentMagazine;
    private int currentAmmo;
    public Image muzzleFlashImage;
    public Sprite[] flashes;

    public Vector3 normalLocalPos;
    public Vector3 aimingLocalPos;
    public float aimSmoothing = 10;
    
    private void Awake()
    {
        currentAmmo = ammo;
        currentMagazine = magazine;
        canShoot = true;
        stats = GetComponentInParent<PlayerStats>();
        hud = GetComponentInParent<HUDManager>();
        hud.UpdateWeaponUI(this);
    }

    private void OnEnable()
    {
        hud.UpdateWeaponUI(this);
    }

    // Update is called once per frame
    void Update()
    {
        DetermineAim();
        if (Input.GetMouseButton(0) && !stats.isDead && canShoot && currentMagazine > 0 )
        {
            canShoot = false;
            currentMagazine--;
            hud.UpdateWeaponUI(this);
            StartCoroutine(ShootGun());
            
        }
        else if(Input.GetKeyDown(KeyCode.R) && currentMagazine<magazine && currentAmmo>0)
        {
            int amountNeeded = magazine - currentMagazine;
            if (amountNeeded >= currentAmmo)
            {
                currentMagazine += currentAmmo;
                currentAmmo = 0;
            }
            else
            {
                currentMagazine = magazine;
                currentAmmo -= amountNeeded;
            }
            hud.UpdateWeaponUI(this);
        }
    }

    IEnumerator ShootGun()
    {
        
        StartCoroutine(MuzzleFlash());
        DetermineRecoil();
        RaycastForEnemy();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    IEnumerator MuzzleFlash()
    {
        muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.Length)];
        muzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0, 0, 0, 0);
    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPos;
        if (Input.GetMouseButton(1)) target = aimingLocalPos;
        Vector3 desiredPos = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
        transform.localPosition = desiredPos;
    }

    void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * 0.1f;
    }

    void RaycastForEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, transform.forward, out hit, range))
        {
            EnemyManager enemyManager = hit.transform.GetComponent<EnemyManager>();
            if (enemyManager != null)   
            {
                enemyManager.Hit(damage);
                enemyManager.navMeshAgent.velocity = enemyManager.navMeshAgent.velocity.normalized * -1f;
            }
        }
    }

    public void RestoreCurAmmo()
    {
        currentAmmo = ammo;
        hud.UpdateWeaponUI(this);
    }

    public int GetCurAmmo()
    {
        return currentAmmo;
    }
    public int GetCurMagazine()
    {
        return currentMagazine;
    }
}
