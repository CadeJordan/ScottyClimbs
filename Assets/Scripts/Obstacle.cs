using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Feedback;

public class ClimbingObstacle : MonoBehaviour
{
    public float gripDisableDuration = 1.5f;
    public float hapticIntensity = 0.8f;
    public float hapticDuration = 0.3f;
    public GameObject leftInteractor;
    public GameObject rightInteractor;
    public XRInteractionManager interactionManager;
    private XRBaseInputInteractor leftHaptics;
    private XRBaseInputInteractor rightHaptics;
    void Start()
    {
        leftInteractor = GameObject.FindWithTag("LEFTINTER");
        rightInteractor = GameObject.FindWithTag("RIGHTINTER");
        GameObject managerObj = GameObject.FindWithTag("INTERMANAGER");
        if (managerObj != null)
            interactionManager = managerObj.GetComponent<XRInteractionManager>();
        if (leftInteractor != null)
            leftHaptics = leftInteractor.GetComponent<XRBaseInputInteractor>();
        if (rightInteractor != null)
            rightHaptics = rightInteractor.GetComponent<XRBaseInputInteractor>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Debug.Log("[CS135] Player Collision");
        
        // Force the user to let go of both grips
        if (interactionManager != null)
        {
            if (leftInteractor != null)
            {
                var leftInteractorComp = leftInteractor.GetComponent<IXRSelectInteractor>();
                if (leftInteractorComp != null)
                    interactionManager.CancelInteractorSelection(leftInteractorComp);
            }
            
            if (rightInteractor != null)
            {
                var rightInteractorComp = rightInteractor.GetComponent<IXRSelectInteractor>();
                if (rightInteractorComp != null)
                    interactionManager.CancelInteractorSelection(rightInteractorComp);
            }
        }

        leftHaptics?.SendHapticImpulse(hapticIntensity, hapticDuration);
        rightHaptics?.SendHapticImpulse(hapticIntensity, hapticDuration);
        
        Destroy(gameObject);
    }
}