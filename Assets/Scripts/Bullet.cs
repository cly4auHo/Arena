using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(RedEnemyTag) || other.CompareTag(BlueEnemyTag))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
