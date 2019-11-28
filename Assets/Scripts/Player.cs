using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int health = 100;
    private int strength = 50;
    private int fullHP = 100;
    private int fullStrength = 100;

    [SerializeField] private Text healtText;
    [SerializeField] private Text strengthText;

    void Start()
    {
        healtText.enabled = true;
        strengthText.enabled = true;

        healtText.text = "Health " + health.ToString() + "/100";
        strengthText.text = "Strength " + strength.ToString() + "/100";
    }

    void Update()
    {
        if (health <= 0)
        {
            healtText.enabled = false;
            strengthText.enabled = false;
        }
    }

    public int GetHealth()
    {
        return health;
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
    }

    public bool IsAlive()
    {
        if (health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetStrengt()
    {
        return strength;
    }

    public void SetStrengt(int strength)
    {
        this.strength = strength;
        strengthText.text = "Strength " + this.strength.ToString() + "/100";
    }

    public void StrengtUp(int value)
    {
        strength = Mathf.Min(strength + value, fullStrength);
        strengthText.text = "Strength " + this.strength.ToString() + "/100";
    }

    public void StrengtLess(int fade)
    {
        strength = Mathf.Max(0, strength - fade);
        strengthText.text = "Strength " + this.strength.ToString() + "/100";
    }
}
