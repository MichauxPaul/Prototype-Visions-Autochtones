using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public DropZone[] dropZones;
    public GameObject validateButton;
    public TMP_Text resultText;
    public GameObject ReturnMiniGameChoise;
    public GameObject retryButton;

    public GameObject startPanel;
    public GameObject background;
    public DragAndDrop[] draggableObjects;

    private bool gameStarted = false;
    private bool canValidate = false;

    void Start()
    {
        SetGameState(false);

        validateButton.SetActive(false);
        resultText.gameObject.SetActive(false);
        ReturnMiniGameChoise.SetActive(false);
        retryButton.SetActive(false);
    }

    void Update()
    {
        if (!gameStarted) return;

        // 🔥 si déjà activé, on ne vérifie plus
        if (canValidate) return;

        bool allFilled = true;

        foreach (DropZone zone in dropZones)
        {
            if (!zone.IsFilled())
            {
                allFilled = false;
                break;
            }
        }

        // 🔥 on active UNE SEULE FOIS
        if (allFilled)
        {
            canValidate = true;
            validateButton.SetActive(true);
        }
    }

    public void Validate()
    {
        int correct = 0;
        int wrong = 0;

        foreach (DropZone zone in dropZones)
        {
            if (zone.IsCorrect())
                correct++;
            else
                wrong++;
        }

        resultText.gameObject.SetActive(true);
        resultText.text = "Correct : " + correct + " | Faux : " + wrong;

        if (wrong == 0)
        {
            ReturnMiniGameChoise.SetActive(true);
        }
        else
        {
            retryButton.SetActive(true);
        }

        // 🔒 on bloque les objets
        foreach (DragAndDrop obj in draggableObjects)
        {
            obj.enabled = false;
        }

        validateButton.SetActive(false);
    }

    public void StartGame()
    {
        gameStarted = true;
        SetGameState(true);
    }

    void SetGameState(bool state)
    {
        startPanel.SetActive(!state);
        background.SetActive(!state);

        foreach (DragAndDrop obj in draggableObjects)
        {
            obj.enabled = state;
        }
    }

    public void Retry()
    {
        resultText.gameObject.SetActive(false);
        ReturnMiniGameChoise.SetActive(false);
        retryButton.SetActive(false);

        foreach (DragAndDrop obj in draggableObjects)
        {
            obj.enabled = true;
        }

        // 🔥 reset propre
        canValidate = false;
        validateButton.SetActive(false);
    }
}