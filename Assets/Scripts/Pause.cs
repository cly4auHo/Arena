using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    private EnemyCreator enemyCreator;
    private PlayerShot playerShot;

    private bool pause;
    private const string EnemyTag = "Enemy";

    void Start()
    {
        playerShot = FindObjectOfType<PlayerShot>();
        enemyCreator = FindObjectOfType<EnemyCreator>();
        menuPause.SetActive(false);

        pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pause)
            {
                Paused();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Restart()
    {
        foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag(EnemyTag))
        {
            Destroy(Enemies);
        }

        Application.LoadLevel(index: Application.loadedLevel);
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
