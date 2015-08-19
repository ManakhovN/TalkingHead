using UnityEngine;
using System.Collections;

public class ButtonsController : MonoBehaviour {
    public GameObject cube;
    public GameObject head;
    public Animator anim;
    public FocusPointMover fpm;
    public eyeBlinking eb;
    void Start()
    {
        turnOnCube();    
    }
    public void turnOnCube()
    {
        cube.SetActive(true);
        head.SetActive(false);
        anim.enabled = false;
        fpm.enabled = false;
        eb.enabled = false;
    }

    public void turnOnHead()
    {
        cube.SetActive(false);
        head.SetActive(true);
        anim.enabled = false;
        fpm.enabled = false;
        eb.enabled = false;
    }

    public void turnOnAnimatedHead()
    {
        cube.SetActive(false);
        head.SetActive(true);
        anim.enabled = true;
        fpm.enabled = true;
        eb.enabled = true;
    }
}
