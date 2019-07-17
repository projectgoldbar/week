using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed = 11.0f;
    public float originMaxSpeed = 11.0f;
    public float speed = 0.0f;
    public float accelSpeed = 1f;
    public float downSpeed = 1f;
    public float slowSpeed = 0f;
    public float rollDownSpeedTime = 2f;//구르고 난후 느려지는 시간

    public DynamicJoystick dynamicJoystick;
    private NavMeshAgent agent;
    public PlayerData playerData;
    public EvadeSystem evadeSystem;
    public bool isRoll = false;

    [Header("플레이어방향UI")]
    public float rollSensitive;

    public bool accel = false;
    public bool isShaking = false;
    public int equipIdx = 0;

    public ParticleSystem RunnerTail = null;
    public ParticleSystem Portal = null;
    public ParticleSystem biteParticle = null;
    public AnimationEvents animEvent;
    public float shakingDuration = 5f;
    private WaitForSeconds shakeDuration;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        evadeMove = new Action[11] { () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { } };
        evadeMove[0] = NomalRoll;
        evadeMove[1] = Rugby;
        evadeMove[2] = Chearleader;
        evadeMove[3] = Nurse;
        evadeMove[4] = Potal;
        evadeMove[5] = CeleryMan;
        evadeMove[6] = MotorCycle;
        evadeMove[7] = Shaker;
        evadeMove[8] = EnergyShield;
        evadeMove[9] = NomalRoll;
        evadeMove[10] = NomalRoll;
        startPos = new Vector2(-720f, -1280f);
        //knob.position = startPos;
        //center.position = startPos;
        //animEvent = playerData.animator.GetComponent<AnimationEvents>();
        shakeDuration = new WaitForSeconds(shakingDuration);
        Portal.Stop();
        //StartCoroutine(Shake());
    }

    private Vector2 bufferVector2;
    private Vector2 inputVector2;
    private Vector2 start;
    private float speedDistance;

    public bool CalamityRoll = false;
    public bool inputStey = false;

    public int biteCount = 0;

    #region 상현이 코드 추가

    public RectTransform center;
    public RectTransform knob;
    public Vector2 startPos;
    public bool evadeRotate = false;
    public Vector2 direction;
    public float range = 1f;
    private float distance;

    #endregion 상현이 코드 추가

    public void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;
        biteCount = playerData.biteZombies.Count;
        if (biteCount <= 0)
        {
            biteParticle.Stop();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (accel) return;
            //StartCoroutine(FadeIn(0));
            inputVector2 = Input.mousePosition;
            bufferVector2 = Input.mousePosition;
            Quaternion dir = Quaternion.LookRotation(inputVector2);
            transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.deltaTime * 10.0f);
            accel = true;
        }

        if (accel == true)
        {
            if (maxSpeed > speed)
            {
                speed += accelSpeed;
            }
            else if (equipIdx == 6 && playerData.ep > 0 && maxSpeed + 5f > speed)
            {
                speed += accelSpeed;
                if (!RunnerTail.isPlaying) RunnerTail.Play();
                playerData.ep -= playerData.MainusEP * Time.deltaTime;
            }
            else
            {
                if (RunnerTail.isPlaying) RunnerTail.Stop();
                speed = maxSpeed;
            }
        }
        else
        {
            if (0f < speed)
            {
                speed -= downSpeed;
            }
            else if (speed <= 0f)
            {
                speed = 0f;
                evadeMove[equipIdx]();
            }
        }

        if (Input.GetMouseButton(0))
        {
            distance = DisNdir(mousePosition, start).dis;
            speedDistance = DisNdir(mousePosition, bufferVector2).dis;
            var speed = speedDistance * 0.045f;
            var dir = DisNdir(mousePosition, inputVector2).dir;

            Vector3 moveDirection = new Vector3(dir.x, 0, dir.y);
            Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);

            #region 구르기 기능

            //if (speed > rollSensitive && !isRoll)
            //{
            //    if (equipIdx == 2 && playerData.Hp > playerData.rollEp)
            //    {
            //        Debug.Log("치어리더성공");
            //        var dir = DisNdir(mousePosition, bufferVector2).dir;
            //        Vector3 rollDirection = new Vector3(dir.x, 0, dir.y);
            //        transform.rotation = Quaternion.LookRotation(rollDirection);
            //        evadeMove[equipIdx]();
            //        if (biteCount > 0)
            //        {
            //            for (int i = 0; i < playerData.biteZombies.Count;)
            //            {
            //                var x = playerData.biteZombies.Dequeue();
            //                x.transform.parent = null;
            //                x.GetComponent<ZombieState.Zombie_Bite>().ZombieDown();
            //            }
            //        }
            //    }
            //    else if (equipIdx != 2 && playerData.ep > playerData.rollEp)
            //    {
            //        Debug.Log("롤입력성공");
            //        var dir = DisNdir(mousePosition, bufferVector2).dir;
            //        Vector3 rollDirection = new Vector3(dir.x, 0, dir.y);
            //        transform.rotation = Quaternion.LookRotation(rollDirection);

            //        evadeMove[equipIdx]();

            //        if (biteCount > 0)
            //        {
            //            for (int i = 0; i < playerData.biteZombies.Count;)
            //            {
            //                var x = playerData.biteZombies.Dequeue();
            //                x.transform.parent = null;
            //                x.GetComponent<ZombieState.Zombie_Bite>().ZombieDown();
            //            }
            //        }
            //    }
            //}

            #endregion 구르기 기능
        }

        if (Input.GetMouseButtonUp(0))
        {
            //StartCoroutine(FadeIn(1));
            accel = false;
        }
        bufferVector2 = Input.mousePosition;

        agent.velocity = agent.transform.forward * (speed - (slowSpeed + biteCount));

        if (biteCount > 0 && isShaking == false && playerData.hp > 0)
        {
            biteParticle.Play();
            StartCoroutine(Shake());
        }
        

        playerData.animator.SetFloat("moveSpeed", speed);
    }

    public (float dis, Vector3 dir) DisNdir(Vector3 aa, Vector3 bb)
    {
        var Init = (aa - bb);

        var dir = Init.normalized;
        var dis = Init.magnitude;

        return (dis, dir);
    }

    //-------------------- 복장입어서 추가되는 동작들------

    public Action[] evadeMove;

    private void NomalRoll()
    {
        //SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        //playerData.animator.Play("Roll");
        //playerData.ep -= playerData.rollEp;
        playerData.Hp = playerData.breathingHp * Time.deltaTime;
    }

    //럭비
    private void Rugby()
    {
        playerData.Hp = playerData.breathingHp * Time.deltaTime;
    }

    private void Chearleader()
    {
        playerData.Hp = playerData.breathingHp * Time.deltaTime;
        if (EpCheck(playerData.rollEp + 2f)&&playerData.manager.isPause==false)
        {
            playerData.ep -= 10f;
            StartCoroutine(ZeroWorld());
        }
    }

    private IEnumerator ZeroWorld()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        yield break;
    }

    private void Nurse()
    {
        //SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        //playerData.animator.Play("Roll");
        //playerData.ep -= playerData.rollEp;
        playerData.Hp = playerData.breathingHp * Time.deltaTime;
    }

    public bool potalOpen = false;
    public GameObject potal;
    public GameObject potalOut;

    private void Potal()
    {
        //Portal.transform.position = transform.position + (transform.forward * 20);
        //Portal.gameObject.SetActive(true);
        //Portal.Stop();

        //if (Portal.GetComponent<PortalEffectCollider>().is_Building)
        //{
        //    SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        //    playerData.animator.Play("Roll");
        //}
        //else
        //{
        //    //포탈구르기
        //    playerData.animator.Play("portalOpen");
        //    potalOpen = true;
        //    accel = false;
        //}
        if (EpCheck(playerData.rollEp + 2f))
        {
            var startPoint = transform.position + transform.forward * 4f;

            if (!SomethingOnPlace(startPoint))
            {
                float endDistance = 25f;
                var endPoint = transform.position + transform.forward * endDistance;
                if (!SomethingOnPlace(endPoint))
                {
                    potal.transform.rotation = transform.rotation;
                    potal.transform.position = transform.position + transform.forward * 5f;
                    potalOut.transform.rotation = transform.rotation;
                    potalOut.transform.position = transform.position + transform.forward * endDistance;
                    potal.SetActive(true);
                    playerData.ep -= 10f;
                    StartCoroutine(PotalEndCoroutine());
                }
                else
                {
                    endDistance += 4f;
                    if (!SomethingOnPlace(endPoint))
                    {
                        potal.transform.rotation = transform.rotation;
                        potal.transform.position = transform.position + transform.forward * 5f;
                        potalOut.transform.rotation = transform.rotation;
                        potalOut.transform.position = transform.position + transform.forward * endDistance;
                        potal.SetActive(true);
                        playerData.ep -= 10f;
                    }
                }
            }
        }

        playerData.Hp = playerData.breathingHp * Time.deltaTime;
    }

    private IEnumerator PotalEndCoroutine()
    {
        yield return new WaitForSeconds(2f);
        potal.SetActive(false);
    }

    private void CeleryMan()
    {
        playerData.Hp = playerData.breathingHp * Time.deltaTime;
    }

    private void MotorCycle()
    {
        //SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        //if (playerData.ep >= 0)
        //{
        //    playerData.ep -= playerData.rollEp * Time.deltaTime;
        //}
        //else
        //{
        //    playerData.ep = 0f;
        //}

        playerData.Hp = playerData.breathingHp * Time.deltaTime;
    }

    public bool wormSkinEquipRolling;

    private void Shaker()
    {
        //SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        //playerData.animator.Play("Roll");
        //wormSkinEquipRolling = true;
        //if (playerData.ep >= 0)
        //{
        //    playerData.ep -= playerData.rollEp * Time.deltaTime;
        //}
        //else
        //{
        //    playerData.ep = 0f;
        //}

        playerData.Hp = playerData.breathingHp * Time.deltaTime;
    }

    private void EnergyShield()
    {
        playerData.Hp = playerData.breathingHp * Time.deltaTime;
    }

    private void j()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    private void k()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    private bool EpCheck(float x)
    {
        if (playerData.ep > x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Shake()
    {
        isShaking = true;
        if (playerData.isGameOver)
        {
            yield break;
        }
        while (biteCount > 0)
        {
            if (EpCheck(playerData.rollEp) && biteCount > 0)
            {
                if (playerData.isGameOver)
                {
                    yield break;
                }
                playerData.animator.Play("HitSide");
            }
            yield return shakeDuration;
        }
        isShaking = false;
        yield break;
    }

    private bool SomethingOnPlace(Vector3 point)
    {
        Vector3 rayStartPoint = new Vector3(point.x, point.y + 80f, point.z);
        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 200f, 1 << 11))
        {
            rayStartPoint.y = point.y;
            rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Left, 1f);
            if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
            {
                rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Right, 1f);
                if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                {
                    rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Back, 1f);
                    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                    {
                        rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Foward, 1f);
                        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 피벗기준으로 rotation방향으로 distance만큼 떨어진 위치를 반환
    /// </summary>
    /// <param name="pivot"></param>
    /// <param name="rotation"></param>
    private Vector3 PivotPointSet(Vector3 pivot, Vector3 origin, Direction direction, float distance)
    {
        switch (direction)
        {
            case Direction.Left:
                pivot = origin;
                pivot.x -= distance;
                break;

            case Direction.Right:
                pivot = origin;
                pivot.x += distance;
                break;

            case Direction.Foward:
                pivot = origin;
                pivot.z += distance;

                break;

            case Direction.Back:
                pivot = origin;
                pivot.z -= distance;

                break;

            default:
                break;
        }
        return pivot;
    }
}