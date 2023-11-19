using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerStateMachine
{
    [CreateAssetMenu(fileName = "PlayerNumbers", menuName = "ScriptableObjects/PlayerNumbers", order = 1)]
    public class PlayerNumbers : ScriptableObject
    {
        public bool Debug = false;
        [Header("Standing")]
        public Vector2 StandingHitboxDown;
        public Vector2 StandingHitboxDownOffset;
        public Vector2 StandingHitboxUp;
        public Vector2 StandingHitboxUpOffset;
        public Vector2 StandingHitboxFront;
        public Vector2 StandingHitboxFrontOffset;
        public Vector2 StandingHitboxBack;
        public Vector2 StandingHitboxBackOffset;
        [Header("Crouching")]
        public Vector2 CrouchingHitboxDown;
        public Vector2 CrouchingHitboxUp;
        public Vector2 CrouchingHitboxFront;
        public Vector2 CrouchingHitboxBack;
        [Header("Running")]
        public float WalkAcceleration = 10f;
        public float WalkFriction = 10f;
        public float TurnInstantSpeedFactor = -0.8f; //mutliply x speed when turning
        public float StepCheck = 3f; //per second
        public float StandThreshhold = 0.1f;
        public float RunCamProvidence = 0.5f;
        [Header("Jumping and Airborne"), Tooltip("Total Jumpheight. Inital Yeet is calculated off this")]
        public float JumpHeight = 3.25f;
        public float JumpImpulse() {
            return Mathf.Sqrt(2 * JumpHeight * DefaultGravity);
        }
        public Vector2 WallJumpImpulse = new (10,10);
        public float DefaultGravity = 10f;
        public float MaxFallSpeed = 10f;
        public float AirborneAcceleration = 10f;
        public float AirborneAccelerationThreshhold = 0.1f;
        [Header("Spinning")]
        public Vector2 SpinHitboxDown;
        public Vector2 SpinHitboxDownOffset;
        public Vector2 SpinHitboxUp;
        public Vector2 SpinHitboxUpOffset;
        public Vector2 SpinHitboxFront;
        public Vector2 SpinHitboxFrontOffset;
        public Vector2 SpinHitboxBack;
        public Vector2 SpinHitboxBackOffset;
        public float SpinCamProvidence = 0.5f;
        public Vector2 WallJumpBackCheck; //this is just a spot for a sphercast
        [Header("Walljump")]
        public float WallJumpTime = 0.2f;
        public float WallClingBack = 0.1f;
        public Vector2 WalljumpImpulseInward;
        public Vector2 WalljumpImpulseNeutral;
        public Vector2 WalljumpImpulseOutward;
        
    }
}
