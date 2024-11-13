using UnityEngine;

public class AmmoBehavior : MonoBehaviour
{
    public GameManager gameManager;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Plane"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }
}
