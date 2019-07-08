namespace ZombieState
{
    public class ZombieRunBite : Zombie_AttackRun
    {
        public override void Execute()
        {
            transform.tag = "BiteZombie";
        }

        public void ChangeBite()
        {
            StateChange(zombieData.zombieBite);
        }

        public override void Exit()
        {
            transform.tag = "Zombie";
            base.Exit();
        }
    }
}