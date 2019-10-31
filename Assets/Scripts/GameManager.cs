using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text fullScore;
    [SerializeField] private GameObject menuDeath;
    [SerializeField] private GameObject enemyCreator;

    private int score;
    private Player player;

    private const string EnemyTag = "Enemy";
    private const string BulletTag = "Bullet";

    void Start()
    {
        player = FindObjectOfType<Player>();

        score = 0;

        menuDeath.SetActive(false);
        scoreText.enabled = true;
    }

    void Update()
    {
        if (player.IsAlive())
        {
            scoreText.text = "Score : " + score.ToString();
        }
        else
        {
            PlayerDeath();
        }
    }

    public void ScoreUp()
    {
        score++;
    }

    void PlayerDeath()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuDeath.SetActive(true);
        fullScore.text = "Очков набрано :" + score.ToString();
        score = 0;

        scoreText.enabled = false;
        enemyCreator.SetActive(false);

        foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag(EnemyTag))
        {
            Destroy(Enemies);
        }
        foreach (GameObject Bullets in GameObject.FindGameObjectsWithTag(BulletTag))
        {
            Destroy(Bullets);
        }
    }
}
