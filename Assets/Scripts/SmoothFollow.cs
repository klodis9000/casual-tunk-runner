using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    private Transform target;

    public float distance = 6.3f;
    public float height = 3.5f;

    public float heightDamping = 3.25f;
    public float rotationDamping = 0.27f;

    void Start () 
    {
        target = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    void LateUpdate () 
    {
        followPlayer ();
    }

    void followPlayer() //слежка за игроком
    {

        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle (
            currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        currentHeight = Mathf.Lerp (
            currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion current_Rotation = Quaternion.Euler (0f, currentRotationAngle, 0f);

        transform.position = target.position;
        transform.position -= current_Rotation * Vector3.forward * distance;

        transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);

    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
