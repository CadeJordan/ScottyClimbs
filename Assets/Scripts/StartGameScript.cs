using UnityEngine;
using UnityEngine.InputSystem;
using Time = UnityEngine.Time;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public InputActionReference buttonPressReference;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnEnable()
    {
        if (buttonPressReference != null && buttonPressReference.action != null)
        {
            buttonPressReference.action.performed += OnButtonPress;
        }
    }

    private void OnDisable()
    {
        if (buttonPressReference != null && buttonPressReference.action != null)
        {
            buttonPressReference.action.performed -= OnButtonPress;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnButtonPress(InputAction.CallbackContext callback)
    {
        SceneManager.LoadScene("SampleScene");
        this.enabled = false;
    }
}
