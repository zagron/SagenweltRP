
using System;
using UdonSharp;
using Unity.VisualScripting;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;
using VRC.Udon.Common.Interfaces;

public class TriggerOnPickup : UdonSharpBehaviour
{
    [SerializeField]GameObject pickupObject;
    [SerializeField]GameObject[] ToggleObjects;
    [SerializeField]AudioSource audioSource;
    
    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(pickupObject))
        {
            if (!Networking.IsOwner(gameObject))
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
            pickupObject.GetComponent<VRC_Pickup>().Drop();
            pickupObject.SetActive(false);
            pickupObject.GetComponent<VRC_Pickup>().pickupable = false;
            audioSource.Play();
            foreach (GameObject ToggleObject in ToggleObjects)
            {
                ToggleObject.SetActive(!ToggleObject.activeSelf);
            }
        }
    }

    
}
