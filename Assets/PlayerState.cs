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
            //OnEnter();
        }
        public PlayerController player;
        public float StateAge { get; protected set; }

        public void Update(PlayerController player) {
            this.StateAge += Time.deltaTime;
            Motion();
            StateSwap();
            if (player.state != this) {
                player.state.OnExit();
                player.state.OnEnter();
            }
            player.state.Collision();
        }

        public abstract void Motion();
        public void StateSwap() {
            
            
        }
        public void Collision() {

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
