using UnityEngine;
using UnityEngine.InputSystem;
using Time = UnityEngine.Time;
using TMPro;
using System;

public class StartGameScript : MonoBehaviour
{
    public InputActionReference buttonPressReference;
    public TextMeshProUGUI timer;
    public bool gameActive = false;
    public float time = 0;
    public int minutes = 0;
    public int seconds = 0;
    public int milliseconds = 0;
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
    // Update is called once per frame
    void Update()
    {
        if(gameActive)
        {
            time += Time.deltaTime;
            minutes = (int)(time/60);
            seconds = (int)(time%60);
            milliseconds = (int)((time - Math.Floor(time))*1000);
            timer.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        }
    }

    private void OnButtonPress(InputAction.CallbackContext callback)
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(0,100,0);
        GetComponent<CharacterController>().enabled = true;
        gameActive = true;
    }
}
