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
    [SerializeField] private GameObject menu;

    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";

    void Start()
    {
        menu.SetActive(false);
        score = 0;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healtText.enabled = true;
        strengthText.enabled = true;
        scoreText.enabled = true;
        pauseText.enabled = false;
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
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else //death 
        {
            menu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            healtText.enabled = false;
            strengthText.enabled = false;
            scoreText.enabled = false;

            fullScore.text = "Очков набрано :" + score.ToString();
        }
    }

    public void ScoreUp()
    {
        score++;
    }

    public void Restart()
    {
        foreach (GameObject RedEnemies in GameObject.FindGameObjectsWithTag(RedEnemyTag))
        {
            Destroy(RedEnemies);
        }
        foreach (GameObject BlueEnemies in GameObject.FindGameObjectsWithTag(BlueEnemyTag))
        {
            Destroy(BlueEnemies);
        }

        Application.LoadLevel(Application.loadedLevel);
    }

    void Pause()
    {
        pause = true;
        pauseText.enabled = true;
        Time.timeScale = 0f;
    }

    void Resume()
    {
        pause = false;
        pauseText.enabled = false;
        Time.timeScale = 1f;
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
