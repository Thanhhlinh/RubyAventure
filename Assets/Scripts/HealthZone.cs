using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZone : MonoBehaviour
{
    public static HealthZone Instance;
    public float upHealth;
    public bool isZone;

    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("oke");
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null && controller.currentHealth <controller.maxHealth)
        {
            controller.ChangeHealth(upHealth);
            isZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null && controller.currentHealth <= controller.maxHealth)
        {
            isZone = false;
        }
    }
}
