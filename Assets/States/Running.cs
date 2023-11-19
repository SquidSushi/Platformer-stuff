using PlayerStateMachine;
using UnityEngine;
namespace PlayerStateMachine {
    public class Running : PlayerState {
        public Running(PlayerController player) : base(player) {

        }

        public override void OnEnter() {
            base.OnEnter();
            player.vel.y = 0;
        }

        public override string Name() {
            return "Running";
        }

        public override bool Grounded() { return true; }

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
            Vector2 accel = Vector2.zero;
            float cachedWalk = Inputs.Walking.ReadValue<Vector2>().x;
            if (cachedWalk != 0)
            {
                accel.x += cachedWalk * player.numbers.WalkAcceleration * Time.deltaTime;
                if (cachedWalk * player.vel .x < 0)
                {
                    accel.x *= player.numbers.TurnInstantSpeedFactor;
                }
                player.vel.x += accel.x;
                player.facingLeft = cachedWalk < 0;
                player.vel.x *= Mathf.Pow(player.numbers.WalkFriction,Time.deltaTime);
            }
            else
            {
                player.vel *= Mathf.Pow(player.numbers.WalkFriction * player.numbers.WalkFriction,Time.deltaTime);
            }
            //check if touches ground while walking down and move down to ground;
            
            player.transform.Translate(player.vel * Time.deltaTime);
            var hit = Physics2D.Linecast(player.transform.position, player.transform.position + new Vector3(0, -HitboxDown().x - player.numbers.StepCheck * Time.deltaTime), player.groundLayer);
            if (hit.collider != null)
            {
                player.transform.Translate(Vector3.up * (HitboxDown().x - hit.distance));
            }
        }

        public override void StateSwap()
        {
            //on jump press, add jump impulse and swap to spin;
            if (Inputs.Jump.WasPerformedThisFrame())
            {
                player.state = new Spin(player);
                player.vel.y = player.numbers.JumpImpulse();
                return;
            }
            var hit = Physics2D.Linecast(player.transform.position, player.transform.position + new Vector3(0, -HitboxDown().x - player.numbers.StepCheck * Time.deltaTime), player.groundLayer);
            if (hit.collider == null)
            {
                player.state = new Falling(player);
                return;
            }
            //if walk input is zero and abs velocity less than threshhold, swap to standing
            if (Inputs.Walking.ReadValue<Vector2>().x == 0 && Mathf.Abs(player.vel.x) < player.numbers.StandThreshhold)
            {
                player.vel.x = 0;
                player.state = new Standing(player);
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