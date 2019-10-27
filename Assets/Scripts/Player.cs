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
    }

    void Update()
    {
        if (health > 0)
        {
            healtText.text = "Health " + health.ToString() + "/100";
            strengthText.text = "Strength " + strength.ToString() + "/100";
        }
        else //death 
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
    }

    public void Damage(int damage)
    {
        health -= damage;
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
    }

    public void StrengtUp(int value)
    {
        strength = Mathf.Min(strength + value, fullStrength);
    }

    public void StrengtLess(int fade)
    {
        strength = Mathf.Max(0, strength - fade);
    }
}
