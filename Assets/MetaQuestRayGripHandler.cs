using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MetaQuestRayGripHandler : MonoBehaviour
{
    public InputActionProperty gripAction;
    public Camera rayOriginCamera;
    public LayerMask interactableLayer;

    private void Update()
    {
        if (gripAction.action.WasPressedThisFrame())
        {
            TryPressButton();
        }
    }

    private void TryPressButton()
    {
        Ray ray = new Ray(rayOriginCamera.transform.position, rayOriginCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, interactableLayer))
        {
            var button = hit.collider.GetComponent<UnityEngine.UI.Button>();
            if (button != null)
            {
                ExecuteEvents.Execute(button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
                Debug.Log($"Pressed button: {button.name}");
            }
        }
    }
}
