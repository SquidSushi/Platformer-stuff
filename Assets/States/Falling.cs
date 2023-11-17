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
    }
}