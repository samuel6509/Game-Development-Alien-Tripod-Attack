// the script for the boss fight
// FSM going to be used, going to be used to make boss change power & speed between phases
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    Transform playerPosition; // Players position
    Transform companionPosition; // companions position
    NavMeshAgent nav; // bosses nav mesh agent component
    Animator animator; // animator componenet of the boss
    private triPodHealth health; // health script for boss
    public GameObject[] laserEyes; // object array for bosses laser eyes
    private float phase1Speed = 1f; // how fast boss is in the first phase
    private float phase2Speed = 1.5f; // how fast boss will be in seconds phase 
    private float finalStandSpeed = 2f; // speed of boss in final stand phase

    private float encounterDistance = 10f; // how close the player has to be to begin boss encounter

    public enum states
    {
        // add states / phases here
        idle,
        phase1,
        phase2,
        finalStand
    }
    states _state = states.idle; // begin in the idle state

    // once scene is loaded everything here is ran
    void Awake()
    {
        // transform position of the player
        playerPosition = GameObject.FindGameObjectWithTag ("Player").transform;
        // transform pos of companion
        companionPosition = GameObject.FindGameObjectWithTag ("Companion").transform;
        // the nav component attatched to boss 
        nav = GetComponent<NavMeshAgent>();
        // animator component
        animator = GetComponent<Animator>();
        // health script
        health = GetComponent<triPodHealth>();
        // initial speed of boss
        nav.speed = phase1Speed; 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // distance between player & boss
        float distanceFromPlayer = Vector3.Distance(transform.position, playerPosition.position);
        // different states the boss change change into 
        switch(_state)
        {
            case states.idle:
            IdleState(distanceFromPlayer);
            break;

            case states.phase1:
            Phase1();
            break;

            case states.phase2:
            Phase2();
            break;

            case states.finalStand:
            FinalStand();
            break;
        }
        // method for changing animation dependent on the current state
        ChangeAnimation();
    }

    // method for the idle state 
    // only in this state when the boss is not encountered in the scene 
    void IdleState(float distanceFromPlayer)
    {
        // if boss is dead stay in this state
        if(health.CurrentHealth() <= 0)
        {
            nav.isStopped = true; 
            DeactivateLaserEyes();
        }
        // if not then continue with the encounter
        else
        {
            // no eyes activated
            ActivateLaserEyes(0);
            // make sure boss cannot prematurely move
            nav.isStopped = true;
            // looks at player
            Quaternion towardTarget = Quaternion.LookRotation(playerPosition.position - transform.position);
            // Slerp makes the transition smooth between where the companion was looking and where it is looking now
            transform.rotation = Quaternion.Slerp(transform.rotation, towardTarget, Time.deltaTime * 2f);

            // if player is close enough to begin the encounter
            if(distanceFromPlayer <= encounterDistance)
            {
                // begin phase 1
                _state = states.phase1;
            }
        }
    }

    // method for the first phase of the boss fight
    // boss is in this phase once encountered in the scene
    // CANNOT go back to idle state once begins
    void Phase1()
    {
        // first eye activated
        ActivateLaserEyes(1);
        // can now walk 
        nav.isStopped = false; 
        // bosses walking speed 
        nav.speed = phase1Speed;
        // walks to players current position which is updated every frame
        nav.SetDestination(playerPosition.position); 

        // if health is half or less then half go into phase 2 
        if(health.CurrentHealth() <= health.maxHealth * 0.5f)
        {
            _state = states.phase2;
        }
    }

    // method for the seconds phase of the boss fight
    // when boss is on 30% health go into this state
    // maybe smaller & now flys & is faster with different firing & weapoons 
    void Phase2()
    {
        // 2 laser eyes now activated
        ActivateLaserEyes(2);
        // boss is now faster & still locked onto player
        nav.speed = phase2Speed;
        nav.SetDestination(playerPosition.position);
        // when boss is 25% health or lower go into final stand 
        if(health.CurrentHealth() <= health.maxHealth * 0.25f)
        {
            _state = states.finalStand;
        }
    }

    // method for the final phase of the boss
    // wipe encounter
    // must destroy the boss before it wipes everything out
    void FinalStand()
    {
        // 3 laser eyes now activated
        ActivateLaserEyes(3);
        // boss is now even faster & is still locked onto player
        nav.speed = finalStandSpeed;
        nav.SetDestination(playerPosition.position);
        // when boss is dead go back to idle so it doesnt kill player after game end
        if(health.CurrentHealth() <= 0)
        {
            nav.isStopped = true; 
            DeactivateLaserEyes();
            _state = states.idle;
        }
    }

    // method for activating the eyes
    // phase 1 = 1 eye
    // phase 2 = 2 eyes
    // final stand = 3 eyes
    void ActivateLaserEyes(int activeEyeCount)
    {
        // loop to determine how many eyes are active dependent on boss phase / state
        for(int i = 0; i < laserEyes.Length; i++)
        {
            laserEyes[i].SetActive(i < activeEyeCount);
        }
    }

    // method for deactivating the eyes once the player & companion have defeated boss
    void DeactivateLaserEyes()
    {
        foreach (GameObject eye in laserEyes)
        {
            eye.SetActive(false);
        }
    }

    // method for the boss to change it's animation depending on which state it is in
    void ChangeAnimation()
    {
        // idle animation
        if(_state == states.idle)
        {
            animator.SetFloat("speed", 0);
        }
        // walking animation
        else
        {
            animator.SetFloat("speed", 1);
        }
    }
}
