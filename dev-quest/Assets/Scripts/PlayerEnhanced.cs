using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnhanced : MonoBehaviour {

    // Estados
    bool isAlive = true;

    public Rigidbody2D playerBody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider2D;
    BoxCollider2D playerFeetCollider;
    float gravityScaleAtStart;


    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    // Use this for initialization
    void Start () {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider2D = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerBody.gravityScale;
    }
	
	// Update is called once per frame
	void Update () {

        if (!isAlive) { return; }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        print(y);
        print(x);
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        Jump();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    private void Walk(Vector2 dir) {
        playerBody.velocity = (new Vector2(dir.x * speed, playerBody.velocity.y));
        bool playerHasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump() {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }
        if (Input.GetButtonDown("Jump")) {
            playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
            playerBody.velocity += Vector2.up * jumpForce;
        }
    }


    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            // Se o jogador estiver movendo na horizontal
            transform.localScale = new Vector2(Mathf.Sign(playerBody.velocity.x), 1f);
        }

    }

    private void ClimbLadder() {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            playerAnimator.SetBool("Climbing", false);
            playerBody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(playerBody.velocity.x, controlThrow * climbSpeed);
        playerBody.velocity = climbVelocity;
        playerBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void Die() {
        if (playerBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
