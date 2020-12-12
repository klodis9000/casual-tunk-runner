using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance; //экземпляр

    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;
    public Transform[] lanes; //полосы
    public float minObstacleDelay = 10f, maxObstacleDelay = 40f; //задержка появления препятствий
    private float halfGroundSize; //половина размера площадки
    private BaseController playerController;

    private Text scoreText;
    private int zombieKillCount;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject gameoverPanel;

    [SerializeField]
    private Text finalScore;
    
    void Awake()
    {
        MakeIntance();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<GroundBlock>().halfLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();
        StartCoroutine("GeneratorObstacles");

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    void MakeIntance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance!=null)
        {
            Destroy(gameObject);
        }
        
    }

    IEnumerator GeneratorObstacles()
    {
        float timer = Random.Range(minObstacleDelay, maxObstacleDelay) / playerController.speed.z; //таймер в рамках которого будут создаваться препятствия
        yield return new WaitForSeconds(timer);

        CreateObstacles(playerController.gameObject.transform.position.z + halfGroundSize); 
        StartCoroutine("GeneratorObstacles");

    }

    void CreateObstacles(float zPos) //создание препятствий
    {
        int r = Random.Range(0, 10);

        if (0 <= r && r < 7) //т.к препятствий 7, то если случайное число вошло в диапазон
        {
            int obstacleLane = Random.Range(0, lanes.Length); //взятие случайной линии

            AddObstacle(new Vector3(lanes[obstacleLane].transform.position.x, 0f, zPos), Random.Range(0, obstaclePrefabs.Length)); //добавить препятствие на карту

            int zombieLane = 0;

            if (obstacleLane == 0)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 2; 
                
                /*разбор
                 Random.Range(0, 2) == 1 если случайное число =1
                 если это правда, использовать значение, когда линия зомби будет равна одному, иначе использовать значение, чтобы линия зомби имела бы значение два */
                
                /*if (Random.Range(0, 2) == 1)
                {
                    zombieLane = 1;
                }
                else
                {
                    zombieLane = 2;
                }*/
            }
            
            else if (obstacleLane == 1)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 0 : 2; //если случайный диапазон от 0 до 2 равен 1, использовать значение 0, иначе 2
            }
            
            else if (obstacleLane == 2)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 0; //если случайный диапазон от 0 до 2 равен 1, использовать значение 1, иначе 0
            }
            
            AddZombies(new Vector3(lanes[zombieLane].transform.position.x, 0.1f, zPos));
        }
        
        
    }

    void AddObstacle(Vector3 position, int type)
    {
        GameObject obstacle = Instantiate(obstaclePrefabs[type], position, Quaternion.identity);
        bool mirror = Random.Range(0, 2) == 1; //проверка если истина то вернёт 1, если не 1 то ложь

        switch (type)
        {
            case 0:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f); //задаю градус поворота
                //mirror ? -20 : 20 проверка истинно ли зеркало, если правда, то вернёт -20, иначе 20
                break;
            case 1:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 2:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -1 : 1, 0f);
                break;
            case 3:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -170 : 170, 0f);
                break;
        }

        obstacle.transform.position = position;

    }

    void AddZombies(Vector3 pos)
    {
        int count = Random.Range(0, 3) + 1;
        for (int i = 0; i < count; i++)
        {
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(1f, 10f) * i); //сдвиг
            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], pos + shift * i, Quaternion.identity); //создание зомби
        }
    }

    public void IncreaseScore() //увеличение счёта
    {
        zombieKillCount++; //увеличение счётчика
        scoreText.text = zombieKillCount.ToString(); //преобразование в строку
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; //остановка времени в игре
    }

    public void ResumeGame() //перезапуск
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; //возобновление течения времени в стандартном режиме
    }

    public void ExitGame() //выход из игры
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Gameover()
    {
        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
        finalScore.text = "Убито: "+zombieKillCount.ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    
}
