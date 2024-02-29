using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    AttackDamage attackDamage;
    float duration;
    CreatureController tetheredCreature;
    float attackTickTimeTotal = 0.1f;
    float attackTickTimer;
    bool isProjectile;
    private OverworldManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        attackTickTimer = attackTickTimeTotal;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isProjectile)
        {
            attackTickTimer += 1 * Time.deltaTime;
            if (attackTickTimer >= attackTickTimeTotal)
            {
                CheckForHits(); // check for hits on all aoe once every 0.1 of a second
            }
        }
        else
        {
            CheckForHits(); // projectiles must check for hits every frame
        }
        
    }

    public void SetAttackVariables(AttackDamage damageToUse, float durationOfAttack, bool isAttackProjectile, OverworldManager gameManagerToUse)
    {
        gameManager = gameManagerToUse;
        attackDamage = damageToUse;
        duration = durationOfAttack;
        StartCoroutine(LastForSeconds(duration)); // will auto destroy projectile after x seconds
        isProjectile = isAttackProjectile;
        if (isProjectile)
        {
            GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous; // set to continuous for better collision detection for projectiles
        }
    }



    public void SetTetheredCreature(CreatureController creatureToTetherTo)
    {
        tetheredCreature = creatureToTetherTo;
    }

    private void CheckForHits()
    {
        if (isProjectile)
        {
            // projectile hits are dealt with using rays
        }
        else
        {
            // hits are worked out using polygon collider
        }


        // check all creatures hit in this attack
        List<CreatureController> hitCreatures = new();

        for (int i = 0; i < hitCreatures.Count; i++)
        {
            // may need to add in a check here regarding friendly fire
            gameManager.TakeCreatureDamage(attackDamage, hitCreatures[i]);
        }
    }

    private IEnumerator LastForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
