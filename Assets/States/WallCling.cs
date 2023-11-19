using PlayerStateMachine;
using UnityEngine;

class WallCling : PlayerState
{
    public WallCling(PlayerController player) : base(player)
    {
    }

    public void StickToWall() {
        Vector2 direction = -player.FrontVec();
        var hit = Physics2D.BoxCast(
            player.transform.position,
            new Vector2(0.02f, player.numbers.SpinHitboxBack.y),
            0,
            direction,
            1,
            player.groundLayer
            );
        if (hit.collider != null) {
            float move = hit.distance - player.numbers.WallClingBack - 0.01f;
            player.transform.Translate(new Vector3(direction.x * move, 0));
        }

    }

    private Vector2 JumpDirection() {
        float direction = AxisDir(Inputs.Walking.ReadValue<Vector2>().x);
        direction *= Bool2Axis(!player.facingLeft);
        float facingDirection = Bool2Axis(!player.facingLeft);
        switch (direction) {
            case 1:
                return new Vector2(player.numbers.WalljumpImpulseOutward.x * facingDirection, player.numbers.WalljumpImpulseOutward.y);
            case -1:
                return new Vector2(player.numbers.WalljumpImpulseInward.x * facingDirection, player.numbers.WalljumpImpulseInward.y);
            default:
                return new Vector2(player.numbers.WalljumpImpulseNeutral.x * facingDirection, player.numbers.WalljumpImpulseNeutral.y);
        }
    }

    public override Vector3 CamOffset() {
        return JumpDirection() * 0.5f;
    }

    public override bool Grounded() { return false; }

    public override Vector2 HitboxBack()
    {
        return new Vector2(0, player.numbers.SpinHitboxBack.y);
    }

    public override Vector2 HitboxBackOffset()
    {
        return player.numbers.SpinHitboxBackOffset;
    }

    public override Vector2 HitboxDown()
    {
        return player.numbers.SpinHitboxDown;
    }

    public override Vector2 HitboxDownOffset()
    {
        return player.numbers.SpinHitboxDownOffset;
    }

    public override Vector2 HitboxFront()
    {
        return player.numbers.SpinHitboxFront;
    }

    public override Vector2 HitboxFrontOffset()
    {
        return player.numbers.SpinHitboxFrontOffset;
    }

    public override Vector2 HitboxUp()
    {
        return player.numbers.SpinHitboxUp;
    }

    public override Vector2 HitboxUpOffset()
    {
        return player.numbers.SpinHitboxUpOffset;
    }

    public override void Motion()
    {
        //Eventually this should track the wall the players clings to and move the player towards it
        //For now, just do nothing
    }

    public override string Name()
    {
        return "WallCling";
    }

    public override void StateSwap()
    {
        if (Inputs.Jump.WasReleasedThisFrame())
        {
            player.state = new Spin(player);
            player.vel = JumpDirection();
        }
        if (StateAge > player.numbers.WallJumpTime)
        {
            player.vel = Vector2.zero;
            player.state = new Falling(player);
        }
    }
}