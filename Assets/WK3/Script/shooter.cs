using UnityEngine;

public class shooter : MonoBehaviour
{
    public GameObject powercell; //link to the powerCell prefab
    public int no_cell = 1; //number of powerCell owned
    public AudioClip throwSound; //throw sound
    public float throwSpeed= 20;//throw speed 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () 
    {
        //if left control (fire1) pressed, and we still have at least 1 cell 
        if (Input.GetButtonDown ("Fire1") && no_cell > 0) { 

            no_cell --; //reduce the cell
            Debug.Log("Cells Remaining =" + no_cell);

            //play throw sound
            AudioSource.PlayClipAtPoint(throwSound, transform.position); 

            //instantaite the power cel as game object
            GameObject cell = Instantiate(powercell, transform.position, 
                                          transform.rotation) as GameObject;

            //ask physics engine to ignore collison between 
            //power cell and our FPSControler
            Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), 
                                    cell.GetComponent<Collider>(), true);

            //give the powerCell a velocity so that it moves forward
           cell.GetComponent<Rigidbody>().linearVelocity = transform.forward * throwSpeed; 
        }
    }

    //to be used in pickup
    //add a cell when a cell is picked up
    public void AddCell()
    {
        no_cell ++;
        Debug.Log("Cell Count =" + no_cell);
    }

    // function to send to the HUD controller to tell it how many power cells user has
    public int CellCount()
    {
        return no_cell;
    }
}
