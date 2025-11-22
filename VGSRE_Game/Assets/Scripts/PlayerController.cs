using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    bool isDragging = false;
    Vector3 currentMousePosition = Vector3.zero;
    Vector3 originalMousePosition = Vector3.zero;
    Vector3 originalCameraPosition = Vector3.zero;

    [SerializeField]
    float cameraMoveSpeed;

    private void Update()
    {
        if (isDragging)
        {
            Vector3 cameraOffset = new Vector3((currentMousePosition.x - originalMousePosition.x), 0f, 0f) * cameraMoveSpeed;

            Camera.main.transform.position = (originalCameraPosition - cameraOffset);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit) return;
        else if (rayHit.collider.gameObject.CompareTag(GameManager.instance.m_ObjectTag))
        {
            Debug.Log($"'{rayHit.collider.gameObject.name}' was clicked");
        }
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        isDragging = context.performed;

        originalMousePosition = currentMousePosition;
        originalCameraPosition = Camera.main.transform.position;
    }

    public void SetMousePosition(InputAction.CallbackContext context)
    {
        currentMousePosition = new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 0f);
    }
}
