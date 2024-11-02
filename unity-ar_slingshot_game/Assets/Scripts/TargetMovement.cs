using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetMovement : MonoBehaviour
{
    public float speed = 0.5f;
    private Vector3 direction;
    private ARPlane plane;

    public void SetPlaneBounds(ARPlane selectedPlane)
    {
        plane = selectedPlane;
        SetRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (IsOutOfBounds())
        {
            SetRandomDirection();
            CorrectPositionWithinBounds();
        }
    }

    private void SetRandomDirection()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    private bool IsOutOfBounds()
    {
        Vector3 localPosition = plane.transform.InverseTransformPoint(transform.position);
        Vector2 planeSize = plane.size;

        return Mathf.Abs(localPosition.x) > planeSize.x / 2 || Mathf.Abs(localPosition.z) > planeSize.y / 2;
    }

    private void CorrectPositionWithinBounds()
    {
        Vector3 localPosition = plane.transform.InverseTransformPoint(transform.position);
        Vector2 planeSize = plane.size;

        localPosition.x = Mathf.Clamp(localPosition.x, -planeSize.x / 2, planeSize.x / 2);
        localPosition.z = Mathf.Clamp(localPosition.z, -planeSize.y / 2, planeSize.y / 2);

        transform.position = plane.transform.TransformPoint(localPosition);
    }
}
