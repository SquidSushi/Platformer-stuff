using PlayerStateMachine;
using UnityEngine;

namespace PlayerStateMachine {
    public class Standing : PlayerState {
        //new constructor
        public Standing(PlayerController player) : base(player) {

        }
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

        public override string Name() {
            return "Standing";
        }

        public override bool Grounded() { return true; }
        

        public override void OnEnter() {
            base.OnEnter();
            player.vel = Vector2.zero;
        }

        public override void Motion() {
            //Standing doesnt have motion nor any other effects to happen during it :)
        }

        public override void StateSwap()
        {
            if (Inputs.Walking.ReadValue<Vector2>().x != 0)
            {
                player.state = new Running(player);
            }
            if (Inputs.Jump.IsPressed())
            {
                player.state = new Falling(player);
                player.vel.y = player.numbers.JumpImpulse();
            }
            //var hit = Physics2D.Linecast(player.transform.position, player.transform.position + new Vector3(0, - HitboxDown().x-0.1f), player.groundLayer);
            //rewrite above line as boxcast
            var hit = Physics2D.BoxCast(player.transform.position + new Vector3(HitboxDownOffset().x, HitboxDownOffset().y), new Vector2(HitboxDown().y, 0.02f), 0, Vector2.down, HitboxDown().x, player.groundLayer);
            if (hit.collider == null)
            {
                player.state = new Falling(player);
            }
            else
            {
                float move = HitboxDown().x - hit.distance;
                player.transform.Translate(Vector3.up * move);
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
            return  player.numbers.StandingHitboxBackOffset;
        }
    }
}