using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwitchRoomsCampus : MonoBehaviour
{
    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;

    public Button hotspotTo1;
    public Button hotspotTo2;
    public Button hotspotTo3;

    public Animator fadeAnimator;

    private GameObject currentSphere;

    void Start()
    {
        if (sphere1 == null) return;

        SetActiveSphere(sphere1);

        if (hotspotTo1) hotspotTo1.onClick.AddListener(() => StartSwitch(sphere1));
        if (hotspotTo2) hotspotTo2.onClick.AddListener(() => StartSwitch(sphere2));
        if (hotspotTo3) hotspotTo3.onClick.AddListener(() => StartSwitch(sphere3));
    }

    public void StartSwitch(GameObject target)
    {
        if (currentSphere != target)
        {
            if (fadeAnimator != null)
                StartCoroutine(SwitchWithFade(target));
            else
                SwitchSphere(target);
        }
    }

    public void SwitchToSphere1() => StartSwitch(sphere1);
    public void SwitchToSphere2() => StartSwitch(sphere2);
    public void SwitchToSphere3() => StartSwitch(sphere3);

    private IEnumerator SwitchWithFade(GameObject target)
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SwitchSphere(target);
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
    }

    public void SwitchSphere(GameObject target)
    {
        if (target == null) return;

        if (sphere1) sphere1.SetActive(false);
        if (sphere2) sphere2.SetActive(false);
        if (sphere3) sphere3.SetActive(false);

        SetActiveSphere(target);
    }

    void SetActiveSphere(GameObject sphere)
    {
        sphere.SetActive(true);
        currentSphere = sphere;
    }
}
