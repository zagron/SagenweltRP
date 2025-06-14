
using UdonSharp;
using Unity.VisualScripting;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickupMovementSpeed : UdonSharpBehaviour
{
    public float runSpeed = 1f;
    public float walkSpeed = 1f;
    public float strafeSpeed = 1f;
    
    private float normalRunSpeed;
    private float normalWalkSpeed;
    private float normalStrafeSpeed;
    
    
    VRCPlayerApi currentplayer;
    VRC_Pickup pickup;
    void Start()
    {
        pickup = gameObject.GetComponent<VRC_Pickup>();
        currentplayer = Networking.LocalPlayer;
        normalRunSpeed = currentplayer.GetRunSpeed();
        normalWalkSpeed = currentplayer.GetWalkSpeed();
        normalStrafeSpeed = currentplayer.GetStrafeSpeed();
    }

    public override void OnPickup()
    {
        if (Networking.LocalPlayer == null) return;
            pickup.PlayHaptics();
            currentplayer.SetRunSpeed(runSpeed);
            currentplayer.SetWalkSpeed(walkSpeed);
            currentplayer.SetStrafeSpeed(strafeSpeed);
    }
    public override void OnDrop()
    {
        if (Networking.LocalPlayer == null) return;
        currentplayer.SetRunSpeed(normalRunSpeed);
        currentplayer.SetWalkSpeed(normalWalkSpeed);
        currentplayer.SetStrafeSpeed(normalStrafeSpeed);
    }
}
