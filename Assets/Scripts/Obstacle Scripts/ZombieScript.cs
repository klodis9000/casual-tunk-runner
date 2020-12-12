using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Random = System.Random;

public class ZombieScript : MonoBehaviour
{
    public GameObject bloodFXPrefab;
    private float speed = 1f;

    private Rigidbody myBody;

    private bool isAlive; //живой ли зомби
    
    
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();

        speed = UnityEngine.Random.Range(1f, 5f);

        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            myBody.velocity = new Vector3(0f, 0f, -speed); //движение зомби в обратном направлении
        }

        if (transform.position.y < -10f) //если прошёл больше 10 
        {
            gameObject.SetActive(false);
        }
    }

    void Die()
    {
        isAlive = false;
        
        myBody.velocity = Vector3.zero; //остановка объекта
        GetComponent<Collider>().enabled = false; //отключение коллайдера
        GetComponentInChildren<Animator>().Play("Idle"); 
        
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }
    
    void DeactivatedGameObject() //деактивация объекта
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Player" || target.collider.gameObject.tag == "Bullet") //если зомби столкнулся с снарядом или игроком
        {
            Instantiate(bloodFXPrefab, transform.position, Quaternion.identity); //создать префаб с кровью
            Invoke("DeactivatedGameObject", 3f); //отыграть метод

            GameplayController.instance.IncreaseScore(); //увеличение счёта
            
            Die();
        }
    }
}

