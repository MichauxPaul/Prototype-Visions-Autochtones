using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineManager : MonoBehaviour
{
    public static LineManager Instance;

    public LineRenderer linePrefab;

    private LineRenderer currentLine;
    private Node startNode;

    // 🔥 on stocke aussi la ligne associée
    private List<(Node, Node, LineClick)> connections = new List<(Node, Node, LineClick)>();

    private bool canInteract = true;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (currentLine != null)
        {
            if (Mouse.current != null)
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                worldPos.z = 0;

                currentLine.SetPosition(1, worldPos);
            }
        }
    }

    public void SelectNode(Node node)
    {
        if (!canInteract) return;

        if (startNode == null)
        {
            startNode = node;

            currentLine = Instantiate(linePrefab, transform);
            currentLine.positionCount = 2;

            currentLine.SetPosition(0, node.transform.position);
            currentLine.SetPosition(1, node.transform.position);
        }
        else
        {
            currentLine.SetPosition(1, node.transform.position);

            // 🔥 ajout du script de clic
            LineClick lineClick = currentLine.gameObject.AddComponent<LineClick>();
            lineClick.nodeA = startNode;
            lineClick.nodeB = node;

            CheckConnection(startNode, node, lineClick);

            startNode = null;
            currentLine = null;
        }
    }

    void CheckConnection(Node a, Node b, LineClick line)
    {
        if (a == b)
        {
            Debug.Log("Connexion invalide (même node)");
            return;
        }

        foreach (var pair in connections)
        {
            if ((pair.Item1 == a && pair.Item2 == b) ||
                (pair.Item1 == b && pair.Item2 == a))
            {
                Debug.Log("Connexion déjà faite");
                return;
            }
        }

        connections.Add((a, b, line));

        Debug.Log("Connexion : " + a.nodeID + " → " + b.nodeID);
    }

    public void RemoveConnection(LineClick line)
    {
        if (!canInteract) return;

        for (int i = 0; i < connections.Count; i++)
        {
            if (connections[i].Item3 == line)
            {
                connections.RemoveAt(i);
                break;
            }
        }

        Destroy(line.gameObject);
    }

    public bool AllConnected(int totalConnectionsNeeded)
    {
        return connections.Count >= totalConnectionsNeeded;
    }

    public void GetResults(out int correct, out int wrong)
    {
        correct = 0;
        wrong = 0;

        foreach (var pair in connections)
        {
            if (pair.Item1.matchID == pair.Item2.matchID)
                correct++;
            else
                wrong++;
        }
    }

    public void SetInteraction(bool state)
    {
        canInteract = state;
    }

    public void ResetConnections()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        connections.Clear();
    }
}