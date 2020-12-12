using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{

    private Rigidbody myBody;


    public Transform bulletStartPoint;
    public GameObject bulletPrefab;
    public ParticleSystem shootFX;

    private Animator shootSliderAnim;

    [HideInInspector]
    public bool canShoot;

    void Start()
    {
        myBody = GetComponent<Rigidbody>(); //получение компонента риджидбоди

        //shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>(); //не видит объект, ниже ищу по тегу
        shootSliderAnim = GameObject.FindGameObjectWithTag("FireBar").GetComponent<Animator>();
        
        GameObject.Find("ShootBtn").GetComponent<Button>().onClick.AddListener(ShootingControl); //вызов функции ShootingControl при нажатии на кнопку ShootBtn
        canShoot = true;
    }

    void Update()
    {
        ControllerMovementWithKeyboard();
        //ShootingControl();
        ChangeRotation();
    }

    void FixedUpdate()
    {
        MoveTank();
    }

    void MoveTank()
    {
        myBody.MovePosition(myBody.position + speed * Time.deltaTime); //задаю движение вперёд для танка
    }

    void ControllerMovementWithKeyboard() //управление клавитаурой
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) //если зажата "A" или стрелка влево
        {
            MoveLeft(); //то двигаться влево
        }
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) //если зажата "D" или стрелка вправо
        {
            MoveRight(); //то двигаться направо
        }
        
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) //если зажата "S" или стрелка вниз
        {
            MoveSlow(); //то снизить скорость
        }
        
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) //если отпустил "A" или стрелку влево
        {
            MoveStraight(); //то двигаться с обычной скорость
        }
        
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) //если отпустил "D" или стрелку вправо
        {
            MoveStraight(); //то двигаться с обычной скорость
        }
        
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) //если отпустил "W" или стрелку вверх
        {
            MoveNormal(); //то нормализовать скорость и отыграть соответствующий звук
        }
        
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) //если отпустил "W" или стрелку вверх
        {
            MoveNormal(); //то нормализовать скорость и отыграть соответствующий звук
        }
    }

    void ChangeRotation()
    {
        if (speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else if (speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);

        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);

        }
    }

    public void ShootingControl()
    {
        /*
        Масштаб, в котором проходит время. Это можно использовать для эффектов замедленного движения.
        Когда timeScale равен 1.0, время проходит так же быстро, как и в реальном времени. Когда timeScale равен 0,5, время проходит в 2 раза медленнее, чем в реальном времени.
        Когда timeScale установлен на ноль, игра в основном приостанавливается, если все ваши функции не зависят от частоты кадров.
          */
        
        if (Time.timeScale != 0)
        {
            if (canShoot)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletStartPoint.position, Quaternion.identity); //создаю новый снаряд
                bullet.GetComponent<BulletScript>().Move(2000f); //получаю метод движения и задаю скорость полёта снаряда
                shootFX.Play();

                canShoot = false;
                //отображение анимации
                shootSliderAnim.Play("Fill");
            }
        }
    }
    
    
    
    
    
    

}
