using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private Slider slider;
    private float currentHealth, maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = PlayerController.instance.currentHealth;
        maxHealth = PlayerController.instance.maxHealth;      
        float fillValue = currentHealth / (float)maxHealth;
        slider.value = fillValue;

       

    }
}
