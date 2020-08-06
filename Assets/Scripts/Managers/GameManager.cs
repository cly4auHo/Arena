using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text fullScore;
    [SerializeField] private GameObject menuDeath;
    private int score;
    private Player player;
    private const string PlayerTag = "Player";
    private const string pauseManagerTag = "PauseManager";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Player>();
        player.Die += PlayerDeath;
        GameObject.FindGameObjectWithTag(pauseManagerTag).GetComponent<Pause>().RestartGame += Restart;
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
        player.Die -= PlayerDeath;
    }

    private void Restart()
    {
        score = 0;
        menuDeath.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        scoreText.enabled = true;
    }
}
