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
            //while falling, add gravity to the player velocity
            player.vel -= new Vector2(0, player.numbers.DefaultGravity * Time.deltaTime);
            player.transform.Translate(player.vel * Time.deltaTime);
        }

        public override void StateSwap()
        {
            //check if touches ground, then swap players state to a new standing
            var hit = Physics2D.Linecast(player.transform.position, player.transform.position + new Vector3(0, -HitboxDown()), player.groundLayer);
            if (hit.collider != null)
            {
                player.state = new Standing(player);
            }
        }
    }
}