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

    public Light dLightforMood;

    private Color mainCameraBgforMoodColor;
    private Color dLightforMoodColor;



    public Color[] BackGroundColors =
            {
                Color.white,
                Color.red,
                Color.green,
                Color.blue,
                Color.cyan,
                Color.yellow,
                
            };

    //private Color currColor = Color.white;

    private void Awake()
    {
        mainCameraBgforMoodColor = Camera.main.backgroundColor; //mainCameraBgforMood.GetComponent<Camera>().backgroundColor;
        dLightforMood.type = LightType.Directional;
        dLightforMoodColor = dLightforMood.color;

    }

    private float duration = 15;
    private float smoothness = 0.02f;
    private Color currentColor = Color.white;

    private void Start()
    {
        StartCoroutine(LerpColor());

    }

    private IEnumerator LerpColor()
    {
        
        int ColorIndex = 0;
        while (true)
        {
            dLightforMood.color = Color.Lerp(dLightforMood.color, BackGroundColors[ColorIndex%BackGroundColors.Length], Time.deltaTime);

            var vcolor = dLightforMood.color - BackGroundColors[ColorIndex % BackGroundColors.Length];


            Debug.Log(Mathf.Abs(vcolor.r)+" "+Mathf.Abs(vcolor.g) + " " + Mathf.Abs(vcolor.b));

            if (Mathf.Abs(vcolor.r) <= 0.31f && Mathf.Abs(vcolor.g) <= 0.12f && Mathf.Abs(vcolor.b) <= 0.18)
            {
                ++ColorIndex;
            }
            yield return new WaitForSeconds(0.2f);
        }
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