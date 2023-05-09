﻿using UnityEngine;

namespace Data
{
    public class CameraData : MonoBehaviour
    {
        [SerializeField] private Vector3 _angle;
        [SerializeField] private float _distance;
        public float Distance => _distance;
        public Vector3 Angle => _angle;
        public Transform Transform => transform;
    }
}