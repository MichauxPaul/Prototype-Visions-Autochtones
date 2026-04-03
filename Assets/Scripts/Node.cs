using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerClickHandler
{
    public string nodeID;
    public string matchID;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Node cliqué !");
        LineManager.Instance.SelectNode(this);
    }
}