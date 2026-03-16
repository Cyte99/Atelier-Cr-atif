using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZoneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private ButtonSequenceManager buttonManager;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // If there is another level, load it
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Loading next level...");
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("MISSION COMPLETE");

            if (endScreen != null)
                endScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;
            
        }

    }
}