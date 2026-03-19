using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<PillarEye> pillars;
    [SerializeField] private GameObject door;
    private bool doorOpened = false;
    public bool IsPuzzleSolved => doorOpened;

    [Header("Son")]
    [SerializeField] private AudioClip puzzleSolvedSound;
    [SerializeField][Range(0f, 1f)] private float volume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void CheckPuzzle()
    {
        if (doorOpened) return;

        foreach (var pillar in pillars)
        {
            if (!pillar.IsAligned())
            {
                print($"{pillar.name} not aligned");
                return;
            }
            Debug.Log("all pillar alligned");
        }
        OpenDoor();
    }

    private void OpenDoor()
    {
        doorOpened = true;
        Debug.Log("PUZZLE COMPLETE - DOOR OPENED");
        audioSource.PlayOneShot(puzzleSolvedSound, volume);

        if (door != null)
            door.SetActive(false);
    }
}