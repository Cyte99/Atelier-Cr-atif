using System;
using UnityEngine;

public class RotateLeft90Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private float turnDegrees = 90f;
    [SerializeField] private PuzzleManager puzzleManager;

    public void Interact(Player_Mouvement interactor)
    {
        transform.Rotate(0f, -turnDegrees, 0f, Space.World);

        //re-check after rotating
        if (puzzleManager != null)
        {
            puzzleManager.CheckPuzzle();
        }
    }
}