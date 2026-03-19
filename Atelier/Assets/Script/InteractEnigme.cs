using UnityEngine;

public class InteractLivre : MonoBehaviour
{
    public GameObject uiEnigme;
    private bool joueurProche = false;

    void Update()
    {
        if (joueurProche && Input.GetKeyDown(KeyCode.E))
        {
            if (uiEnigme.activeSelf)
            {
                FermerEnigme();
            }
            else
            {
                OuvrirEnigme();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joueurProche = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joueurProche = false;
        }
    }
    void OuvrirEnigme()
    {
        uiEnigme.SetActive(true);
        Time.timeScale = 0f; // pause
    }

    void FermerEnigme()
    {
        uiEnigme.SetActive(false);
        Time.timeScale = 1f; // reprend
    }
}
