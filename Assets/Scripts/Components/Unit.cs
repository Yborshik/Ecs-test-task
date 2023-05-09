using UnityEngine;

namespace Components
{
    public struct Unit
    {
        public Transform Transform;
        public Vector3 Direction;
        public Vector3 Position;
        public Quaternion Rotation;
        public float MoveSpeed;
        public float RotateSpeed;
    }
}