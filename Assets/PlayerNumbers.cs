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
        public float StandingHitboxDown = 0.5f;
        public float StandingHitboxUp = 0.5f;
        public float StandingHitboxFront = 0.25f;
        public float StandingHitboxBack = 0.25f;
        [Header("Crouching")]
        public float CrouchingHitboxDown = 0.25f;
        public float CrouchingHitboxUp = 0.25f;
        public float CrouchingHitboxFront = 0.25f;
        public float CrouchingHitboxBack = 0.25f;
        [Header("Running")]
        public float WalkAcceleration = 10f;
        public float WalkingDeacceleration = 10f;
        public float TurnInstantSpeedFactor = -0.8f; //mutliply x speed when turning
        public float StepCheck = 3f; //per second
        public float StandThreshhold = 0.1f;
        [Header("Jumping and Airborne"), Tooltip("Inital YEET upon pressing Jump")]
        public float JumpImpulse = 10;
        public Vector2 WallJumpImpulse = new (10,10);
        public float DefaultGravity = 10f;
        public float MaxFallSpeed = 10f;
        public float AirborneAcceleration = 10f;
        [Header("Spinning")]
        public float SpinHitboxDown = 0.35f;
        public float SpinHitboxUp = 0.35f;
        public float SpinHitboxFront = 0.25f;
        public float SpinHitboxBack = 0.25f;
        public float WallJumpBackCheck = 0.5f;
    }
}
