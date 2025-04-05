
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class NachtToggle : UdonSharpBehaviour
{
    VRCPlayerApi spieler;
    
    public GameObject[] Skydomes;
    [Space(10)]
    public GameObject[] ToggleObject;
    [Space(10)]
    public GameObject[] ToggleObjectEmission;
    [Space(10)]
    public GameObject Sonne;
    
    public Color SonneTag = Color.white;
    public Color SonneNacht = Color.black;

    public float SonneIntenseTag = 1.0f;
    public float SonneIntenseNacht = 0.0f;
    [Space(10)]
    public float SkydomeTag = 0.03f;
    public float SkydomeNacht = 0.32f;
    [Space(10)]
    public float EnviromentLichtTag = 1.0f;
    public float EnviromentLichtNacht = 0.4f;
    [Space(10)]
    public Color WolkenFarbeNacht = Color.black;
    public Color WolkenFarbeTag = Color.white;

    [UdonSynced] 
    bool isNacht = false;



    void Start()
    {
        
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

        isNacht = !isNacht; // Bool-Wert umkehren
        RequestSerialization(); // Synchronisation auslösen
        OnInteract();
    }
    public void OnInteract()
    {
       
            ChangeSkydome();
            objectToggle();
            SetLight();
            toggleCloundEmission();
            SetSun();

    }

    void ChangeSkydome()
    {
        foreach(GameObject gameobject in Skydomes) {

            Renderer rend = gameobject.GetComponent<Renderer>();
            
        if(isNacht)
        {
        rend.material.mainTextureOffset = new Vector2(SkydomeNacht, 0.0f);
        }else
        {
            rend.material.mainTextureOffset = new Vector2(SkydomeTag, 0.0f);
        }
        }
    }

    public void objectToggle()
    {
    foreach(GameObject go in ToggleObject)
    {
                    go.SetActive(!go.activeSelf); // Umkehren des aktuellen Zustands
    }
    }
    public void SetLight()
    {
        if(isNacht)
        {
            RenderSettings.ambientIntensity = EnviromentLichtNacht;
        }
        else{
            RenderSettings.ambientIntensity = EnviromentLichtTag;
        }
    }
    public void toggleCloundEmission()
    {

        foreach(GameObject go in ToggleObjectEmission)
        {
            Renderer rend = go.GetComponent<Renderer>();
            rend.material.EnableKeyword("_EMISSION");

            if(isNacht)
            {
                rend.material.SetColor("_EmissionColor", WolkenFarbeNacht); 
            }
            else
            {
                rend.material.SetColor("_EmissionColor", WolkenFarbeTag); 
            }
            
        }
    }
    public void SetSun()
    {
        Light licht = Sonne.GetComponent<Light>();
        if (isNacht)
        {
            licht.color = SonneNacht;
            licht.intensity = SonneIntenseNacht;
        }
        else
        {
            licht.color = SonneTag;
            licht.intensity = SonneIntenseTag;
        }
    }
}
