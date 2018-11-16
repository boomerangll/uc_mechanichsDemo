using System.Collections;
using UnityEngine;

public class PlayerStr : Player {
    private float timeBetweenJumps = 2f;
    private bool jumpReady = true;
    private bool playerJustJumped = false;

    public PlayerStr () : base(400f, 25f) { }

    public override void setIsJumping (bool isJumping) {
        base.setIsJumping(isJumping);
        playerJustJumped = true;
    }

    public override void setIsGrounded (bool isGrounded) {
        base.setIsGrounded(isGrounded);

        if (isGrounded && playerJustJumped) {
            playerJustJumped = false;
            jumpReady = false;

            //MonoBehaviourInstance.getMonoBehaviourInstance();
        }
    }

    public override bool canJump () {
        return base.canJump() && jumpReady;
    }

    private IEnumerator startJumpCountdime () {
        float currentTime = 0f;
        while (currentTime <= timeBetweenJumps) {
            yield return new WaitForSeconds(1.0f);
            timeBetweenJumps++;
        }

        jumpReady = true;
    }
}