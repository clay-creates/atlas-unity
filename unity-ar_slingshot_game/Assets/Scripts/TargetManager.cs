using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [Header("Target Configuration")]
    public GameObject targetPrefab;
    public int numberOfTargets = 5;
    public float movementSpeed = 1f;
    private GameObject[] targets;

    private void Start()
    {
        SpawnTargets();
    }

    private void SpawnTargets()
    {
        targets = new GameObject[numberOfTargets];
        for (int i = 0; i < numberOfTargets; i++)
        {
            Vector3 spawnPosition = GetRandomPositionOnPlane();
            targets[i] = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-5, 5), 0.5f, Random.Range(-5, 5));
    }

    private void Update()
    {
        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                Vector3 movement = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized * movementSpeed * Time.deltaTime;
                target.transform.Translate(movement);
            }
        }
    }
}
