using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int health;
    private int strength;
    [SerializeField] private Text healtText;
    [SerializeField] private Text strengthText;

    void Start()
    {
        health = 100;
        strength = 50;
    }

    void Update()
    {
        healtText.text = "Health " + health.ToString() + "/100";
        strengthText.text = "Strength " + strength.ToString() + "/100";
    }

    public int GetStrengt()
    {
        return strength;
    }

    public void SetStrengt(int strength)
    {
        this.strength = strength;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }
}
