
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SpawnWorldMenu : UdonSharpBehaviour
{
    public GameObject kopf;

    void Start()
    {
        this.transform.position = new Vector3(0, -50, 0);
    }
    public void spawnWorldMenu()
    {
        this.transform.position = kopf.transform.position + kopf.transform.forward * 5.0f;
        this.transform.rotation = kopf.transform.rotation;
    }
   void OnTriggerExit(Collider other)
   {

    if(other.gameObject.name.Contains("kopf"))
    {
        this.transform.position = new Vector3(0, -50, 0);
    }
   }

}
