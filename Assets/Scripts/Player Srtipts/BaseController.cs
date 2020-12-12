using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Vector3 speed; //скорость по вектору
    public float xSpeed = 8f, zSpeed = 15f; //скорость во время поворота и скорость во время прямой езды
    public float accelerated = 15f, deaccelerated = 10f; //ускорение и снижение скорости
    
    protected float rotationSpeed = 10f; //скорость вращения
    protected float maxAngle=10f; //угол поворота

    public float lowSoundPitch, normalSoundPitch, highSoundPitch; //низкий, средний и высокий уровень звука

    public AudioClip engineOnSound, engineOffSound; //звука включённого и выключенного двигателя
    private bool isSlow; //флаг медленности

    private AudioSource soundManager;



    // Start is called before the first frame update
    void Awake()
    {
        speed = new Vector3(0f, 0f, zSpeed); //задаюю скорость движения по оси Z
        soundManager = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void MoveLeft() //движение влево
    {
        speed = new Vector3(-xSpeed / 2f, 0f, speed.z);
    }
    
    protected void MoveRight() //движение вправо
    {
        speed = new Vector3(xSpeed / 2f, 0f, speed.z);
    }
    
    protected void MoveStraight() //движение прямо
    {
        speed = new Vector3(0f, 0f, speed.z);
    }

    protected void MoveNormal() //нормализация скорости движения
    {
        if (isSlow)
        {
            isSlow = false;
            soundManager.Stop();
            soundManager.clip = engineOnSound;
            soundManager.volume = 0.3f;
            soundManager.Play();
        }

        speed = new Vector3(speed.x, 0f, zSpeed);
    }

    protected void MoveSlow() //медленное движение
    {
        if (!isSlow)
        {
            isSlow = true;
            
            soundManager.Stop();
            soundManager.clip = engineOffSound;
            soundManager.volume = 0.5f;
            soundManager.Play();
        }

        speed = new Vector3(speed.x, 0f, deaccelerated);
    }

    protected void MoveFast() //ускорение
    {
        speed = new Vector3(speed.x, 0f, accelerated);
    }
}
