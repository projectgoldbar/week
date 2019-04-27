using System;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public Vector2 Start;
    public Vector2 Current;
    public Vector2 pos;
    public bool GoSwipe = false;

    public float SwipeDistance = 0;
    public float MinMaxRange = 0;

    private void Awake()
    {
        GoSwipe = true;
    }

    private void Update()
    {
        if (!GoSwipe)
        {
            Start = Vector2.zero;
            Current = Vector2.zero;
            pos = Vector2.zero;
            return;
        }
        PositionSet();
    }

    private void PositionSet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Start = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Current = Input.mousePosition;
        }
        pos = Current - Start;
    }

    public void LeftNRightSwipe(Action LeftNRight)
    {
        //왼쪽에서 오른쪽
        if (pos.x >= SwipeDistance && (Current.y > Start.y - MinMaxRange && Current.y < Start.y + MinMaxRange))
        {
            LeftNRight?.Invoke();
            Debug.Log("좌에서 우로 ");
            GoSwipe = false;
        }
    }

    public void RightNLeftSwipe(Action RightNLeft)
    {
        //왼쪽에서 오른쪽
        if (pos.x <= -SwipeDistance && (Current.y > Start.y - MinMaxRange && Current.y < Start.y + MinMaxRange))
        {
            RightNLeft?.Invoke();
            Debug.Log("우에서 좌로 ");
            GoSwipe = false;
        }
    }

    public void UpNDownSwipe(Action UpNDown)
    {
        if (pos.y <= -SwipeDistance && (Current.x > Start.x - MinMaxRange && Current.x < Start.x + MinMaxRange))
        {
            UpNDown?.Invoke();
            Debug.Log("위에서 아래로");
            GoSwipe = false;
        }
    }

    public void DownNUpSwipe(Action DownNUp)
    {
        if (pos.y >= SwipeDistance && (Current.x > Start.x - MinMaxRange && Current.x < Start.x + MinMaxRange))
        {
            DownNUp?.Invoke();
            Debug.Log("아래에서 위로");
            GoSwipe = false;
        }
    }

    public void SwipeProcess(Action LeftNRight)
    {
        if (!GoSwipe) return;

        if (Input.GetMouseButtonDown(0))
        {
            Start = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Current = Input.mousePosition;

            var pos = Current - Start;
            //왼쪽에서 오른쪽
            if (pos.x >= SwipeDistance && (Current.y > Start.y - MinMaxRange && Current.y < Start.y + MinMaxRange))
            {
                LeftNRight?.Invoke();
                Debug.Log("좌에서 우로 ");
                GoSwipe = false;
            }
            else if (pos.x <= -SwipeDistance && (Current.y > Start.y - MinMaxRange && Current.y < Start.y + MinMaxRange))
            {
                Debug.Log("우에서 좌로 ");
                GoSwipe = false;
            }
            else if (pos.y >= SwipeDistance && (Current.x > Start.x - MinMaxRange && Current.x < Start.x + MinMaxRange))
            {
                Debug.Log("아래에서 위로");
                GoSwipe = false;
            }
            else if (pos.y <= -SwipeDistance && (Current.x > Start.x - MinMaxRange && Current.x < Start.x + MinMaxRange))
            {
                Debug.Log("위에서 아래로");
                GoSwipe = false;
            }
        }
    }
}