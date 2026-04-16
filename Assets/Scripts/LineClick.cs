using UnityEngine;
using UnityEngine.InputSystem;

public class LineClick : MonoBehaviour
{
    public Node nodeA;
    public Node nodeB;

    void Update()
    {
        if (!LineManager.Instance) return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Ligne cliquée !");
                LineManager.Instance.RemoveConnection(this);
            }
        }
    }
}