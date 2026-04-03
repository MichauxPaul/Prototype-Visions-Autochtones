using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineManager : MonoBehaviour
{
    public static LineManager Instance;

    public LineRenderer linePrefab;

    private LineRenderer currentLine;
    private Node startNode;
    //liste de toutes les connections faite
    private List<(Node, Node)> connections = new List<(Node, Node)>();

    //on rend le script accessible partout
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        //Si une ligne est en cours
        if (currentLine != null)
        {
            //on convertit la position de la souris en position dans le monde
            if (Mouse.current != null)
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                worldPos.z = 0;

                currentLine.SetPosition(1, worldPos);
            }
        }
    }

    //Fonction appelée quand on clique sur un Node (un point dans notre mini jeu)
    public void SelectNode(Node node)
    {
        //Si le jeu est bloqué on ignore les clics
        if (!canInteract) return;

        if (startNode == null)
        {
            // Premier clic, creer le début de la ligne et on enregistre le point comme point de départ
            startNode = node;

            //On crée une nouvelle ligne avec 2 points
            currentLine = Instantiate(linePrefab);
            currentLine.positionCount = 2;

            //La ligne commence au point cliqué
            currentLine.SetPosition(0, node.transform.position);
            currentLine.SetPosition(1, node.transform.position);
        }
        //Un point est déjà sélectionné
        else
        {
            //Deuxième clic, on créer la fin de la ligne
            currentLine.SetPosition(1, node.transform.position);

            //On enregistre la connexion
            CheckConnection(startNode, node);

            // reset : prêt pour une nouvelle connexion
            startNode = null;
            currentLine = null;
        }
    }

    void CheckConnection(Node a, Node b)
    {
        //On stocke la connexion dans la liste
        connections.Add((a, b));
        Debug.Log("Connexion : " + a.nodeID + " → " + b.nodeID);
    }

    public bool AllConnected(int totalConnectionsNeeded)
    {
        //on vérifie si toutes les connexions sont faites
        return connections.Count >= totalConnectionsNeeded;
    }

    //Calcule les bonnes et mauvaises réponses
    public void GetResults(out int correct, out int wrong)
    {
        //initialise les compteurs
        correct = 0;
        wrong = 0;

        //pour chaque connexion
        foreach (var pair in connections)
        {
            //compare les IDs, si les 2 match = bon, sinon faux
            if (pair.Item1.matchID == pair.Item2.matchID)
                correct++;
            else
                wrong++;
        }
    }

    //contrôle si le joueur peut interagir
    private bool canInteract = true;

    //permet si c'est false de bloquer le jeu sinon si c'est true d'autoriser le jeu
    public void SetInteraction(bool state)
    {
        canInteract = state;
    }

    //on supprime toutes les lignes (enfants du manager)
    public void ResetConnections()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        connections.Clear();
    }
}