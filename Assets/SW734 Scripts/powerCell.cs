using UnityEngine;

public class powerCell : MonoBehaviour
{
    public GameObject explode;
    private GameObject tripod;
    private GameObject companion; // companionAI
    float removeTime = 2; //3 seconds until explosion
    public float explosionRadius = 5; // area affected by explosion
    public float explosionForce = 1000; // how far things go when in the radius

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Use this for initialization
    void Start () 
    {    
        tripod = GameObject.Find ("tripod");//find the tripod 
        companion = GameObject.Find("AI Companion"); // find companion 
        Destroy(gameObject, removeTime); //destory the object after 3s
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) //When powercell collides with something
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            //instantiate the explosion 
            Instantiate(explode, transform.position, transform.rotation);

            //reduce the tripod's health
            tripod.GetComponent<triPodHealth>().reduceHealth();
            Destroy(gameObject);//destory self 
        } 

        else if(other.gameObject.tag == "Companion")
        {
            //instantiate the explosion 
            Instantiate(explode, transform.position, transform.rotation);

            //reduce the companions health
            companion.GetComponent<HealthAI>().DecreaseHealth();
            Destroy(gameObject);//destory self 
        }
    }

    void OnDestroy() //powercells that miss explode after 3 seconds
    {
        Instantiate(explode, transform.position, transform.rotation);
        Explosion(); // call my explode method
    }

    //finds all colliders in the powercells radius parameters 
    // if has rigidbody make things be effected by physics 
    void Explosion()
    {
        Collider[] effected = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in effected)
        {
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // ensures the objects are physics affected
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}
