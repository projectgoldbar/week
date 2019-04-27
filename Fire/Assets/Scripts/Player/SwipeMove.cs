using UnityEngine;

public class SwipeMove : Move
{
    public Vector2 pos;
    public Vector2 start;
    public Vector2 Current;

    private RaycastHit hit;

    private float AngleY;

    private void FixedUpdate()
    {
        MoveState();

        if (Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Current = Input.mousePosition;

            var cur = DistanceNRot(start, Current).rot;

            AngleY = Mathf.Atan2(cur.x, cur.y) * Mathf.Rad2Deg;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, AngleY, 0);
        }
    }

    public override void PcMove()
    {
    }

    public (float dis, Vector2 rot) DistanceNRot(Vector2 start, Vector2 current)
    {
        var cur = (Current - start);
        var rot = cur.normalized;
        var dis = Vector3.Distance(start, current);

        return (dis, rot);
    }

    public override void SelectState()
    {
        runSpeed = agent.speed;
        agent.velocity = agent.transform.forward * runSpeed;
    }
}