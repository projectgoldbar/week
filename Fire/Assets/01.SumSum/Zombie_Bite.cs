namespace ZombieState
{
    public class Zombie_Bite : ZombieState
    {
        public override void Setting()
        {
        }

        public override void Execute()
        {
            zombieData.sturnCollider.gameObject.SetActive(false);
            zombieData.agent.enabled = false;
            //zombieData.animator.StopPlayback();
            zombieData.animator.SetBool("Bite", true);
            zombieData.animator.Play("Bite");
        }

        public void ZombieDown()
        {
            zombieData.animator.SetBool("Bite", false);
            StateChange(zombieData.stun);
        }

        public void BiteDamage()
        {
            zombieData.player.GetComponent<PlayerData>().Hp = -1 * zombieData.damage;
        }

        public override void Exit()
        {
        }
    }
}