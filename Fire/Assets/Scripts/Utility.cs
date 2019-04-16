using UnityEngine;
using UnityEngine.UI;

public class Utility : Singleton<Utility>
{
    [Header("Player")]
    public Transform playerTr = null;

    public static int CameraViewMonster;

    public Image PlayerHpBar = null;
    public Text PlayerHpText = null;

    private static int i = 0;

    public int CarKey = 1;

    public Color ColorChageForColorBlink(Color currentColor, Color changedColor)
    {
        Color returnColor = currentColor;
        var a = i % 2;

        if (a == 1)
        {
            returnColor = currentColor;
        }
        else
        {
            returnColor = changedColor;
        }
        i++;

        return returnColor;
    }

    public Color ChangeColor(Color color)
    {
        return color;
    }

    /// <summary>
    /// 백터를 각도로 변환
    /// </summary>
    /// <param name="dir">방향백터</param>
    /// <returns></returns>
    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}

public class RandomAngle
{
    public float PatrolDistance = 10.0f;

    public float Value(float a, float b)
    {
        var value = Random.Range(a, b);
        return value;
    }

    public Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3();

        var angle = Value(0.0f, 360.0f);

        pos.x = Mathf.Cos(angle) * PatrolDistance;
        pos.y = 0;
        pos.z = Mathf.Sin(angle) * PatrolDistance;

        return pos;
    }
}