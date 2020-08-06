using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    [Header("Settings of Player")]
    [Range(0, 500)]
    [SerializeField] private int health = 100;
    [Range(0, 100)]
    [SerializeField] private int strength = 50;
    [Range(0, 500)]
    [SerializeField] private int fullHP = 100;
    [Range(0, 100)]
    [SerializeField] private int fullStrength = 100;
    [Header("Bottom Text")]
    [SerializeField] private Text healtText;
    [SerializeField] private Text strengthText;
    public Action Ult;
    public Action Die;
    private const string pauseManagerTag = "PauseManager";

    public int Health => health;
    public int FullHealth => fullHP;
    public bool IsAlive => health > 0;

    private void Start()
    {
        health = 100;
        healtText.enabled = true;
        strengthText.enabled = true;
        healtText.text = "Health " + health.ToString() + "/100";
        strengthText.text = "Strength " + strength.ToString() + "/100";
        GameObject.FindGameObjectWithTag(pauseManagerTag).GetComponent<Pause>().RestartGame += Restart;
    }

    public void Healing(int heal)
    {
        health = Mathf.Min(health + heal, fullHP);
        healtText.text = "Health " + health.ToString() + "/100";
    }

    public void Damage(int damage)
    {
        health -= damage;
        healtText.text = "Health " + health.ToString() + "/100";

        if (health <= 0)
            Die?.Invoke();
    }

    public void StrengtUp(int value)
    {
        strength = Mathf.Min(strength + value, fullStrength);
        strengthText.text = "Strength " + strength.ToString() + "/100";
    }

    public void StrengtLess(int fade)
    {
        strength = Mathf.Max(0, strength - fade);
        strengthText.text = "Strength " + strength.ToString() + "/100";
    }

    public void Ultimate()
    {
        if (strength >= fullStrength)
        {
            Ult?.Invoke();
            strength = 0;
        }
    }

    private void Restart()
    {
        Healing(fullHP);
    }
}
