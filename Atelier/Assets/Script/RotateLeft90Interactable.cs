using System;
using UnityEngine;

public class RotateLeft90Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private float turnDegrees = 90f;
    [SerializeField] private PuzzleManager puzzleManager;

    [Header("Son")]
    [SerializeField] private AudioClip rotateSound;
    [SerializeField][Range(0f, 1f)] private float volume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Interact(Player_Mouvement interactor)
    {
        transform.Rotate(0f, -turnDegrees, 0f, Space.World);

        audioSource.PlayOneShot(rotateSound, volume);

        if (puzzleManager != null)
        {
            puzzleManager.CheckPuzzle();
        }
    }
}