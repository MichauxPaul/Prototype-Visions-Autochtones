using UnityEngine;
using UnityEngine.InputSystem;

public class Node : MonoBehaviour
{
    public int nodeID;
    public string matchID;

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // 🔥 On ignore les lignes et UI
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Node cliqué : " + nodeID);
                    LineManager.Instance.SelectNode(this);
                    return;
                }
            }
        }
    }
}