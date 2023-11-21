using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using PlayerStateMachine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Vector2 vel = new(0, 0);
    public PlayerStateMachine.PlayerNumbers numbers;
    public bool facingLeft = false;
    public LayerMask groundLayer;
    public LayerMask semiSolid;
    public float timeSinceJump = 0;
    public PlayerState state;
    public Animator anim;
    private new SpriteRenderer renderer;
    [SerializeField]
    public GameplayIAA Inputs;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        Inputs = new GameplayIAA();
        Inputs.Enable();
        state = new Standing(this);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        state.Update(this);
        renderer.flipX = facingLeft;
        timeSinceJump += Time.deltaTime;
    }
    private void OnDrawGizmos()
    {
        //only do in PlayMode
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            //Draw Cubes to represent hitboxes
            Gizmos.DrawWireCube(transform.position + new Vector3(0, -state.HitboxDown().x / 2) + new Vector3(state.HitboxDownOffset().x, state.HitboxDownOffset().y), new Vector3(state.HitboxDown().y, state.HitboxDown().x));
            Gizmos.DrawWireCube(transform.position + new Vector3(0, state.HitboxUp().x / 2) + new Vector3(state.HitboxUpOffset().x, state.HitboxUpOffset().y), new Vector3(state.HitboxUp().y, state.HitboxUp().x));
            Gizmos.DrawWireCube(transform.position + new Vector3(state.HitboxFront().x / 2 * FrontVec().x, 0) + new Vector3(state.HitboxFrontOffset().x, state.HitboxFrontOffset().y), new Vector3(state.HitboxFront().x, state.HitboxFront().y));
            Gizmos.DrawWireCube(transform.position + new Vector3(-state.HitboxBack().x / 2 * FrontVec().x, 0) + new Vector3(state.HitboxBackOffset().x, state.HitboxBackOffset().y), new Vector3(state.HitboxBack().x, state.HitboxBack().y));

            //Draw a cube based on velocity
            float lengthScale = 0.25f;
            float thicknessScale = 1f / 16f;
            int lineResolution = 40;
            for (int i = 0; i <= lineResolution; i++)
            {
                Gizmos.DrawWireSphere(transform.position + new Vector3(vel.x, vel.y) * i / lineResolution * lengthScale, thicknessScale);
            }
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(vel.x, vel.y));
        }
    }


    public Vector2 FrontVec()
    {
        if (facingLeft)
        {
            return new Vector2(-1, 0);
        }
        else
        {
            return new Vector2(1, 0);
        }
    }
}
