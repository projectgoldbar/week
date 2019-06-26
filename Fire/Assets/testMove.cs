using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMove : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    public RectTransform rect;

    public Camera UICamera;

    // Update is called once per frame
    private void Update()
    {
        Test1();
    }

    private void Test1()
    {
        Vector2 pos = Input.mousePosition;
        {
            pos.x = Mathf.Clamp01(pos.x / Screen.width);
            pos.y = Mathf.Clamp01(pos.y / Screen.height);

            Vector3 screenPos = new Vector3(pos.x, pos.y, 0f);

            var uiv = UICamera.ViewportToWorldPoint(screenPos);
            //rect.position  = rect.position

            Debug.Log(rect.position.z);
        }

        //Vector3 lp = rect.localPosition;
        //lp.x = Mathf.Round(lp.x);
        //lp.y = Mathf.Round(lp.y);
        //rect.localPosition = lp;
    }
}