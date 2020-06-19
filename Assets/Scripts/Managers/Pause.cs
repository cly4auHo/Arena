using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    private EnemyCreator enemyCreator;
    private PlayerShot playerShot;
    private Player player;
    private bool pause;
    private const string EnemyTag = "Enemy";
    private const string MainSceneName = "Main";

    void Start()
    {
        playerShot = FindObjectOfType<PlayerShot>();
        enemyCreator = FindObjectOfType<EnemyCreator>();
        player = FindObjectOfType<Player>();

        menuPause.SetActive(false);
        pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && player.IsAlive())
        {
            if (pause)
                Resume();
            else
                Paused();
        }
    }

    public void Restart()
    {
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag(EnemyTag))
            Destroy(enemies);

        SceneManager.LoadScene(MainSceneName);
        Time.timeScale = 1f;
    }

    void Paused()
    {
        pause = true;
        playerShot.StopAim();
        Time.timeScale = 0f;

        menuPause.SetActive(true);
        enemyCreator.SetSpawn(false);
    }

    void Resume()
    {
        pause = false;
        playerShot.GoAim();
        Time.timeScale = 1f;

        menuPause.SetActive(false);
        enemyCreator.SetSpawn(true);
    }
}
