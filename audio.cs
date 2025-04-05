using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class audio : UdonSharpBehaviour
{
    VRCPlayerApi spieler;
    float sound;

    private void Start() {
        spieler = Networking.LocalPlayer;
    }

    public override void Interact()
    {
        if(spieler.isLocal)
        {
            if(sound > 10f)
            {

                spieler.SetVoiceDistanceFar(sound = 10f);
            }
            else 
            {
                spieler.SetVoiceDistanceFar(sound = 25f);
            }
        }

    }
}
