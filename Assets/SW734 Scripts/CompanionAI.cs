using UnityEngine;
using UnityEngine.AI;

// TODO:
// change targeting to the boss rather than player 

public class CompanionAI : MonoBehaviour
{
    Transform playerPosition; // Transform of the players position, used by FSM 
    NavMeshAgent nav; // the nav mesh in scene, used to see baked navigation
    Animator animator; // the animator attatched to companion
    Rigidbody rb; // companions rigidbody

    public float walkSpeed = 1f; // speed of walking state
    public float walkDistance = 3f; // distance between player & companion before walking 

    public float runSpeed = 4f; // speed of running state
    public float runDistance = 10f; // distance for running state to be used

    public float surviveSpeed = 15f; // speed of companion when on low health
    public float surviveDistance = 30f; // distance the companion must have between them & the boss to stop being in survive state

    private TargetingAI targeting; // script that holds all the logic for targeting & shooting
    private HealthAI health; // script that holds all logic for companions health
    private PlayerHealth playerHealth; // scirpt for player health

    // the states for my FSM to switch through
    public enum states
    {
        // add more when they are made
        idle,
        walk,
        run,
        survive,
        getDownMrPresident
    }
    states _state = states.idle; // the idle state if the default state to start on

    // ran when the script instance is loaded
    void Awake()
    {
        // transform position of the player, this case the FirstPerson AIO
        playerPosition = GameObject.FindGameObjectWithTag ("Player").transform;
        // the nav component in scene 
        nav = GetComponent<NavMeshAgent>();
        // get the animator component from this companion
        animator = GetComponent<Animator>();
        // get the companions rigidbody
        rb = GetComponent<Rigidbody>();
        // default speed of the companion
        nav.speed = walkSpeed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // script references
        targeting = GetComponent<TargetingAI>(); 
        health = GetComponent<HealthAI>();
        playerHealth = GameObject.Find("FirstPerson-AIO").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // distance between player & companion
        float distanceFromPlayer = Vector3.Distance(transform.position, playerPosition.position);

        switch(_state)
        {
            case states.idle:
            idleState(distanceFromPlayer);
            break;

            case states.walk:
            walkState(distanceFromPlayer);
            break;

            case states.run:
            runState(distanceFromPlayer);
            break;

            case states.survive:
            surviveState(distanceFromPlayer);
            break;

            case states.getDownMrPresident:
            bulletShield();
            break;
        }
        // method to change animation based on movement 
        updateAnimation();
    }

    // logic for the idle state of Finite State Machine
    // companion CAN shoot in this state
    void idleState(float distanceFromPlayer)
    {
        nav.speed = 0f; // make sure the companion cannot walk 
        // method call to check is state needs to change to survive
        survival();
        PlayerSurvival();
        targeting.targetEnemy();

        // looks at player
        Quaternion towardTarget = Quaternion.LookRotation(playerPosition.position - transform.position);
        // Slerp makes the transition smooth between where the companion was looking and where it is looking now
        transform.rotation = Quaternion.Slerp(transform.rotation, towardTarget, Time.deltaTime * 2f);

        // if theres enough distance between player & companion
        if(distanceFromPlayer >= walkDistance)
        {
            // change state to walk
            _state = states.walk;
        }
    }

    // logic for the walk state of the FSM
    // companion CAN shoot in this state

    void walkState(float distanceFromPlayer)
    {
        // call to the survive condition method
        survival();
        PlayerSurvival();
        // if distance from player is short 
        if(distanceFromPlayer <= 2.5f)
        {
            // change to idle state
            _state = states.idle;
            nav.isStopped = true; // stop walking towards the player
        }
        // if distance from player is far enough to run
        if(distanceFromPlayer >= runDistance)
        {
            // change to run state
            _state = states.run;
        }
        // must still be in the walk state
        else
        {
            nav.isStopped = false; // still walking
            nav.SetDestination(playerPosition.position); // companions destination
            nav.speed = walkSpeed; // changing speed of companion
            targeting.targetEnemy();
        }
    }

    // logic for the run state for companions FSM
    // companion CANNOT shoot in this state
    void runState(float distanceFromPlayer)
    {
        // method call to survive 
        survival();
        PlayerSurvival();
        // if distance from player is in walk conditions
        if(distanceFromPlayer <= walkDistance)
        {
            _state = states.walk; // change to walk state
            nav.speed = walkSpeed; // make sure the speed is set to walking
        }
        else
        {
            nav.isStopped = false; // makes sure companion is still running
            nav.speed = runSpeed; // speed set to running
            nav.SetDestination(playerPosition.position);
        }
    }

    // logic for the FSM state survive
    // companion CAN shoot in this state
    // maybe they get some extra bullets for final stand?
    void surviveState(float distanceFromPlayer)
    {
        PlayerSurvival();
        if(health.CurrentHealth() >= 2)
        {
            _state = states.idle;
        }
        if(distanceFromPlayer >= surviveDistance)
        {
            nav.isStopped = true;
        }
        else
        {
            nav.isStopped = false;
            // change this to companion rotates away from BOSS when implemented
            // looks away from the target
            Quaternion awayFromTarget = Quaternion.LookRotation(transform.position - playerPosition.position);
            // Slerp makes the transition smooth between where companion was looking & where it is looking now
            transform.rotation = Quaternion.Slerp(transform.rotation, awayFromTarget, Time.deltaTime * 2f);

            // get to a safe spot
            Vector3 escape = transform.position + transform.forward * 1.5f;
            // set destination to escape coordanites
            nav.SetDestination(escape);
            // change speed
            nav.speed = surviveSpeed;

            // method call
            health.StartPassiveHealing();

            // method call
            //targeting.targetEnemy();
        }
    }

    // method for the MR President state
    // when player is low health run in front of the player & attempt to take the damage
    void bulletShield()
    {
        if(playerHealth.CurrentHealth() >= playerHealth.playerMaxHealth)
        {
            _state = states.idle;
        }
        else
        {
            playerHealth.StartPassiveHealing();
            nav.speed = surviveSpeed;
            nav.SetDestination(playerPosition.position);
        }
    }

    // method to reuse something to check players health
    // for state changing
    void PlayerSurvival()
    {
        // if player is close to dying
        if(playerHealth.CurrentHealth() <=1)
        {
            // change companion into bulletShield state
            _state = states.getDownMrPresident;
            return;
        }
    }

    // method to encapsulate a re used component of the FSM
    void survival()
    {
        // if companions health is low
        if(health.CurrentHealth() <= 1f)
        {
            // change state to survive
            _state = states.survive;
            return; // exit method early 
        }
    }

    // method for updating the parameters for deciding on current animation
    // change animation depending on what state is chosen, animations in a blend tree
    void updateAnimation()
    {
        if (_state == states.walk)
        {
            animator.SetFloat("Direction", 1);  // Walking
        }
        else if (_state == states.run || _state == states.survive || _state == states.getDownMrPresident)
        {
            animator.SetFloat("Direction", 2);  // Running
        }
        // ADD OTHER STATES HERE
        else
        {
            animator.SetFloat("Direction", 0);  // Idle
        }
    }
}