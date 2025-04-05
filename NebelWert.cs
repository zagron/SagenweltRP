
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class NebelWert : UdonSharpBehaviour
{
     VRCPlayerApi spieler;
    
    public float NebelwertBase = 0.06f;
    public Color farbeBase = Color.white;

    public float NebelwertToggle = 0.06f;
    public Color farbeToggle = Color.white;

    [UdonSynced] 
    bool isAn = true;



    void Start()
    {
      RenderSettings.fogColor = farbeBase;  
      RenderSettings.fogDensity = NebelwertBase;   

    }
    public override void OnDeserialization()
    {
        RequestSerialization();
        OnInteract(); // Late Joiner sehen den aktuellen Wert
    }
    
   
    public override void Interact()
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

        isAn = !isAn; // Bool-Wert umkehren
        RequestSerialization(); // Synchronisation auslösen
        OnInteract();
    }
    public void OnInteract()
    {
        FogColour();
        FogWert();

    }
    void FogColour()
    {
        if(isAn)
        {
        RenderSettings.fogColor = farbeBase;
        }
        else 
        {
        RenderSettings.fogColor = farbeToggle;    
        }
    }
    void FogWert()
    {
        if(isAn)
        {
        RenderSettings.fogDensity = NebelwertBase;   
        }
        else
        {
        RenderSettings.fogDensity = NebelwertToggle;
        }

 
}
}
