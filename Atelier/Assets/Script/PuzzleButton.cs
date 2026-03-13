using UnityEngine;

public class PuzzleButton : MonoBehaviour, IInteractable
{
    [SerializeField] private int buttonID;
    [SerializeField] private ButtonSequenceManager buttonManager;

    public void Interact(Player_Mouvement interactor)
    {
        if (buttonManager != null)
        {
            buttonManager.PressButton(buttonID);
        }
    }
}