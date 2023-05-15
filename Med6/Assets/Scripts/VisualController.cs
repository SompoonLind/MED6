using UnityEngine;

public class VisualController : MonoBehaviour
{
    public bool writeData = false;
    public bool particles = false;
    public bool primitives = false;
    public bool followAlong = false;
    //Camera mainCamera;
    GameObject player;
    GameObject writerController;
    GameObject particleController;
    GameObject primitivesController;
    GameObject followController;

    void Start()
    {
        writerController = GameObject.FindGameObjectWithTag("Player");
        CSVWriter setWriteTrue = writerController.GetComponent<CSVWriter>();

        particleController = GameObject.Find("Particle System");
        primitivesController = GameObject.Find("point visualizer");
        followController = GameObject.Find("Camera Movement");
        
        player = GameObject.Find("Player");
        //mainCamera =  Camera.main;
        //mainCamera.enabled = false;

        if (writeData == true)
        {
            particleController.SetActive(false);
            primitivesController.SetActive(false);
            followController.SetActive(false);
        }

        else if (particles == true)
        {
            setWriteTrue.enabled = false;
            primitivesController.SetActive(false);
            followController.SetActive(false);
        }

        else if (primitives == true)
        {
            setWriteTrue.enabled = false;
            particleController.SetActive(false);
            followController.SetActive(false);
        }

        else if (followAlong == true)
        {
            setWriteTrue.enabled = false;
            particleController.SetActive(false);
            primitivesController.SetActive(false);
            player.SetActive(false);
        }
    }
}
