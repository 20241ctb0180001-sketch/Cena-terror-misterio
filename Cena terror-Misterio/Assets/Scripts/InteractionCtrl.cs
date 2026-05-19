using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;

namespace DefaltNamespace
{
    public class InteractionCtrl : MonoBehaviour
    {
        [SerializeField] Camera playerCam;
        [SerializeField] TextMeshProUGUI interactionText;
        [SerializeField] float InteractionDistance = 2f;
        Iinteractable currentTargetedInteractable;
        public void Update()
        {
            UpdateCurrInteractable();
            UpdateInteractionText();
            CheckForInteInput();
        }

        void UpdateCurrInteractable()
        {
            var ray = playerCam.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            Physics.Raycast(ray, out var hit, InteractionDistance);
            currentTargetedInteractable = hit.collider?.GetComponent<Iinteractable>();
        }

        void UpdateInteractionText()
        {
            if(currentTargetedInteractable == null)
            {
                interactionText.text = string.Empty;
                return;
            }
            interactionText.text = currentTargetedInteractable.InteractMessage;
        }

        void CheckForInteInput()
        {
            if(Keyboard.current.eKey.wasPressedThisFrame && currentTargetedInteractable != null)
            {
                currentTargetedInteractable.interact();
            }
        }
    }
}