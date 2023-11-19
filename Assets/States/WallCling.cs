using PlayerStateMachine;
using UnityEngine;

class WallCling : PlayerState
{
    public WallCling(PlayerController player) : base(player)
    {
    }

    public override bool Grounded() { return false; }

    public override Vector2 HitboxBack()
    {
        return player.numbers.SpinHitboxBack;
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
        if (StateAge > player.numbers.WallJumpTime || !Inputs.Jump.IsPressed())
        {
            player.state = new Spin(player);
            float direction = AxisDir(Inputs.Walking.ReadValue<Vector2>().x);
            direction *= Bool2Axis(!player.facingLeft);
            float facingDirection = Bool2Axis(!player.facingLeft);
            switch (direction)
            {
                case 1:
                    player.state = new Spin(player);
                    player.vel.x = player.numbers.WalljumpImpulseOutward.x * facingDirection;
                    player.vel.y = player.numbers.WalljumpImpulseOutward.y;
                    break;
                case -1:
                    player.state = new Spin(player);
                    player.vel.x = player.numbers.WalljumpImpulseInward.x * facingDirection;
                    player.vel.y = player.numbers.WalljumpImpulseInward.y;
                    break;
                default:
                    player.state = new Spin(player);
                    player.vel.x = player.numbers.WalljumpImpulseNeutral.x * facingDirection;
                    player.vel.y = player.numbers.WalljumpImpulseNeutral.y;
                    break;
            }
        }
    }
}