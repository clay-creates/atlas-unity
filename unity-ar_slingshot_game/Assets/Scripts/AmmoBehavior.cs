using UnityEngine;

public class AmmoBehavior : MonoBehaviour
{
    private GameManager gameManager;

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            gameManager?.TargetHit();
            Destroy(gameObject);
        }
    }
}
