using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    public float upHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if(controller != null && controller.currentHealth < controller.maxHealth)
        {
            controller.ChangeHealth(upHealth);
            Destroy(gameObject);

        }
    }

}
