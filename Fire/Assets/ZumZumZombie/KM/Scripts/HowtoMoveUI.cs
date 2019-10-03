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
        Time.timeScale = 0;
        howtoMoveUIColor = howtoMoveUI.color;
        StartCoroutine(DisappearHowtoMoveUI());
    }

    public void GameStart()
    {
        Time.timeScale = 1;
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
        howtoMoveUI.gameObject.SetActive(false);
        yield break;
    }
}