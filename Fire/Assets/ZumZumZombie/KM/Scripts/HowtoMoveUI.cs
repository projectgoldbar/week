using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HowtoMoveUI : MonoBehaviour
{
    public float disappearTimes = 2.0f;
    public RawImage howtoMoveUI;
    private Color howtoMoveUIColor;
    private WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();

    private void Start()
    {
        howtoMoveUIColor = howtoMoveUI.color;
        StartCoroutine("DisappearHowtoMoveUI");
    }

    private IEnumerator DisappearHowtoMoveUI()
    {
        for (float a = disappearTimes; a >= 0; a -= 0.01f)
        {
            howtoMoveUI.color = new Vector4(howtoMoveUIColor.r,
                                            howtoMoveUIColor.g,
                                            howtoMoveUIColor.b,
                                            a);
            yield return fixedUpdate;
        }
        yield break;
    }
}