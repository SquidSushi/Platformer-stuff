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


        public override void Motion() {
            throw new System.NotImplementedException();
        }

        public override void StateSwap()
        {
            throw new System.NotImplementedException();
        }
    }
}