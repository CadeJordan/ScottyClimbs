using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using static UnityEngine.XR.Interaction.Toolkit.XRInteractionManager;

public class ClimbingObstacle : MonoBehaviour
{
    public float gripDisableDuration = 1.5f;
    public float hapticIntensity = 0.8f;
    public float hapticDuration = 0.3f;
    public GameObject leftInteractor;
    public GameObject rightInteractor;
    public XRInteractionManager interactionManager;
    void Start()
    {
        leftInteractor = GameObject.FindWithTag("LEFTINTER");
        rightInteractor = GameObject.FindWithTag("RIGHTINTER");
        interactionManager = GameObject.FindWithTag("INTERMANAGER");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Debug.Log("[CS135] Player Collision");
        interactionManager.CancelInteractorSelection((IXRSelectInteractor)interactor);
        Destroy(gameObject);
    }
}