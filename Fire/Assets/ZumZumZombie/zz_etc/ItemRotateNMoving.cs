using UnityEngine;

public class ItemRotateNMoving : MonoBehaviour
{
    private float xpos = 3.0f;
    private float ypos = 4.0f;
    private float zpos = 4.0f;

    [Header("위아래")]
    public float UpDownSpeed;

    public float YRange = 3;

    public bool UpDownFlag = false;

    [Header("좌우")]
    public float LeftRightSpeed;

    public float XRange = 3;

    public bool LeftRightFlag = false;

    [Header("회전")]
    public float rotSpeed = 3;

    public bool RotateFlag = false;

    [Header("스케일")]
    public float ScaleRange = 3;

    public float ScaleSpeed = 3;
    public bool ScaleFlag = false;

    private Vector3 UpDownPos = Vector3.zero;
    private Vector3 LeftRightPos = Vector3.zero;

    public Transform Target = null;

    private void Awake()
    {
        xpos = Target.position.x;
        ypos = Target.position.y;
        zpos = Target.position.z;
    }

    private void Update()
    {
        if (!UpDownFlag && !LeftRightFlag && !RotateFlag && !ScaleFlag) return;

        if (UpDownFlag) UpDown(Target);
        if (LeftRightFlag) LeftRight(Target);
        if (RotateFlag) Rotate(Target);
        if (ScaleFlag) Scale(Target);
    }

    public void UpDown(Transform tg)
    {
        UpDownPos = Vector3.up * (ypos + Mathf.PingPong(Time.time * UpDownSpeed, YRange)) + (xpos * Vector3.right) + (zpos * Vector3.forward);           //new Vector3(tg.position.x, ypos + Mathf.PingPong(Time.time * movingSpeed, YRange), tg.position.z);
        tg.position = UpDownPos;
    }

    public void LeftRight(Transform tg)
    {
        //LeftRightPos = Vector3.right * (xpos * Mathf.PingPong(Time.time * LeftRightSpeed, XRange)) + (ypos * Vector3.up) + (zpos * Vector3.forward);
        //LeftRightPos = Vector3.right * xpos + Vector3.right * (Mathf.PingPong(Time.time * movingSpeed, XRange)) + (tg.position.y * Vector3.up);
        LeftRightPos = new Vector3(xpos + Mathf.PingPong(Time.time * LeftRightSpeed, XRange), tg.position.y, tg.position.z);
        tg.position = LeftRightPos;
    }

    public void Rotate(Transform tg)
    {
        tg.transform.rotation *= Quaternion.Euler(Vector3.up * Time.deltaTime * rotSpeed);
    }

    public void Scale(Transform tg)
    {
        tg.localScale = Vector3.one * (ScaleRange + Mathf.PingPong(Time.time * ScaleSpeed, ScaleRange));
    }
}