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
            player.vel.y = 0;
            player.vel.x = 0;
        }

        public override void Motion() {
            throw new System.NotImplementedException();
        }
    }
}