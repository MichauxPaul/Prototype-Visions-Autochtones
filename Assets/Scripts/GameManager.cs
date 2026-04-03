using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public DropZone[] dropZones;
    public GameObject validateButton;
    public TMP_Text resultText;
    public GameObject explicationButton;
    public GameObject retryButton;

    public GameObject startPanel;   
    public GameObject background;   
    public DragAndDrop[] draggableObjects;


    private bool gameStarted = false;

    //on bloque le fait de pouvoir déplacer les objects et on cache tout le texte de fin
    void Start()
    {
        SetGameState(false); 

        validateButton.SetActive(false);
        resultText.gameObject.SetActive(false);
        explicationButton.SetActive(false);
        retryButton.SetActive(false);
    }

    void Update()
    {
        //tant que le joueur n'a pas cliquer sur le bouton démarrer on ne fait rien, on bloque tout
        if (!gameStarted) return; 

        bool allFilled = true;

        //vérification de chaque zone si elle est remplie ou pas
        foreach (DropZone zone in dropZones)
        {
            if (!zone.IsFilled())
            {
                allFilled = false;
                break;
            }
        }

        //si toutes les drop zone sont remplie on affiche le bouton pour valider 
        validateButton.SetActive(allFilled);
    }

    public void Validate()
    {
        //compteur de réponse juste et fausse
        int correct = 0;
        int wrong = 0;

        //pour chaque zone, on augmente les variables juste et fausse en fonction du placement de nos objects
        foreach (DropZone zone in dropZones)
        {
            if (zone.IsCorrect())
                correct++;
            else
                wrong++;
        }

        //on affiche le texte
        resultText.gameObject.SetActive(true);
        resultText.text = "Correct : " + correct + " | Faux : " + wrong;

        //on vérifie le nombre d'élément qui sont bien placé et si tout est bien placer on affiche le bouton pour aller vers les explications sinon on affiche le bouton pour recommencer a placer les objects
        if (wrong == 0)
        {
            explicationButton.SetActive(true);
            
        }
        else
        {
            retryButton.SetActive(true);
        }

        // on bloque les objets après vérification
        foreach (DragAndDrop obj in draggableObjects)
        {
            obj.enabled = false;
        }

        validateButton.SetActive(false);
    }
    //quand le joueur clique sur le bouton démarrer, on commence le mini jeu et on fait que les objects sont actif
    public void StartGame()
    {
        gameStarted = true;
        SetGameState(true);
    }

    //si le jeu commence, on cache le menu d'explication et on fait que les object sont déplacable
    void SetGameState(bool state)
    {
        startPanel.SetActive(!state);
        background.SetActive(!state);

        foreach (DragAndDrop obj in draggableObjects)
        {
            obj.enabled = state;
        }
    }

    //fonction apeller via le vouton recommencer
    public void Retry()
    {
        //une fois que l'on a cliquer sur le bouton recommencer, on cache tout les textes
        resultText.gameObject.SetActive(false);

        explicationButton.SetActive(false);
        retryButton.SetActive(false);

        // et on réactive les objets pour pouvoir les redéplacer
        foreach (DragAndDrop obj in draggableObjects)
        {
            obj.enabled = true;
        }

        // on remet le bouton vérifier
        validateButton.SetActive(true);
    }
}