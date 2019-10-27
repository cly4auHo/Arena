using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject enemyCreator;

    private Player player;

    private bool pause;

    private const string EnemyTag = "Enemy";

    void Start()
    {
        player = FindObjectOfType<Player>();
        menuPause.SetActive(false);

        pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && player.IsAlive())
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

        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1f;
    }

    void Paused()
    {
        menuPause.SetActive(true);
        enemyCreator.SetActive(false);
        pause = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    void Resume()
    {
        menuPause.SetActive(false);
        enemyCreator.SetActive(true);
        pause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    public bool IsPaused()
    {
        return pause;
    }
}
