using UnityEngine;

public class Jump : MonoBehaviour
{
    public Player player;
    public Move move;
    public Swipe swipe;

    // Start is called before the first frame update
    private void Awake()
    {
        swipe = FindObjectOfType<Swipe>();
        //swipe.GoSwipe = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
//<<<<<<< HEAD
            swipe.GoSwipe = true;
//=======
            //swipe.GoSwipe = true;
//>>>>>>> merge
            PlayerJump();
            Debug.Log("스와이프 준비됨");
        }
        if (swipe.GoSwipe)
        {
            // swipe.UpNDownSwipe(() => PlayerJump());
            swipe.DownNUpSwipe(() => PlayerJump());
            //  swipe.RightNLeftSwipe(() => PlayerJump());
            // swipe.LeftNRightSwipe(() => PlayerJump());
        }
        if (player.Anim.GetCurrentAnimatorStateInfo(0).IsName("BigJump"))
        {
            if (player.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.35f)
            {
            }
            if (player.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
            {
                player.Anim.speed = 2.0f;
            }
            if (player.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                player.Anim.speed = 1.0f; move.agent.enabled = true;
            }
        }

        // else
        // player.Anim.speed = 1;
    }

    public void PlayerJump()
    {
        player.Anim.SetTrigger("Jump");
        //swipe.GoSwipe = true;
        move.agent.enabled = false;
        pomulseon Fly = move.GetComponent<pomulseon>();
        //Fly.flySpd = 15f;
        Fly.FlyToTarget(move.transform,
                        move.transform.position,
                        move.transform.position + move.transform.forward * 10.0f,
                        EndAnimNPomul, 19.8f, 4.0f);
    }

    public void EndAnimNPomul(Transform a)
    {
        move.agent.enabled = true;
        swipe.GoSwipe = false;
        move.rotState = Move.State.ADVANCE;
    }
}