using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headbob : MonoBehaviour
{


    [SerializeField] private bool enable = true;
    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.001f; 
    [SerializeField, Range(0, 30)] private float frequency = 10.0f;
    [SerializeField] private Transform camera = null; 
    [SerializeField] private Transform cameraHolder = null;
    private float toggleSpeed = 3.0f; 
    private Vector3 startPos; 
    private CharacterController controller;

    // Start is called before the first frame update
    private void Awake() {
        controller = GetComponent<CharacterController>();
        startPos = camera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!enable) return;

        CheckMotion();
        ResetPosition();
        camera.LookAt(FocusTarget());
    }

    private void CheckMotion(){
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        if (speed < toggleSpeed) return; 
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    private Vector3 FootStepMotion(){
        Vector3 pos = Vector3.zero; 
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude; 
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2; 
        return pos;
    }

    private void ResetPosition(){
        if(camera.localPosition == startPos) return;

        camera.localPosition = Vector3.Lerp(camera.localPosition, startPos, 1 * Time.deltaTime);
    }

    private void PlayMotion(Vector3 motion){
        camera.localPosition += motion;
    }

    private Vector3 FocusTarget(){
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        return pos;
    }
}
