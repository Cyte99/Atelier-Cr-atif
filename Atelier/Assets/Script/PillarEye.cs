using UnityEngine;

public class PillarEye : MonoBehaviour
{
    [SerializeField] private Transform eyeTransform;  // the "eyes"
    [SerializeField] private Transform centerPoint;   // object in the middle
    [SerializeField] private float alignmentThreshold = 10f; 

    public bool IsAligned()
    {
        if (eyeTransform == null || centerPoint == null)
            return false;

        Vector3 dirToCenter = (centerPoint.position - eyeTransform.position).normalized;

        float angle = Vector3.Angle(eyeTransform.forward, dirToCenter);

        return angle <= alignmentThreshold;
    }
}