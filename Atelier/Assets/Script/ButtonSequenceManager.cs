using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ButtonSequenceManager : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private bool doorOpened = false;
    public bool IsPuzzleSolved => doorOpened;

    [SerializeField] private int[] correctOrder;
    private int currentIndex = 0;

    public void PressButton(int buttonID)
    {
        if(buttonID == correctOrder[currentIndex])
        {
            Debug.Log("Correct One");

            currentIndex++;

            if(currentIndex >= correctOrder.Length)
            {
                OpenDoor();
            }
        }
        else
        {
            Debug.Log("Wrong button");
            currentIndex = 0;
        }
    }

    private void OpenDoor()
    {
        doorOpened = true;
        Debug.Log("PUZZLE COMPLETE - DOOR OPENED");

        if (door != null)
            door.SetActive(false);
    }
}
