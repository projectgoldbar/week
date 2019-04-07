using UnityEngine;
using UnityEngine.UI;

public class Ref : Singleton<Ref>
{
    [Header("Player")]
    public Slider hpbar = null;

    public Transform playerTr = null;
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