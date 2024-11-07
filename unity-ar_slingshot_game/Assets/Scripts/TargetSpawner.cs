using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private int targetCount = 5;
    private ARPlane selectedPlane;
    private Camera arCamera;

    public void SetSelectedPlane(ARPlane plane)
    {
        selectedPlane = plane;
    }

    public void SetARCamera(Camera camera)
    {
        arCamera = camera;
    }

    public void SpawnTargets()
    {
        if (selectedPlane == null || arCamera == null)
        {
            Debug.LogError("Selected plane or AR Camera is not set.");
            return;
        }

        for (int i = 0; i < targetCount; i++)
        {
            Vector3 spawnPosition = GetRandomPointOnPlane(selectedPlane);
            GameObject target = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);

            float scaleFactor = CalculateScaleFactor(selectedPlane, arCamera);
            target.transform.localScale = Vector3.one * scaleFactor;

            TargetMovement targetMovement = target.AddComponent<TargetMovement>();
            targetMovement.SetPlaneBounds(selectedPlane);
        }
    }

    private Vector3 GetRandomPointOnPlane(ARPlane plane)
    {
        Vector3 planeCenter = plane.center;
        Vector2 planeSize = plane.size;

        float randomX = Random.Range(-planeSize.x / 2, planeSize.x / 2);
        float randomZ = Random.Range(-planeSize.y / 2, planeSize.y / 2);

        return planeCenter + new Vector3(randomX, 0, randomZ);
    }

    private float CalculateScaleFactor(ARPlane plane, Camera camera)
    {
        float distance = Vector3.Distance(camera.transform.position, plane.center);
        return Mathf.Clamp(1 / distance, 0.05f, 0.2f);
    }

    public int GetTargetCount()
    {
        return targetCount;
    }
}
