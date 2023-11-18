using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using PlayerStateMachine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    public Vector2 vel = new(0, 0);
    public PlayerStateMachine.PlayerNumbers numbers;
    public bool facingLeft = false;
    public LayerMask groundLayer;
    public LayerMask semiSolid;
    public PlayerState state;
    public Animator anim;
    private new SpriteRenderer renderer;
    [SerializeField]
    public GameplayIAA Inputs;

    private void Awake() {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        Inputs = new GameplayIAA();
        Inputs.Enable();
        state = new Standing(this);
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        state.Update(this);
        if (vel.x != 0)
        {
            facingLeft = vel.x < 0;
        }
    }
    private void OnDrawGizmos() {
        //only do in PlayMode
        if (Application.isPlaying) {

            Vector3[] diamondPoints = new Vector3[4]{
            transform.position + new Vector3(0,-state.HitboxDown()),
            transform.position + new Vector3(state.HitboxBack() * -FrontVec().x,0),
            transform.position + new Vector3(0,state.HitboxUp()),
            transform.position + new Vector3(state.HitboxFront() * FrontVec().x,0)
        };
            Gizmos.color = Color.yellow;
            Gizmos.DrawLineStrip(diamondPoints, true);
            Gizmos.color = Color.red;
            //Draw a cube based on velocity
            float lengthScale = 0.25f;
            float thicknessScale = 1f/16f;
            int lineResolution = 40;
            for (int i = 0; i <= lineResolution; i++) {
                Gizmos.DrawWireSphere(transform.position + new Vector3(vel.x,vel.y) * i / lineResolution * lengthScale, thicknessScale);
            }
            //Gizmos.DrawLine(transform.position, transform.position + new Vector3(vel.x * scale, vel.y * scale));
        }
    }

    public Vector2 FrontVec() {
        if (facingLeft) {
            return new Vector2(-1, 0);
        } else {
            return new Vector2(1, 0);
        }
    }
}
