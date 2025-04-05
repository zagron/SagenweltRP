
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ToggleObject : UdonSharpBehaviour
{
    public GameObject[] ToggleObjects;

    [UdonSynced] 
    bool isNacht = false;



    void Start()
    {
        
    }
    public override void OnDeserialization()
    {
        OnInteract(); // Late Joiner sehen den aktuellen Wert
    }
    
   
    public override void Interact()
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

        isNacht = !isNacht; // Bool-Wert umkehren
        RequestSerialization(); // Synchronisation auslösen
        OnInteract();
    }
    public void OnInteract()
    {
       
            
            objectToggle();
            

    }
    public void objectToggle()
    {
    foreach(GameObject go in ToggleObjects)
    {
                    go.SetActive(!go.activeSelf); // Umkehren des aktuellen Zustands
    }
    }
}
    