using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("oke");
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null && controller.currentHealth >0)
        {
            controller.ChangeHealth(damage);
        }
    }
}
