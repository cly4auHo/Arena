using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int health = 100;
    private int strength = 50;
    private int score;
    private bool pause = false;

    [SerializeField] private Text healtText;
    [SerializeField] private Text strengthText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text pauseText;
    [SerializeField] private Text fullScore;
    [SerializeField] private GameObject menuDeath;
    [SerializeField] private GameObject menuPause;

    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";

    private Camera camera;
    private int size = 12;
    private float posX;
    private float posY;

    void Start()
    {
        menuDeath.SetActive(false);
        menuPause.SetActive(false);
        score = 0;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healtText.enabled = true;
        strengthText.enabled = true;
        scoreText.enabled = true;

        camera = GetComponentInChildren<Camera>();

    }

    void Update()
    {
        if (health > 0)
        {
            healtText.text = "Health " + health.ToString() + "/100";
            strengthText.text = "Strength " + strength.ToString() + "/100";
            scoreText.text = "Score : " + score.ToString();

            if (Input.GetKeyDown(KeyCode.Escape)) //pause
            {
                if (!pause)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }
        else //death 
        {
            menuDeath.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            healtText.enabled = false;
            strengthText.enabled = false;
            scoreText.enabled = false;

            fullScore.text = "Очков набрано :" + score.ToString();

            foreach (GameObject RedEnemies in GameObject.FindGameObjectsWithTag(RedEnemyTag))
            {
                Destroy(RedEnemies);
            }
            foreach (GameObject BlueEnemies in GameObject.FindGameObjectsWithTag(BlueEnemyTag))
            {
                Destroy(BlueEnemies);
            }
        }
    }

    public void ScoreUp()
    {
        score++;
    }

    public void Restart()
    {
        score = 0;

        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        menuPause.SetActive(true);
        pause = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    void Resume()
    {
        menuPause.SetActive(false);
        pause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    void OnGUI()
    {
        if (!pause)
        {
            posX = camera.pixelWidth / 2 - size / 4;
            posY = camera.pixelHeight / 2 - size / 2;
            GUI.Label(new Rect(posX, posY, size, size), "*");
        }
    }

    public int GetStrengt()
    {
        return strength;
    }

    public void SetStrengt(int strength)
    {
        this.strength = strength;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public bool IsPaused()
    {
        return pause;
    }
}
