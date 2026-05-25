using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchRooms : MonoBehaviour
{
    public GameObject livingRoomSphere;   
    public GameObject cantinaSphere;      
    public GameObject cubeSphere;         
    public GameObject mezzanineSphere;    

    public Button cantinaHotspot;         
    public Button livingRoomHotspot;      
    public Button cubeHotspotFromLiving;  
    public Button cubeHotspotFromCantina; 
    public Button cubeHotspotFromMezzanine; 
    public Button mezzanineHotspot;       

    public Animator fadeAnimator;         

    private GameObject currentSphere;     

    void Start()
    {
        if (livingRoomSphere == null) return;

        SetActiveSphere(livingRoomSphere);

        if (cantinaHotspot) cantinaHotspot.onClick.AddListener(() => StartSwitch(cantinaSphere));
        if (livingRoomHotspot) livingRoomHotspot.onClick.AddListener(() => StartSwitch(livingRoomSphere));
        if (cubeHotspotFromLiving) cubeHotspotFromLiving.onClick.AddListener(() => StartSwitch(cubeSphere));
        if (cubeHotspotFromCantina) cubeHotspotFromCantina.onClick.AddListener(() => StartSwitch(cubeSphere));
        if (cubeHotspotFromMezzanine) cubeHotspotFromMezzanine.onClick.AddListener(() => StartSwitch(cubeSphere));
        if (mezzanineHotspot) mezzanineHotspot.onClick.AddListener(() => StartSwitch(mezzanineSphere));
    }

    
    public void StartSwitch(GameObject targetSphere)
    {
        if (currentSphere != targetSphere)
        {
            if (fadeAnimator != null)
                StartCoroutine(SwitchWithFade(targetSphere));
            else
                SwitchSphere(targetSphere);
        }
    }

    public void SwitchToCantina() => StartSwitch(cantinaSphere);
    public void SwitchToLivingRoom() => StartSwitch(livingRoomSphere);
    public void SwitchToCube() => StartSwitch(cubeSphere);
    public void SwitchToMezzanine() => StartSwitch(mezzanineSphere);

    
    private IEnumerator SwitchWithFade(GameObject targetSphere)
    {
        
        fadeAnimator.SetTrigger("FadeOut");

        
        yield return new WaitForSeconds(1f);  

        
        SwitchSphere(targetSphere);

        
        fadeAnimator.SetTrigger("FadeIn");

        
        yield return new WaitForSeconds(1f);
    }

    
    public void SwitchSphere(GameObject targetSphere)
    {
        if (livingRoomSphere == null || targetSphere == null) return;

        livingRoomSphere.SetActive(false);
        cantinaSphere.SetActive(false);
        cubeSphere.SetActive(false);
        mezzanineSphere.SetActive(false);

        SetActiveSphere(targetSphere);

        Debug.Log(targetSphere.name + " sphere is now active");
    }

    
    void SetActiveSphere(GameObject sphere)
    {
        sphere.SetActive(true);  
        currentSphere = sphere;  
    }
}
