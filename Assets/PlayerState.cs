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
                return this;
            }
            else
            {
                return new Falling(player);
            }
        }
    }

    public class Falling : PlayerState
    {
        public Falling(PlayerController player) : base(player)
        {
            
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
            //check for Ground
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, this.HitboxDown(), player.groundLayer);
            if (hit.collider != null)
            {
                //if ground, move to ground and return standing
                float move = HitboxDown() - hit.distance;
                player.transform.position += new Vector3(0, move);
                return new Standing(player);
            }
            else
            {
                return this;
            }
        }
    }

    public class Running: PlayerState
    {
        Running(PlayerController player) : base(player)
        {
            
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
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
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
            //multiply by deacceleration
            player.vel.x *= player.numbers.WalkingDeacceleration;
            player.transform.Translate(player.vel * Time.deltaTime);
            return this;
        }
    }
}
