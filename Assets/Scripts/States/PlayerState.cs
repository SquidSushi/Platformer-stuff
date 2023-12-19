using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerStateMachine
{
    
    public abstract class PlayerState
    {
        protected static float AxisDir(float axis)
        {
            if (axis > 0)
            {
                return 1;
            }
            if (axis < 0)
            {
                return -1;
            }
            return 0;
        }
        protected static float Bool2Axis(bool b)
        {
            if (b)
            {
                return 1;
            }
            return -1;
        }
        public PlayerState(PlayerController player)
        {
            this.player = player;
            this.StateAge = 0;
            Inputs = player.Inputs.Movement;
        }
        public PlayerController player;
        public GameplayIAA.MovementActions Inputs;
        public float StateAge { get; protected set; }

        public void Update(PlayerController player) {
            this.StateAge += Time.deltaTime;
            Motion();
            Physics2D.SyncTransforms();
            StateSwap();
            if (player.state != this) {
                this.OnExit();
                player.state.OnEnter();
            }
            player.state.GenericCollision();
            Physics2D.SyncTransforms();
        }

        public abstract void Motion();
        public abstract void StateSwap();
        public void GenericCollision() {
            //generic collision check and moving out of collision
            float distance = 0;
            if (TouchesGround(ref distance)) {
                player.transform.Translate(Vector3.up * (HitboxDown().x - distance));
                if (player.vel.y < 0)
                {
                    player.vel.y = 0;
                }
            }
            if (TouchesWallFront(ref distance)) {
               player.transform.Translate(-player.FrontVec() * (HitboxFront().x - distance));
                if (player.vel.x * player.FrontVec().x > 0)
                {
                    player.vel.x = 0;
                }
            }
            if (TouchesWallBack(ref distance)) {
                player.transform.Translate(player.FrontVec() * (HitboxBack().x - distance));
                if (player.vel.x * player.FrontVec().x < 0)
                {
                    player.vel.x = 0;
                }
            }
            if (TouchesCeiling(ref distance)) {
                player.transform.Translate(Vector3.down * (HitboxUp().x - distance));
                if (player.vel.y > 0)
                {
                    player.vel.y = 0;
                }
            }

        }

        public bool TouchesGround(ref float outDistance) {
            Vector2 origin = player.transform.position + new Vector3(HitboxDownOffset().x,HitboxDownOffset().y);
            Vector2 direction = new Vector2(0, -1);
            float thickness = 0.02f;
            RaycastHit2D hit = Physics2D.BoxCast(origin,new Vector2(HitboxDown().y,thickness),0,direction,HitboxDown().x,player.groundLayer);
            if (hit.collider != null) {
                outDistance = hit.distance+thickness/2;
                return true;
            }
            return false;
        }

        public bool TouchesWallFront(ref float outDistance) {
            Vector2 origin = player.transform.position + new Vector3(HitboxFrontOffset().x,HitboxFrontOffset().y);
            Vector2 direction = new Vector2(player.FrontVec().x, 0);
            float thickness = 0.02f;
            RaycastHit2D hit = Physics2D.BoxCast(origin, new Vector2(thickness, HitboxFront().y), 0, direction, HitboxFront().x, player.groundLayer);
            if (hit.collider != null)
            {
                outDistance = hit.distance + thickness / 2;
                return true;
            }
            return false;
        }

        protected bool TouchesWallBack(ref float outDistance) {
            Vector2 origin = player.transform.position + new Vector3(HitboxBackOffset().x,HitboxBackOffset().y);
            Vector2 direction = new Vector2(-player.FrontVec().x, 0);
            float thickness = 0.02f;
            RaycastHit2D hit = Physics2D.BoxCast(origin, new Vector2(thickness, HitboxBack().y), 0, direction, HitboxBack().x, player.groundLayer);
            if (hit.collider != null)
            {
                outDistance = hit.distance + thickness / 2;
                return true;
            }
            return false;
        }

        public bool TouchesCeiling(ref float outDistance) {
            Vector2 origin = player.transform.position + new Vector3(HitboxUpOffset().x,HitboxUpOffset().y);
            Vector2 direction = new Vector2(0, 1);
            float thickness = 0.02f;
            RaycastHit2D hit = Physics2D.BoxCast(origin, new Vector2(HitboxUp().y, thickness), 0, direction, HitboxUp().x, player.groundLayer);
            if (hit.collider != null)
            {
                outDistance = hit.distance + thickness / 2;
                return true;
            }
            return false;
        }
        
        public abstract Vector2 HitboxDown();

        public abstract Vector2 HitboxDownOffset();
        public abstract Vector2 HitboxUp();
        public abstract Vector2 HitboxUpOffset();
        public abstract Vector2 HitboxFront();
        public abstract Vector2 HitboxFrontOffset();
        public abstract Vector2 HitboxBack();
        public abstract Vector2 HitboxBackOffset();
        public abstract String Name();

        public abstract bool Grounded();
        public virtual Vector3 CamOffset()
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




    
}
