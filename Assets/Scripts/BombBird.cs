using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBird : Bird
{
    public float explosionForce = 10f;
    public float explosionDistance = 5f;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (State != BirdState.HitSomething && collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Overridden method called");
            Vector2 explosionPosition = gameObject.transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionDistance);
            foreach (Collider2D objectHit in colliders)
            {
                Rigidbody2D rb = objectHit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = (Vector2)objectHit.gameObject.transform.position - explosionPosition;
                    float explosionMagnitude = explosionForce / direction.magnitude;
                    rb.AddForce(direction, ForceMode2D.Impulse);
                }
            }
            this.State = BirdState.HitSomething;
        }
        
    }
}
