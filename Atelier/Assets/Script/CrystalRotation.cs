using UnityEngine;

public class CrystalRotation : MonoBehaviour, IInteractable
{
    [Header("Rotation")]
    [SerializeField] private float turnDegrees = 45f;

    [Header("Correct Direction")]
    [SerializeField] private float correctYAngle;
    [SerializeField] private float angleTolerance = 1f;

    [Header("Beam")]
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Puzzle Manager")]
    [SerializeField] private CrystalManager crystalManager;

    public void Interact(Player_Mouvement interactor)
    {
        transform.Rotate(0f, -turnDegrees, 0f, Space.Self);

        Vector3 angles = transform.localEulerAngles;
        angles.y = Mathf.Round(angles.y / turnDegrees) * turnDegrees;
        transform.localEulerAngles = angles;

        CheckAlignment();

        if (crystalManager != null)
            crystalManager.CheckPuzzle();
    }

    private void Start()
    {
        CheckAlignment();

        if (crystalManager != null)
            crystalManager.CheckPuzzle();
    }

    public bool IsCorrectlyRotated()
    {
        float currentY = transform.localEulerAngles.y;
        float delta = Mathf.Abs(Mathf.DeltaAngle(currentY, correctYAngle));
        return delta <= angleTolerance;
    }

    public void CheckAlignment()
    {
        bool correct = IsCorrectlyRotated();

        if (lineRenderer != null)
            lineRenderer.enabled = correct;
    }
}