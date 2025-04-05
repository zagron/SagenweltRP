
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class WorldMenu : UdonSharpBehaviour
{
    VRCPlayerApi localPlayer;

    public GameObject sprungTextobj; 
    public GameObject rennenTextobj; 
    public GameObject laufenTextobj; 
    public GameObject seitenTextobj; 
    public GameObject[] toggleObject;
    public Slider aktuellerSlider;


        public float jumpHeight = 3f;
        public float runSpeed = 4f;
        public float walkSpeed = 2f;
        public float strafeSpeed = 2f;
        public float gravity = 1f;
        
        public float sound = 25f; 
       

    void Start()
    {
        localPlayer = Networking.LocalPlayer;
        sprungTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = jumpHeight.ToString();
        rennenTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = runSpeed.ToString();
        laufenTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = walkSpeed.ToString();
        seitenTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = strafeSpeed.ToString();

    }
    public void SprungHoch()
    {
        localPlayer.SetJumpImpulse(jumpHeight = jumpHeight + 1f);
        sprungTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = jumpHeight.ToString();
    }
    public void SprungRunter()
    {
        localPlayer.SetJumpImpulse(jumpHeight = jumpHeight - 1f);
        sprungTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = jumpHeight.ToString();
    }
public void RennenSchnell()
{
    localPlayer.SetRunSpeed(runSpeed = runSpeed + 1f);
    rennenTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = runSpeed.ToString();
}
public void RennenLangsam()
{
    localPlayer.SetRunSpeed(runSpeed = runSpeed - 1f);
    rennenTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = runSpeed.ToString();
}
public void LaufenSchnell()
{
    localPlayer.SetWalkSpeed(walkSpeed = walkSpeed + 1f);
    laufenTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = walkSpeed.ToString();
}
public void LaufenLangsam()
{
    localPlayer.SetWalkSpeed(walkSpeed = walkSpeed - 1f);
    laufenTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = walkSpeed.ToString();
}
public void SeitenSchnell()
{
    localPlayer.SetStrafeSpeed(strafeSpeed = strafeSpeed + 1f);
    seitenTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = strafeSpeed.ToString();
}
public void SeitenLangsam()
{
    localPlayer.SetStrafeSpeed(strafeSpeed = strafeSpeed - 1f);
    seitenTextobj.GetComponent<TMPro.TextMeshProUGUI>().text = strafeSpeed.ToString();
}

public void lichtAn()
{
    RenderSettings.ambientIntensity = 1.0f;
}
public void lichtAus()
{
    RenderSettings.ambientIntensity = 0.0f;
}
public void objectToggle()
{
foreach(GameObject go in toggleObject)
{
    if(go.activeSelf)
    {
        go.SetActive(false);
    }
    else
    {
        go.SetActive(true);
    }
}
}
public void SliderSound()
{
    localPlayer.SetVoiceDistanceFar(aktuellerSlider.value);
}
public void ButtonSound()
{
if(sound > 10f)
{

    localPlayer.SetVoiceDistanceFar(sound = 10f);
}
else 
{
    localPlayer.SetVoiceDistanceFar(sound = 25f);
}


}
}
