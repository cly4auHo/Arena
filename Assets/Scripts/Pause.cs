﻿using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    private EnemyCreator enemyCreator;

    private bool pause;
    private const string EnemyTag = "Enemy";

    void Start()
    {
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

        //[System.Obsolete]
        Application.LoadLevel(index: Application.loadedLevel);
        Time.timeScale = 1f;
    }

    void Paused()
    {
        menuPause.SetActive(true);
        enemyCreator.SetSpawn(false);
        pause = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    void Resume()
    {
        menuPause.SetActive(false);
        enemyCreator.SetSpawn(true);
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
