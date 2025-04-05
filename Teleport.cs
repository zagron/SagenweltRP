
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Teleport : UdonSharpBehaviour
{
    VRCPlayerApi spieler;
    public GameObject ZielObject;

    private void Start() {
    
        spieler = Networking.LocalPlayer;

    }

    public override void Interact()
    {
        if(spieler.isLocal)
        spieler.TeleportTo(ZielObject.transform.position, spieler.GetRotation());

    }
}
