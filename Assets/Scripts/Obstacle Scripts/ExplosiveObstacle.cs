using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : MonoBehaviour
{
    public GameObject explosivePrefab;
    public int damage=20; //еденица наносимого урона

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Player")
        {
            Instantiate(explosivePrefab, transform.position, Quaternion.identity);

            target.gameObject.GetComponent<PlayerHealth>().ApplyDamage(damage); //нанесение урона
            
            gameObject.SetActive(false);
        }

        if (target.gameObject.tag == "Bullet")
        {
            Instantiate(explosivePrefab, transform.position, Quaternion.identity);
            
            gameObject.SetActive(false);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
    
    
    
    
    
    
    
    
    
}
