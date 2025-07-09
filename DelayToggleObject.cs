using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class DelayToggleObject : UdonSharpBehaviour
{
    public GameObject[] ToggleObjects;
    [SerializeField] private float WaitForSeconds;
    [SerializeField] private GameObject OtherSwitch;
    [SerializeField]private bool IsSolvingButtonOn;
    private bool isPressed;
    private float Duration;
    [SerializeField]private AudioSource SwitchSound;
    [SerializeField]private AudioSource SucessSound;

    private void Start()
    {
        isPressed = false;
       
    }
    public override void Interact()
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnInteract");
    }
    public void OnInteract()
    {
        if (IsSolvingButtonOn == false && isPressed == false)
        {
            if(SwitchSound != null)
                SwitchSound.Play();
            foreach(GameObject go in ToggleObjects)
            {
              go.SetActive(!go.activeSelf); 
            }
            isPressed = true;
            SendCustomEventDelayedSeconds(nameof(Wait), WaitForSeconds);
            Debug.Log("Nach Wait");
        }
        else if(IsSolvingButtonOn == true && isPressed == false)
        {
            isPressed = true;
        }
        else
        {
            if (SucessSound != null)
                SucessSound.Play();
        }
    }
    public void Wait()
    {
        Debug.Log("Wait");
        if (OtherSwitch.GetComponent<DelayToggleObject>().GetIsPressed() != true)
        {
            foreach(GameObject go in ToggleObjects)
            {
                go.SetActive(!go.activeSelf);
            }
            isPressed = false;
        }
    }
    public bool GetIsPressed()
    {
        return isPressed;
    }
}
