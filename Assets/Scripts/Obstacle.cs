using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

public class ClimbingObstacle : MonoBehaviour
{
    public XRDirectInteractor leftHandInteractor;
    public XRDirectInteractor rightHandInteractor;
    public float gripDisableDuration = 1.5f;
    public float hapticIntensity = 0.8f;
    public float hapticDuration = 0.3f;
    public ClimbInteractable handholds;
    void Start()
    {
        FindHandInteractors();
        // Debug.Log($"[CS135] Left: {leftHandInteractor}");
        // Debug.Log($"[CS135] Right: {rightHandInteractor}");
    }

    void FindHandInteractors()
    {
        XRDirectInteractor[] interactors = FindObjectsByType<XRDirectInteractor>(FindObjectsSortMode.None);

        foreach (var i in interactors)
        {
            Debug.Log($"[CS135] Found XRDirectInteractor: {i.name}");
            string n = i.gameObject.name.ToLower();
            if (n.Contains("left")) leftHandInteractor = i;
            if (n.Contains("right")) rightHandInteractor = i;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Debug.Log("[CS135] Player Collision");
        // Vector3 hitPoint = collision.contacts[0].point;

        // XRDirectInteractor hitHand = GetCloserHand(hitPoint);
        // XRDirectInteractor otherHand =
        //     (hitHand == leftHandInteractor) ? rightHandInteractor : leftHandInteractor;
        // Debug.Log($"[CS135]", hitHand);
        // // Disable the hit hand's grip
        // if (hitHand != null)
            // StartCoroutine(DisableGrip(rightHandInteractor, gripDisableDuration));

        // Briefly stagger the other hand
        // StartCoroutine(DisableGrip(leftHandInteractor, gripDisableDuration));

        // TriggerHaptics(leftHandInteractor);
        // TriggerHaptics(rightHandInteractor);
        DisableHandholds(gripDisableDuration);
        Destroy(gameObject);
    }

    XRDirectInteractor GetCloserHand(Vector3 point)
    {
        if (leftHandInteractor == null) return rightHandInteractor;
        if (rightHandInteractor == null) return leftHandInteractor;

        float leftDist = Vector3.Distance(point, leftHandInteractor.transform.position);
        float rightDist = Vector3.Distance(point, rightHandInteractor.transform.position);
        return leftDist < rightDist ? leftHandInteractor : rightHandInteractor;
    }

    System.Collections.IEnumerator DisableGrip(XRDirectInteractor hand, float duration)
    {
        Debug.Log("[CS135] Grip disable called started");
        if (hand.hasSelection)
        {
            hand.interactionManager.SelectExit((IXRSelectInteractor)hand, hand.firstInteractableSelected);
        }

        hand.enabled = false;
        Debug.Log("[CS135] Grib Disabled");
        yield return new WaitForSeconds(duration);
        hand.enabled = true;
        Debug.Log("[CS135] Grib Enabled");
    }

    System.Collections.IEnumerator DisableHandholds(float duration)
    {
        Debug.Log("[CS135] Handhold disable called started");

        handholds.enabled = false;
        Debug.Log("[CS135] Handholds disabled");

        yield return new WaitForSeconds(duration);

        handholds.enabled = true;
        Debug.Log("[CS135] Handholds enabled");
    }

    void TriggerHaptics(XRDirectInteractor hand)
    {
        if (hand == null) return;
        var controller = hand.GetComponentInParent<ActionBasedController>();
        if (controller != null)
            controller.SendHapticImpulse(hapticIntensity, hapticDuration);
    }
}