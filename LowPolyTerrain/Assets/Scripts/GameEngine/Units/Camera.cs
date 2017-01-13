using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts.GameEngine.Units
{
    class Camera: MonoBehaviour
    {
        [SerializeField]
        private Player player;
        private Vector3 Angle;
        private Vector3 Offset;
        private float currentVelocity;

        private Player Player { get { return player; } }

        private void Start()
        {
            Offset = new Vector3(0, 0.05f, -0.15f);
            Angle = Vector3.zero;
        }

        private void Update()
        {
            transform.position = Player.transform.position+ Quaternion.Euler(Angle)* Offset;
            transform.eulerAngles = Angle;

            Offset.z += Input.GetAxis("Mouse ScrollWheel");
            if (Offset.z > -0.15f)
                Offset.z = -0.15f;

            if (Offset.z < -1.5f)
                Offset.z = -1.5f;

            Angle.y += Input.GetAxis("Mouse X")*2;
            Angle.x += Input.GetAxis("Mouse Y")*2;
            if (Angle.x > 90)
                Angle.x = 90;

            if (Angle.x <- 90)
                Angle.x = -90;

            if (Input.GetAxis("Fire2") == 0)
            {
                Angle.y += Input.GetAxis("Rotate")*2;

                Vector3 euler = Player.transform.eulerAngles;
                Player.transform.eulerAngles = new Vector3(euler.x, Mathf.SmoothDampAngle(euler.y, transform.eulerAngles.y, ref currentVelocity, 0.1f), euler.z);
            }
        }

        private void LateUpdate()
        {

        }

    }
}
