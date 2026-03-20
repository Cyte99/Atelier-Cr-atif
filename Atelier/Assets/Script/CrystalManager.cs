using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    [SerializeField] private List<CrystalRotation> crystals;
    [SerializeField] private GameObject door;

    private bool solved = false;

    public void CheckPuzzle()
    {
        if (solved) return;

        foreach (var crystal in crystals)
        {
            if (crystal == null || !crystal.IsCorrectlyRotated())
                return;
        }

        solved = true;

        if (door != null)
            door.SetActive(false);

        Debug.Log("Crystal puzzle solved");
    }
}