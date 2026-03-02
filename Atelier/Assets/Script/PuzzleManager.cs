using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<PillarEye> pillars;
    [SerializeField] private GameObject door;
    private bool doorOpened = false;

    public void CheckPuzzle()
    {
        if (doorOpened) return;

        foreach (var pillar in pillars)
        {
            if (!pillar.IsAligned())
                return;
        }

        OpenDoor();
    }

    private void OpenDoor()
    {
        doorOpened = true;
        Debug.Log("PUZZLE COMPLETE - DOOR OPENED");

        if (door != null)
            door.SetActive(false);
    }
}