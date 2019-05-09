using System.Collections;
using UnityEngine;

namespace ZombieState
{
    public class Zombie_Chase : Zombie_Moving
    {
        public float pathCount;
        public float coolDownCount;
        private WaitForSeconds coolDownSecond;
        public bool coolDown = false;

        public override void Setting()
        {
            resetPathCount = pathCount;
            coolDownSecond = new WaitForSeconds(coolDownCount);
            zombieData.canIAttackCoroutine = CanIAttack();
            base.Setting();
        }

        public override void Initiate()
        {
            coolDown = false;
        }

        public override void Execute()
        {
            base.Execute();
            StartCoroutine(zombieData.canIAttackCoroutine);
        }

        private IEnumerator CanIAttack()
        {
            int a = 0;
            while (true)
            {
                //Debug.Log(coolDown);
                yield return coolDownSecond;
                if (coolDown == true)
                {
                    if (Vector3.Distance(transform.position, zombieData.player.position) < 10f)
                    {
                        coolDown = false;
                        StateChange(zombieData.attack);
                        //Debug.Log("상태변화호출");
                        continue;
                    }
                }
                coolDown = true;
                //a++;
                //Debug.Log(a);
            }
        }

        public override void Exit()
        {
            StopCoroutine(zombieData.canIAttackCoroutine);
            base.Exit();
            this.enabled = false;
            //Debug.Log("이동상태exit");
        }
    }
}