using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ClimbingObstacle : MonoBehaviour
{
    public XRDirectInteractor leftHandInteractor;
    public XRDirectInteractor rightHandInteractor;
    public float gripDisableDuration = 1.5f;
    public float hapticIntensity = 0.8f;
    public float hapticDuration = 0.3f;

    void Start()
    {
        if (leftHandInteractor == null || rightHandInteractor == null)
            FindHandInteractors();
    }

    void FindHandInteractors()
    {
        XRDirectInteractor[] interactors = FindObjectsByType<XRDirectInteractor>(FindObjectsSortMode.None);

        foreach (var i in interactors)
        {
            string n = i.gameObject.name.ToLower();
            if (n.Contains("left")) leftHandInteractor = i;
            if (n.Contains("right")) rightHandInteractor = i;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Vector3 hitPoint = collision.contacts[0].point;

        XRDirectInteractor hitHand = GetCloserHand(hitPoint);
        XRDirectInteractor otherHand =
            (hitHand == leftHandInteractor) ? rightHandInteractor : leftHandInteractor;

        // Disable the hit hand's grip
        if (hitHand != null)
            StartCoroutine(DisableGrip(hitHand, gripDisableDuration));

        // Briefly stagger the other hand
        StartCoroutine(DisableGrip(otherHand, gripDisableDuration * 0.5f));

        TriggerHaptics(hitHand);
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
        hand.enabled = false;
        yield return new WaitForSeconds(duration);
        hand.enabled = true;
    }

    void TriggerHaptics(XRDirectInteractor hand)
    {
        if (hand == null) return;
        var controller = hand.GetComponentInParent<ActionBasedController>();
        if (controller != null)
            controller.SendHapticImpulse(hapticIntensity, hapticDuration);
    }
}