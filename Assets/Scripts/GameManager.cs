using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text fullScore;
    [SerializeField] private GameObject menuDeath;
    private EnemyCreator enemyCreator;

    private int score;
    private Player player;

    private const string EnemyTag = "Enemy";
    private const string BulletTag = "Bullet";

    void Start()
    {
        enemyCreator = FindObjectOfType<EnemyCreator>();
        player = FindObjectOfType<Player>();

        score = 0;
        scoreText.text = "Score : " + score.ToString();

        menuDeath.SetActive(false);
        scoreText.enabled = true;
    }

    void Update()
    {
        if (!player.IsAlive())
        {
            PlayerDeath();
        }
        else
        {
            enemyCreator.SetSpawn(true);
        }
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = "Score : " + score.ToString();
    }

    void PlayerDeath()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuDeath.SetActive(true);
        fullScore.text = "Очков набрано :" + score.ToString();

        scoreText.enabled = false;
        enemyCreator.SetSpawn(false);

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
