using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private bool medkit;
    [SerializeField] private bool ammunition;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            
            Consume();
            Destroy(gameObject);
        }
    }

    private void Consume()
    {
        if (medkit) RestoreHealth();
        if (ammunition) RestoreAmmo();
    }

    void RestoreHealth()
    {
        player.GetComponent<PlayerStats>().Heal();
    }

    void RestoreAmmo()
    {
        player.GetComponentInChildren<WeaponManager>().RestoreCurAmmo();
    }
    void Start()
    {
        
    }

   
}
