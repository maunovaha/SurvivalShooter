using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private float smoothing = 5.0f;

        [SerializeField]
        private Transform target;

        private Vector3 offset;

        private void Start()
        {
            offset = transform.position - target.position;
        }

        private void FixedUpdate()
        {
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}