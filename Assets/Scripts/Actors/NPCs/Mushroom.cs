using Unity.Mathematics;
using UnityEngine;

public class Mushroom : Enemy {
    void FixedUpdate()
    {
        if(HealthPoint < 1) {
            Die();
            isAttacking = false;
        }
        Reset();
        
        GetPlayer();

        if(adventurer != null) {
            float playerDistance = adventurer.HorizontalDistanceFrom(transform.position);
            if(playerDistance < -2f) {
                isMovingRight = true;
                isMovingLeft = false;
            } else if(playerDistance > 2f){
                isMovingRight = false;
                isMovingLeft = true;
            }

            if(playerDistance < 0) {
                FlipRight();
            } else if(playerDistance > 0) {
                FlipLeft();
            }

            if(Time.time >= nextAttack && !animator.GetBool("isDying") && math.abs(playerDistance) < 3f) {
                nextAttack = Time.time + attackCD;
                isAttacking = true;
            }
            
            DetachPlayer(null);
        }

        if(isAttacking) {
            Attack();
            isAttacking = false;
        }

        if (isMovingRight) {
            horizontal = 2f;
            FlipRight();
        } else if (isMovingLeft) {
            horizontal = -2f;
            FlipLeft();
        } else {
            horizontal = 0f;
        }

        if(animator.GetBool("isAttacking")) horizontal = 0;

        Move(horizontal);
        
        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
    }

    private void Reset() {
        isMovingLeft = false;
        isMovingRight = false;
        // isAttacking = false;
        isAttacked = false;
        isJumping = false;
        horizontal = 0;
    }
}