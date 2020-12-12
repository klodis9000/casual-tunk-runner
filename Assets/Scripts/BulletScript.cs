using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody myBody;

    public void Move(float speed)
    {
        myBody.AddForce(transform.forward.normalized * speed); //задаю силу толчка снаряда
        Invoke("DeactivatedGameObject",5f); //вызов функции через 5 секунд
    }

    void DeactivatedGameObject() //деактивация объекта
    {
        gameObject.SetActive(false);
    }

     void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag=="Obstacle") //если снаряд солкнулся с препятствием
        {
            gameObject.SetActive(false);
        }
    }
}
