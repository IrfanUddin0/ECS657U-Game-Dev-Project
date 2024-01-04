using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragonState
{ 
    flying,
    grounded,
    dead
}
public class DragonScript : MonoBehaviour
{
    public float speed;
    public List<Transform> flyingPoints;
    public GameObject groundedPoint;
   
    private PlayerSurvival player;
    private DragonState state;
    private int currentFlyingPointIndex = 0;
    void Start()
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
                break;
            case DragonState.dead:
                return;
        }
    }

    private void flyToNext()
    {
        // get to next, if distance nearly== 0, go next
        transform.rotation = Quaternion.LookRotation(flyingPoints[currentFlyingPointIndex].position - transform.position);
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

        if(Vector3.SqrMagnitude(transform.position - flyingPoints[currentFlyingPointIndex].position) <= 0.1f)
        {
            incrementFlyingPointIndex();
        }
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

        switch(mode)
        {
            case DragonState.flying:break;
            case DragonState.grounded:break;
            case DragonState.dead:break;
        }

        state = mode;
    }
}
