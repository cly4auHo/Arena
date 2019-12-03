using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text fullScore;
    [SerializeField] private GameObject menuDeath;
    private int score;

    private const string EnemyTag = "Enemy";
    private const string BulletTag = "Bullet";

    void Start()
    {
        Player.Die += PlayerDeath;

        score = 0;
        scoreText.text = "Score : " + score.ToString();

        menuDeath.SetActive(false);
        scoreText.enabled = true;
    }

    public void ScoreUp()
    {
        scoreText.text = "Score : " + (++score).ToString();
    }

    private void PlayerDeath()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuDeath.SetActive(true);
        fullScore.text = "Очков набрано :" + score.ToString();

        scoreText.enabled = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        GameObject[] bullets = GameObject.FindGameObjectsWithTag(BulletTag);

        if (enemies.Length != 0)
        {
            foreach (GameObject enemie in enemies)
            {
                Destroy(enemie);
            }
        }

        if (bullets.Length != 0)
        {
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }
        }

        Player.Die -= PlayerDeath;
    }
}
