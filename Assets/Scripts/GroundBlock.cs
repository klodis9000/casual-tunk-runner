using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    public Transform otherBlock; //другой блок
    public float halfLength = 100f; //половина от длины трассы по Z, поскольку её длина по оси составляет 200
    private Transform player;
    private float endOffset=10f; //конец смещения карты по Z
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveGround();
    }

    void MoveGround() //движение блоков
    {
        if (transform.position.z + halfLength < player.transform.position.z - endOffset) //если игрок проехал первый блок и 5% от второго
        {
            //то сместить предыдущий блок вперёд
            transform.position = new Vector3(otherBlock.position.x, otherBlock.position.y, otherBlock.position.z + halfLength * 2);
            
            
        }
    }
    
    
}
