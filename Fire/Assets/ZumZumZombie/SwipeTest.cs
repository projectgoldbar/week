using UnityEngine;

public class SwipeTest : MonoBehaviour
{
    private Swipe swipe;

    // Start is called before the first frame update
    private void Start()
    {
        swipe = GetComponent<Swipe>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            swipe.GoSwipe = true;
            Debug.Log("스와이프 준비됨");
        }

        if (swipe.GoSwipe)
        {
            //swipe.SwipeProcess(() => aa());
            swipe.LeftNRightSwipe(() => aa());

            swipe.RightNLeftSwipe(() => aa());

            swipe.UpNDownSwipe(() => aa());

            swipe.DownNUpSwipe(() => aa());
        }
    }

    public void aa()
    { }
}