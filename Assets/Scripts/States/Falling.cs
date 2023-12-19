using PlayerStateMachine;
using UnityEngine;
namespace PlayerStateMachine {
    public class Falling : PlayerState {
        public Falling(PlayerController player) : base(player) {

        }

        public override string Name() {
            return "Falling";
        }
        public override bool Grounded() { return false; }

        public override Vector2 HitboxBack() {
            return player.numbers.StandingHitboxBack;
        }

        public override Vector2 HitboxDown() {
            return player.numbers.StandingHitboxDown;
        }

        public override Vector2 HitboxFront() {
            return player.numbers.StandingHitboxFront;
        }

        public override Vector2 HitboxUp() {
            return player.numbers.StandingHitboxUp;
        }

        public override void Motion() {
            //while falling, add gravity to the player velocity
            player.vel -= new Vector2(0, player.numbers.DefaultGravity * Time.deltaTime);
            player.vel += Inputs.Walking.ReadValue<Vector2>() * player.numbers.AirborneAcceleration * Time.deltaTime;
            if (Inputs.Jump.IsPressed() && player.timeSinceJump < player.numbers.JumpHoldTime) { 
                player.vel += new Vector2(0, player.numbers.jumpImpulse * Time.deltaTime/player.numbers.JumpHoldTime);
            }
            player.transform.Translate(player.vel * Time.deltaTime);
        }

        public override void StateSwap()
        {
            float p = 0;
            if (TouchesGround(ref p))
            {
                player.state = new Running(player);
            }
        }

        public override Vector2 HitboxDownOffset()
        {
            return player.numbers.StandingHitboxDownOffset;
        }

        public override Vector2 HitboxUpOffset()
        {
            return player.numbers.StandingHitboxUpOffset;
        }

        public override Vector2 HitboxFrontOffset()
        {
            return player.numbers.StandingHitboxFrontOffset;
        }

        public override Vector2 HitboxBackOffset()
        {
            return player.numbers.StandingHitboxBackOffset;
        }
    }
}