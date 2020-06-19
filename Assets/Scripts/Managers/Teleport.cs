using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform[] TeleportZones;
    private const string PlayerTag = "Player";
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            other.transform.rotation = NewPosition(TeleportZones).rotation;
            other.transform.position = NewPosition(TeleportZones).position;

            EnemyBullet[] enemyBullets = FindObjectsOfType<EnemyBullet>();

            if (enemyBullets.Length != 0)
                foreach (EnemyBullet bullets in enemyBullets)
                    bullets.AfterTeleport(NewPosition(TeleportZones));
        }
    }

    private Transform NewPosition(Transform[] TeleportZones)
    {
        Transform bestTarget = null;
        float farDistanceSqr = 0;
        Vector3 currentPosition = player.transform.position;

        foreach (Transform potentialTarget in TeleportZones)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget > farDistanceSqr)
            {
                farDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
