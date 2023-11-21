using PlayerStateMachine;
using UnityEngine;

class Spin : PlayerState
{
    public Spin(PlayerController player) : base(player)
    {

    }

    public override bool Grounded(){return false;}

    public override Vector3 CamOffset()
    {
        return player.vel * player.numbers.SpinCamProvidence;
    }

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
        player.vel.y -= player.numbers.DefaultGravity * Time.deltaTime;
        if (Mathf.Abs(player.vel.x) < player.numbers.AirborneAccelerationThreshhold || player.vel.x * Inputs.Walking.ReadValue<Vector2>().x < 0)
        {
            player.vel.x += Inputs.Walking.ReadValue<Vector2>().x * player.numbers.AirborneAcceleration * Time.deltaTime;
        }
        if (player.vel.y < -player.numbers.MaxFallSpeed)
        {
            player.vel.y = -player.numbers.MaxFallSpeed;
        }
        if (Inputs.Jump.IsPressed() && player.timeSinceJump < player.numbers.JumpHoldTime)
        {
            player.vel += new Vector2(0, player.numbers.jumpImpulse * Time.deltaTime / player.numbers.JumpHoldTime);
        }
        player.transform.Translate(player.vel * Time.deltaTime);
    }

    public override string Name()
    {
        //return class name
        return this.GetType().Name;
    }

    public override void StateSwap()
    {
        float d = 0;
        if (TouchesGround(ref d))
        {
            player.state = new Running(player);
        }
        if (TouchesWallFront(ref d) && Inputs.Jump.WasPerformedThisFrame())
        {
            player.state = new WallCling(player);
            player.facingLeft = !player.facingLeft;
            (player.state as WallCling).StickToWall();
        }
        if (TouchesWallBack(ref d) && Inputs.Jump.WasPerformedThisFrame()) {
            player.state = new WallCling(player);
            (player.state as WallCling).StickToWall();
        }
    }
}