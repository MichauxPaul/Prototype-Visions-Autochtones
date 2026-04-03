using UnityEngine;
using TMPro;

public class LinkGameManager : MonoBehaviour
{
    [Header("Références")]
    public LineManager lineManager;
    public int totalConnectionsNeeded;

    [Header("UI")]
    public GameObject startPanel;
    public GameObject background;

    public GameObject validateButton;
    public GameObject retryButton;
    public GameObject ReturnMiniGameChoiseButton;

    public TMP_Text resultText;

    private bool gameStarted = false;

    void Start()
    {
        SetGameState(false);

        validateButton.SetActive(false);
        retryButton.SetActive(false);
        ReturnMiniGameChoiseButton.SetActive(false);
        resultText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameStarted) return;

        //on verifie si toutes les connections on été faite
        bool allConnected = lineManager.AllConnected(totalConnectionsNeeded);

        //si elles ont toute été faite on affiche le bouton pour vérifier
        validateButton.SetActive(allConnected);
    }

    //on lance le jeu
    public void StartGame()
    {
        gameStarted = true;
        SetGameState(true);
    }

    void SetGameState(bool state)
    {
        startPanel.SetActive(!state);
        background.SetActive(!state);
    }

    // ✅ Vérifier
    public void Validate()
    {
        //avec le line manager on calcule combien de connexions sont bonnes et combien sont fausses
        int correct, wrong;
        lineManager.GetResults(out correct, out wrong);

        //on affiche le résultat
        resultText.gameObject.SetActive(true);
        resultText.text = "Liaison correct : " + correct + " | Lisaisons fausse : " + wrong;

        //si il n'y a pas d'erreur, on affiche le bouton d'explication sinon on affiche le bouton pour recommencer
        if (wrong == 0)
        {
            ReturnMiniGameChoiseButton.SetActive(true);
        }
        else
        {
            retryButton.SetActive(true);
        }

        validateButton.SetActive(false);

        //on bloque les connexions
        lineManager.SetInteraction(false);
    }

    // afficher le ui pour corriger
    public void Retry()
    {
        resultText.gameObject.SetActive(false);

        retryButton.SetActive(false);
        ReturnMiniGameChoiseButton.SetActive(false);

        lineManager.ResetConnections();
        lineManager.SetInteraction(true);

        validateButton.SetActive(false);
    }
}