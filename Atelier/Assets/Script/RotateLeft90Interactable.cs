using UnityEngine;

public class RotateLeft90Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private float turnDegrees = 90f;

    public void Interact(Player_Mouvement interactor)
    {
        transform.Rotate(0f, -turnDegrees, 0f, Space.World);
    }
}