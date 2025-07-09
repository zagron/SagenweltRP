
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class OnCollisionToggleFogAndObjects : UdonSharpBehaviour
{
    [SerializeField] private float FogValue;
    [SerializeField] Material SkyBoxMaterial;
    [SerializeField] [Range(0f,8f)] private float SkyBoxIntensity = 1f;
    public GameObject[] ToggleObjects;
    
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            RenderSettings.fogDensity = FogValue;
            RenderSettings.ambientIntensity = SkyBoxIntensity;
            RenderSettings.skybox = SkyBoxMaterial;
            foreach(GameObject go in ToggleObjects)
            {
                go.SetActive(!go.activeSelf); 
            }
        }
    }
}
