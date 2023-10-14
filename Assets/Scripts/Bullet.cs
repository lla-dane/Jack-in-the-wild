using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int attackDamage = 40;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.tag != "Interactables")
        {
            this.gameObject.SetActive(false);
        }
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<DamageScript>().TakeDamage(attackDamage);
        }
    }
}
