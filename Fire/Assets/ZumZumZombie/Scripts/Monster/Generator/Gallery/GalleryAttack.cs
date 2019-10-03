public class GalleryAttack : Attack
{
    public Player player;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Initiate()
    {
        unit.Anim.SetBool("GalleryAttack", true);
    }

    public override void Execution()
    {
        if (unit.distance > 3.0f)
        {
            state.ChangeState(StateIndex.CHASE);
        }
    }

    public override void Exit()
    {
        unit.Anim.SetBool("GalleryAttack", false);
    }
}