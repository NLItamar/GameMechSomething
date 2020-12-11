using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValuesScript : MonoBehaviour
{
    public float enemyNormalParticlesDistance;
    public float enemyJumpyParticlesDistance;

    public float creepNormalDistance;
    public float creepJumpyDistance;    

    public float followNormalDistance;
    public float followJumpyDistance;

    public float speedJumpyFollow;
    public float speedNormalFollow;

    public float speedNormalPatrol;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("values started");

        //view the values when the game starts
        //IN CONSOLE
        Debug.Log(
            "enemyNormalParticlesDistance: " +
            enemyNormalParticlesDistance + " " + 

            "enemyJumpyParticlesDistance:  " +
            enemyJumpyParticlesDistance + " " +

            "creepNormalDistance:  " +
            creepNormalDistance + " " +

            "creepJumpyDistance:  " +
            creepJumpyDistance + " " +

            "followNormalDistance:  " +
            followNormalDistance + " " +

            "followJumpyDistance:  " +
            followJumpyDistance
            );
    }
}
