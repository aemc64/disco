using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public MeshRenderer A;
    public MeshRenderer B;
    public MeshRenderer X;
    public MeshRenderer Y;
    public Transform LeftJoyStick;
    public Transform RightJoyStick;
    public string player;

    private Vector3 startposL;
    private Vector3 startposR;

    // Use this for initialization
    void Start () {
        startposL = LeftJoyStick.position;
        startposR = RightJoyStick.position;

    }

    // Update is called once per frame
    void Update () {
        A.enabled = Input.GetButton("A_"+player);
        B.enabled = Input.GetButton("B_"+player);
        X.enabled = Input.GetButton("X_"+player);
        Y.enabled = Input.GetButton("Y_"+player);

        Vector3 inputDirecton = Vector3.zero;
        inputDirecton.x = Input.GetAxis("LHorizontal_"+player);
        inputDirecton.y = Input.GetAxis("LVertical_"+player);
        LeftJoyStick.position = startposL + inputDirecton;

        inputDirecton = Vector3.zero;
        inputDirecton.x = Input.GetAxis("RHorizontal_"+player);
        inputDirecton.y = Input.GetAxis("RVertical_"+player);
        RightJoyStick.position = startposR + inputDirecton;
    }
}
