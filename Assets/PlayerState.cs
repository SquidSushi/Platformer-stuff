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
        public PlayerState(PlayerController player)
        {
            this.player = player;
            this.StateAge = 0;
            Inputs = player.Inputs;
        }
        public PlayerController player;
        public GameplayIAA Inputs;
        public float StateAge { get; protected set; }

        public void Update(PlayerController player) {
            this.StateAge += Time.deltaTime;
            Motion();
            StateSwap();
            if (player.state != this) {
                player.state.OnExit();
                player.state.OnEnter();
            }
            player.state.GenericCollision();
        }

        public abstract void Motion();
        public void StateSwap() {
            
            
        }
        public void GenericCollision() {
            //generic collision check and moving out of collision
            float distance = 0;
            if (TouchesGround(ref distance)) {
                player.transform.position += new Vector3(0, distance);
                //player.vel.y = 0;
            }
            if (TouchesWallFront(ref distance)) {
                player.transform.position += new Vector3(player.FrontVec().x * distance, 0);
                //player.vel.x = 0;
            }
            if (TouchesWallBack(ref distance)) {
                player.transform.position += new Vector3(-player.FrontVec().x * distance, 0);
                //player.vel.x = 0;
            }
            if (TouchesCeiling(ref distance)) {
                player.transform.position += new Vector3(0, -distance);
                //player.vel.y = 0;
            }

        }

        public bool TouchesGround(ref float outDistance) {
            Vector2 origin = player.transform.position + new Vector3(0, -player.state.HitboxDown());
            Vector2 direction = new Vector2(0, -1);
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, HitboxDown(), player.groundLayer);
            if (hit.collider != null) {
                outDistance = hit.distance;
                return true;
            }
            return false;
        }

        public bool TouchesWallFront(ref float outDistance) {
            Vector2 origin = player.transform.position + new Vector3(player.state.HitboxFront() * player.FrontVec().x, 0);
            Vector2 direction = new Vector2(player.FrontVec().x, 0);
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, HitboxFront(), player.groundLayer);
            if (hit.collider != null) {
                outDistance = hit.distance;
                return true;
            }
            return false;
        }

        public bool TouchesWallBack(ref float outDistance) {
            Vector2 origin = player.transform.position + new Vector3(player.state.HitboxBack() * -player.FrontVec().x, 0);
            Vector2 direction = new Vector2(-player.FrontVec().x, 0);
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, HitboxBack(), player.groundLayer);
            if (hit.collider != null) {
                outDistance = hit.distance;
                return true;
            }
            return false;
        }

        public bool TouchesCeiling(ref float outDistance) {
            Vector2 origin = player.transform.position + new Vector3(0, player.state.HitboxUp());
            Vector2 direction = new Vector2(0, 1);
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, HitboxUp(), player.groundLayer);
            if (hit.collider != null) {
                outDistance = hit.distance;
                return true;
            }
            return false;
        }
        
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




    
}
