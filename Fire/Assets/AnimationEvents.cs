using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerMove playerMove;
    public Animator animator;
    public float evadeSpeed = 20f;
    public SkinnedMeshRenderer meshRenderer;


    public void RollStart()
    {
        playerMove.isRoll = true;
        playerMove.maxSpeed = evadeSpeed;
        playerMove.speed = evadeSpeed;
        playerMove.playerData.rollStack--;
        playerMove.evadeSystem.enabled = true;


        RollDfUp();
    }

    public void RollEnd()
    {
        playerMove.isRoll = false;
        playerMove.maxSpeed = 11f;
        playerMove.speed = 11f;
        playerMove.playerData.rollStack++;
        playerMove.evadeSystem.enabled = false;

        animator.StopPlayback();
        animator.SetBool("Roll", false);
        animator.speed = 1f;

        RollDfDown();
    }

    public void SlowDown()
    {
        animator.speed = 2f;
        playerMove.maxSpeed = 6f;
        playerMove.speed = 6f;
    }

    public void DieStart()
    {
        playerMove.enabled = false;
        FindObjectOfType<UITweenEffectManager>().LeaveInGame();
    }

    public void GameOver()
    {
        FindObjectOfType<Manager>().GameOver();
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>


    // 포탈 생성 <애니메이션> 끝날때 
    public void CreatePotal()
    {
        //포탈 위치
        playerMove.Portal.transform.position = transform.position + transform.up * 1.5f +(transform.forward * 3.0f);
        //포탈 보여주고 
        playerMove.Portal.Play();
        //n초후 구르기
        Invoke("RollingStart", 1.0f);

    }
    public void RollingStart()
    {
        //구르는 애니메이션 실행
        playerMove.playerData.animator.Play("Roll");
    }

    #region 구르기 애니메이션 이벤트로 넣음.
    public void RollDfUp()
    {
        playerMove.playerData.df += playerMove.playerData.WormData;
        if (playerMove.CalamityRoll)
        {
            playerMove.playerData.df += 1;
        }
        if (playerMove.wormSkinEquipRolling)
        {
            playerMove.playerData.df += 3;
        }

        #region 구를때 소시지 스킨이라면 속도올려?
        if (playerMove.equipIdx == 3)
        {
            playerMove.maxSpeed = 100000;
            playerMove.speed = 30;
            playerMove.Portal.Stop();
            playerMove.Portal.gameObject.SetActive(false);
            Invoke("RendederDelay", 0.1f);
        }
        #endregion
    }

    public void RendederDelay()
    {
        RendererNColliderOnOff(false);
    }



    public void RendererNColliderOnOff(bool b)
    {
        playerMove.GetComponent<CapsuleCollider>().enabled = b;
        meshRenderer.enabled = b;
    }
        



    public void RollDfDown()
    {
        playerMove.playerData.df -= playerMove.playerData.WormData;
        if (playerMove.CalamityRoll)
        {
            playerMove.playerData.df -= 1;
            playerMove.CalamityRoll = false;
        }

        if (playerMove.wormSkinEquipRolling)
        {
            playerMove.playerData.df -= 3;
            playerMove.wormSkinEquipRolling = false;
        }

        #region 구르는게 끝날때 소시지 스킨이라면 속도 11?
        if (playerMove.equipIdx == 3)
        {
            playerMove.accel = true;
            playerMove.maxSpeed = 11;
            playerMove.speed = 11;
            RendererNColliderOnOff(true);

            //구르는게 끝났을때 포탈 보여주고 
            if (!playerMove.Portal.GetComponent<PortalEffectCollider>().is_Building)
            {
                playerMove.Portal.transform.position = transform.position + (transform.forward * 1.5f);
                playerMove.Portal.gameObject.SetActive(true);
                playerMove.Portal.Play();
                //포탈을 n초후 꺼줌
                Invoke("PortalEffectEnd", 1.0f);
            }
        }
        #endregion

    }
    #endregion

    public void PortalEffectEnd()
    {
        playerMove.Portal.Stop();
        playerMove.Portal.gameObject.SetActive(false);
    }
}