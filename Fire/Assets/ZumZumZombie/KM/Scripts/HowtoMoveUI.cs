using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HowtoMoveUI : MonoBehaviour
{
    public RawImage howtoMoveUI;
    Color howtoMoveUIColor;

    // Start is called before the first frame update
    void Start()
    {
        howtoMoveUIColor = howtoMoveUI.color;
        StartCoroutine("DisappearHowtoMoveUI");
    }

    IEnumerator DisappearHowtoMoveUI(){

        for (float a = 1.5f; a >= 0; a -= 0.01f)
        {
            howtoMoveUI.color = new Vector4(howtoMoveUIColor.r, 
                                            howtoMoveUIColor.g,
                                            howtoMoveUIColor.b,
                                            a);
            yield return new WaitForFixedUpdate();
        }
    }
}
