using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class LocalToggle: UdonSharpBehaviour
{
    public GameObject[] ToggleObjects;
   
    public override void Interact()
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

        foreach(GameObject go in ToggleObjects)
        {
            go.SetActive(!go.activeSelf); // Umkehren des aktuellen Zustands
        }
    }
   
}