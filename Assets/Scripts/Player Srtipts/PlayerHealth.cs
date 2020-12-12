using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int healthValue = 100;
    private Slider healthSlider;

    private GameObject UIHolder;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GameObject.Find("Health Bar").GetComponent<Slider>();

        healthSlider.value = healthValue;

        UIHolder = GameObject.Find("UI Holder");
    }

    public void ApplyDamage(int damageAmount)
    {
        healthValue -= damageAmount;

        if (healthValue < 0)
        {
            healthValue = 0;
        }

        healthSlider.value = healthValue;

        if (healthValue == 0)
        {
            UIHolder.SetActive(false);
            GameplayController.instance.Gameover(); //окончание игры
        }

    }
    
    
    
    
    
    
    
}
