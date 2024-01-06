using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragonState
{ 
    flying,
    grounded,
    dead
}
public class DragonScript : PlayerHittable
{
    public float speed;
    public List<Transform> flyingPoints;
    public ArenaTriggerScript arenaTrigger;

    public GameObject healthBar;
    public GameObject dragonProjectile;
    public GameObject deathEffect;

    public AudioClip flyingSound;
    public AudioClip attackSound;
    public AudioClip deadSound;
   
    private PlayerSurvival player;
    private DragonState state;
    private int currentFlyingPointIndex = 0;
    public override void Start()
    {
        player = FindAnyObjectByType<PlayerSurvival>();

        // set mode to flying
        setMode(DragonState.grounded);
        setMode(DragonState.flying);
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case DragonState.flying:
                flyToNext();
                break;
            case DragonState.grounded:
                flyToPlayer();
                break;
            case DragonState.dead:
                return;
        }
    }

    public override void OnPlayerHit(float dmg)
    {
        base.OnPlayerHit(dmg);
        healthBar.transform.localScale = new Vector3((health / maxHealth) * 4f, 0.2f, 0.02f);
        if(health<=0)
        {
            Util.PlayClipAtPoint(deadSound, transform.position, 100f);
            setMode(DragonState.dead);
            FindAnyObjectByType<PlayerObjectives>().addDataEntry("DragonKilled", "true");
            var effect = Instantiate(deathEffect, transform.position, transform.rotation);
            effect.transform.localScale = new Vector3(5f, 5f, 5f);
            Destroy(gameObject);
        }
            
    }

    private void flyToPlayer()
    {
        var nextLookRot = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, nextLookRot, Time.deltaTime * 3f);
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

        if (Vector3.SqrMagnitude(transform.position - player.transform.position) <= 64f)
        {
            setMode(DragonState.flying);
            attackPlayer();
            for (int i = 0; i < (flyingPoints.Count/2); i++)
            {
                incrementFlyingPointIndex();
            }
        }
    }

    private void flyToNext()
    {
        // get to next, if distance nearly== 0, go next
        var nextLookRot = Quaternion.LookRotation(flyingPoints[currentFlyingPointIndex].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, nextLookRot, Time.deltaTime * 3f);
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

        if(Vector3.SqrMagnitude(transform.position - flyingPoints[currentFlyingPointIndex].position) <= 0.1f)
        {
            Util.PlayClipAtPoint(flyingSound, transform.position, 100f);
            Util.PlayClipAtPoint(attackSound, transform.position, 100f);
            attackPlayer();
            if (arenaTrigger.PlayerInArena() && Util.RngDifficultyScaled(0.1f))
                setMode(DragonState.grounded);
            else
                incrementFlyingPointIndex();
        }
    }

    private void attackPlayer()
    {
        if (arenaTrigger.PlayerInArena())
            Instantiate(dragonProjectile, transform.position, Quaternion.LookRotation(player.transform.position- transform.position));
    }

    private void incrementFlyingPointIndex()
    {
        currentFlyingPointIndex++;
        if(currentFlyingPointIndex==flyingPoints.Count)
            currentFlyingPointIndex=0;
    }

    private void setMode(DragonState mode)
    {
        if (state == mode)
            return;

        state = mode;
    }
}
