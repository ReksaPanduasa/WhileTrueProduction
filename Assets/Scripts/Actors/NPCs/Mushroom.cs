using Unity.Mathematics;
using UnityEngine;

public class Mushroom : Enemy {
    void FixedUpdate()
    {
        if(healthManager.CurrentHealthPoint < 1) {
            Die();
            isAttacking = false;
            animator.SetBool("isAttacked", false);
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
            horizontal = 1.5f;
            FlipRight();
        } else if (isMovingLeft) {
            horizontal = -1.5f;
            FlipLeft();
        } else {
            horizontal = 0f;
        }

        if(animator.GetBool("isAttacking")) horizontal = 0;

        Move(horizontal);
        
        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
    }
}