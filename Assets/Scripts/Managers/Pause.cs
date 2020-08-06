using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public Action RestartGame;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private EnemyCreator enemyCreator;
    [SerializeField] private PlayerShot playerShot;
    [SerializeField] private Player player;
    private bool pause;

    private void Start()
    {
        menuPause.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && player.IsAlive)
        {
            if (pause)
                Resume();
            else
                Paused();
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        RestartGame?.Invoke();
    }

    private void Paused()
    {
        pause = true;
        playerShot.StopAim();
        Time.timeScale = 0f;
        menuPause.SetActive(true);
        enemyCreator.SetSpawn(false);
    }

    private void Resume()
    {
        pause = false;
        playerShot.GoAim();
        Time.timeScale = 1f;
        menuPause.SetActive(false);
        enemyCreator.SetSpawn(true);
    }
}
