using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ButtonSequenceManager : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private bool doorOpened = false;
    public bool IsPuzzleSolved => doorOpened;

    [SerializeField] private int[] correctOrder;
    private int currentIndex = 0;

    [Header("Sons")]
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip puzzleSolvedSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PressButton(int buttonID)
    {
        if (buttonID == correctOrder[currentIndex])
        {
            Debug.Log("Correct One");
            currentIndex++;

            if (currentIndex >= correctOrder.Length)
            {
                OpenDoor();
            }
            else
            {
                audioSource.PlayOneShot(correctSound);
            }
        }
        else
        {
            Debug.Log("Wrong button");
            audioSource.PlayOneShot(wrongSound);
            currentIndex = 0;
        }
    }

    private void OpenDoor()
    {
        doorOpened = true;
        Debug.Log("PUZZLE COMPLETE - DOOR OPENED");
        audioSource.PlayOneShot(puzzleSolvedSound);

        if (door != null)
            door.SetActive(false);
    }
}
