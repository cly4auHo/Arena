using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Player player;
    private EnemyBullet[] enemyBullets;

    [SerializeField] private Transform[] TeleportZones;
    private const string PlayerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            player = other.GetComponent<Player>();
            enemyBullets = FindObjectsOfType<EnemyBullet>();

            player.transform.rotation = NewPosition(TeleportZones).rotation;
            other.transform.position = NewPosition(TeleportZones).position;

            if (enemyBullets.Length != 0)
            {
                foreach (EnemyBullet bullets in enemyBullets)
                {
                    bullets.AfterTeleport(NewPosition(TeleportZones));
                }
            }
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
