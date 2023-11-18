using PlayerStateMachine;
using UnityEngine;

namespace PlayerStateMachine {
    public class Standing : PlayerState {
        //new constructor
        public Standing(PlayerController player) : base(player) {

        }
        public override float HitboxBack() {
            return player.numbers.StandingHitboxBack;
        }

        public override float HitboxDown() {
            return player.numbers.StandingHitboxDown;
        }

        public override float HitboxFront() {
            return player.numbers.StandingHitboxFront;
        }

        public override float HitboxUp() {
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
            if (Inputs.Jump.triggered)
            {
                //player.state = new Jumping(player);
            }
            var hit = Physics2D.Linecast(player.transform.position, player.transform.position + new Vector3(0, -(HitboxDown())), player.groundLayer);
            if (hit.collider == null)
            {
                player.state = new Falling(player);
            }
        }
    }
}