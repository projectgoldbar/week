using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyBase_Controller : MonoBehaviour
{
    public LobbyPlayerController lobbyPlayerController;
    public DoorController doorController;
    public StartObjController StartObjController;

    public GameObject mainCameraBgforMood;

    //public GameObject dLightforMood;
    //public GameObject rawImageforMood;

    private Color mainCameraBgforMoodColor;
    //private Color dLightforMoodColor;
    //private Color rawImageforMoodColor;

    //private float changeMoodTimeSpan = 2.0f;
    //private float changeMoodTime = 2.0f;

    //private Color currColor = Color.white;

    private void Awake()
    {
        mainCameraBgforMoodColor = mainCameraBgforMood.GetComponent<Camera>().backgroundColor;
        //dLightforMoodColor = dLightforMood.GetComponent<Light>().color;
        //rawImageforMoodColor = rawImageforMood.GetComponent<Renderer>().material.
    }

    private float duration = 5;
    private float smoothness = 0.02f;
    private Color currentColor = Color.white;

    private void Start()
    {
        StartCoroutine("LerpColor");
    }

    private IEnumerator LerpColor()
    {
        float progress = 0;
        float increment = smoothness / duration;
        while (progress < 1)
        {
            mainCameraBgforMood.GetComponent<Camera>().backgroundColor = Color.Lerp(Color.red, Color.blue, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        //return true;
    }

    //private void Start()
    //{
    //    //   Debug.Log(rawImageforMoodColor);
    //     StartCoroutine("ChangeMoodColor");
    //}

    //private IEnumerator ChangeMoodColor()
    //{
    //    float progress = 0;
    //    float increment = changeMoodTime / changeMoodTimeSpan;

    //    while (progress < 1)
    //    {
    //        mainCameraBgforMoodColor = Color.Lerp(Color.red, Color.blue, progress);
    //        progress += increment;
    //        Debug.Log(mainCameraBgforMoodColor);
    //        yield return new WaitForSeconds(changeMoodTime);
    //    }
    //}

    //private void Update()
    //{
    //    float progress = 0;
    //    float increment = changeMoodTime / changeMoodTimeSpan;

    //    while (progress < 1)
    //    {
    //        mainCameraBgforMoodColor = Color.Lerp(Color.red, Color.blue, progress);
    //        progress += increment;
    //        Debug.Log(mainCameraBgforMoodColor);
    //    }
    //}
}