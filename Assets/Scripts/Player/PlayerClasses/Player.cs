using UnityEngine;

public class Player {
    private float jumpForce;
    private float moveForce;

    public bool isGrounded;
    public bool isJumping;

    public Player (float jumpForce, float moveForce) {
        this.jumpForce = jumpForce;
        this.moveForce = moveForce;

        isGrounded = false;
        isJumping = false;
    }

    public float move () {
        return moveForce;
    }

    public float jump () {
        return jumpForce;
    }

    public virtual void setIsGrounded (bool isGrounded) {
        this.isGrounded = isGrounded;
    }

    public virtual void setIsJumping (bool isJumping) {
        this.isJumping = isJumping;
    }

    public virtual bool canJump () {
        return isGrounded;
    }
}