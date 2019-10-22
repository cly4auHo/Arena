using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    [SerializeField] private Text scoreText;
    private Player player;


    void Start()
    {
        score = 0;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player && player.GetHealth() > 0)
        {
            scoreText.text = "Score : " + score.ToString();
        }
        else
        {

        }
    }

    public void ScoreUp()
    {
        score++;
    }
}
