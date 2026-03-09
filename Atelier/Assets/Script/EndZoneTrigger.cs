using UnityEngine;

public class EndZoneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private PuzzleManager puzzleManager;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (!other.CompareTag("Player"))
            return;

        if (puzzleManager == null || !puzzleManager.IsPuzzleSolved)
        {
            Debug.Log("Puzzle not completed yet.");
            return;
        }

        triggered = true;

        Debug.Log("MISSION COMPLETE");

        if (endScreen != null)
            endScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }
}