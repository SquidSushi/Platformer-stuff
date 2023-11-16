using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerStateMachine
{
    public abstract class PlayerState
    {
        public PlayerState(PlayerController player)
        {
            this.player = player;
            this.StateAge = 0;
            OnEnter();
        }
        public PlayerController player;
        public float StateAge { get; protected set; }

        public abstract PlayerState Update(PlayerController player);
        
        public abstract float HitboxDown();
        public abstract float HitboxUp();
        public abstract float HitboxFront();
        public abstract float HitboxBack();
        public abstract String Name();

        public abstract bool Grounded();
        public virtual Vector2 CamOffset()
        {
            return new Vector2(0, 0);
        }


        public virtual void OnEnter()
        {
            if (player.numbers.Debug)
            {
                Debug.Log("Entering State" + this.GetType().Name);
            }
            player.anim.Play(Name());
        }
        public virtual void OnExit()
        {
            if (player.numbers.Debug)
            {
                Debug.Log("Exiting State" + this.GetType().Name);
            }
        }
    }

    public class Standing : PlayerState
    {
        //new constructor
        public Standing(PlayerController player) : base(player)
        {
            
        }
        public override float HitboxBack()
        {
            return player.numbers.StandingHitboxBack;
        }

        public override float HitboxDown()
        {
            return player.numbers.StandingHitboxDown;
        }

        public override float HitboxFront()
        {
            return player.numbers.StandingHitboxFront;
        }

        public override float HitboxUp()
        {
            return player.numbers.StandingHitboxUp;
        }

        public override string Name() {
            return "Standing";
        }

        public override bool Grounded() { return true;}
        public override PlayerState Update(PlayerController player)
        {
            //check for ground
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, this.HitboxDown() + player.numbers.StepCheck * Time.deltaTime, player.groundLayer);
            if (hit.collider != null)
            {
                //if ground, stay in state but move to ground
                float move = HitboxDown() - hit.distance;
                player.transform.position += new Vector3(0, move);
            } else {
                return new Falling(player);
            }

            
            //get movement input and return Running if not running into a wall
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
                player.facingLeft = true;
                RaycastHit2D wallHit = Physics2D.Raycast(player.transform.position, Vector2.left, this.HitboxFront(), player.groundLayer);
                if (wallHit.collider != null) {
                    //if wall, stay in state but move to wall
                    float move = HitboxFront() - wallHit.distance;
                    player.transform.position += new Vector3(move, 0);
                    return this;
                } else {
                    return new Running(player);
                }
                
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
                player.facingLeft = false;
                RaycastHit2D wallHit = Physics2D.Raycast(player.transform.position, Vector2.right, this.HitboxFront(), player.groundLayer);
                if (wallHit.collider != null) {
                    //if wall, stay in state but move to wall
                    float move = HitboxFront() - wallHit.distance;
                    player.transform.position -= new Vector3(move, 0);
                    return this;
                } else {
                    return new Running(player);
                }
            }

            return this;
        }

        public override void OnEnter() {
            base.OnEnter();
            player.vel.y = 0;
            player.vel.x = 0;
        }
    }

    public class Falling : PlayerState
    {
        public Falling(PlayerController player) : base(player)
        {
            
        }

        public override string Name() {
            return "Falling";
        }
        public override bool Grounded() { return false;}

        public override float HitboxBack()
        {
            return player.numbers.StandingHitboxBack;
        }

        public override float HitboxDown()
        {
            return player.numbers.StandingHitboxDown;
        }

        public override float HitboxFront()
        {
            return player.numbers.StandingHitboxFront;
        }

        public override float HitboxUp()
        {
            return player.numbers.StandingHitboxUp;
        }

        public override PlayerState Update(PlayerController player)
        {
            player.vel.y -= player.numbers.DefaultGravity * Time.deltaTime;
            player.vel.y = Mathf.Max(player.vel.y, -player.numbers.MaxFallSpeed);
            player.transform.Translate(player.vel * Time.deltaTime);
            //Raycast for walls and push away from them
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, player.FrontVec(), this.HitboxFront(), player.groundLayer);
            if (hit.collider != null) {
                float move = HitboxFront() - hit.distance;
                if (player.facingLeft) {
                    player.transform.position += new Vector3(move, 0);
                } else {
                    player.transform.position -= new Vector3(move, 0);
                }
                player.vel.x = 0;
            }
            hit = Physics2D.Raycast(player.transform.position, -player.FrontVec(), this.HitboxBack(), player.groundLayer);
            if (hit.collider != null) {
                float move = HitboxBack() - hit.distance;
                player.transform.position += new Vector3(move * -player.FrontVec().x, 0);
                player.vel.x = 0;
            }

            //check for Ground
            hit = Physics2D.Raycast(player.transform.position, Vector2.down, this.HitboxDown(), player.groundLayer);
            if (hit.collider != null)
            {
                //if ground, move to ground and return standing
                float move = HitboxDown() - hit.distance;
                player.transform.position += new Vector3(0, move);
                return new Running(player);
            }
            else
            {
                return this;
            }
        }
    }

    public class Running: PlayerState
    {
        public Running(PlayerController player) : base(player)
        {
            
        }

        public override void OnEnter() {
            base.OnEnter();
            player.vel.y = 0;
        }

        public override string Name() {
            return "Running";
        }

        public override bool Grounded(){return true;}

        public override float HitboxBack()
        {
            return player.numbers.StandingHitboxBack;
        }

        public override float HitboxDown()
        {
            return player.numbers.StandingHitboxDown;
        }

        public override float HitboxFront()
        {
            return player.numbers.StandingHitboxFront;
        }

        public override float HitboxUp()
        {
            return player.numbers.StandingHitboxUp;
        }

        public override PlayerState Update(PlayerController player)
        {
            //if neither a or d are pressed, return standing
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && Math.Abs(player.vel.x) < player.numbers.StandThreshhold)
            {
                player.vel.x = 0;
                return new Standing(player);
            }
            if (Input.GetKey(KeyCode.A))
            {
                player.vel.x -= player.numbers.WalkAcceleration * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                player.vel.x += player.numbers.WalkAcceleration * Time.deltaTime;
            }
            player.facingLeft = player.vel.x < 0;
            //check for ground and move to ground
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, this.HitboxDown() + Mathf.Abs(player.vel.x * 2 * Time.deltaTime) + 1E-4f, player.groundLayer);
            if (hit.collider != null) {
                //if ground, stay in state but move to ground
                float move = HitboxDown() - hit.distance;
                player.transform.position += new Vector3(0, move);
            } else {
                return new Falling(player);
            }
            //check for wall and put Samus in a standing state
            hit = Physics2D.Raycast(player.transform.position, player.FrontVec(), this.HitboxFront(), player.groundLayer);
            if (hit.collider != null) {
                //if wall, stay in state but move to wall
                float move = HitboxFront() - hit.distance;
                player.transform.position += new Vector3(move * player.FrontVec().x, 0);
                player.vel.x = 0;
                return new Standing(player);
            }

            //multiply by deacceleration
            player.vel.x *= Mathf.Pow(player.numbers.WalkingDeacceleration, Time.deltaTime);
            player.transform.Translate(player.vel * Time.deltaTime);
            return this;
        }
    }
}
