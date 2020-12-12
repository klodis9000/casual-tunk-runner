using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timer = 3f;

    void Start()
    {
        Invoke("DeactivatedGameObject", timer);
    }

    void DeactivatedGameObject()
    {
        gameObject.SetActive(false);
    }
}
