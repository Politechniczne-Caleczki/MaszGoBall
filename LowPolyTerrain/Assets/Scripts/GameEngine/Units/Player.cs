

using Assets.Scripts.GUI.Game;
using Assets.Scripts.Terrains;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.GameEngine.Units
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player: NetworkBehaviour
    {
        [SerializeField]
        private Rigidbody Rigidbody;
        [SerializeField]
        private Tentakel Tentakel;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Light light;

        private Vector3 Angle;
        private Vector3 Offset;
        private float currentVelocity;
        public string Name { get { return this._name; } private set { this._name = value; } }
        public NationType NationType { get; private set; }
        private bool CanJump { get;  set; }
        Vector3 NewPosition { get; set; }

        [SyncVar]
        private string _name;
        [Command]
        public void CmdTouch(GameObject _nation)
        {
            Nation nation = _nation.GetComponent<Nation>();

            if(NationType== NationType.None)
            {
                CmdAddNation(_nation);
                return;
            }

            if (nation.NationType == NationType)
                CmdAddNation(_nation);
            else
                KillPlayer(nation);
        }
        private void KillPlayer(Nation nation)
        {
            if (nation is Bomb)
            {
                Debug.Log("Zabity przez gracza");
            }else
            {
                Debug.Log("Zabity przez moba");
            }
            nation.Explosion();
        }

        [Command]
        private void CmdAddNation(GameObject _nation)
        {
            Nation nation = _nation.GetComponent<Nation>();
            if (Tentakel.IsReady && nation.CanCath)
            {
                Debug.Log("Add nation: " + nation.NationType);
                NationType = nation.NationType;
                nation.CmdCatch();
                Tentakel.Catch(_nation);
                light.enabled = true;
                switch(nation.NationType)
                {
                    case NationType.Red:
                        {
                            GameGUI.PlayerImage.color = light.color = Color.red;
                        }
                        break;
                    case NationType.Blue:
                        {
                            GameGUI.PlayerImage.color = light.color = Color.blue;
                        }
                        break;
                    case NationType.None:
                        {
                            GameGUI.PlayerImage.color = light.color =  new Color(1, 1, 1, 0);
                        }break;
                }
            }
        }

        [Command]
        private void CmdShot()
        {
            Tentakel.Shot();
        }
        private void Start()
        {
            Offset = new Vector3(0, 0.05f, -0.15f);
            Angle = Vector3.zero;

            gameObject.layer = 8;
            CanJump = true;

            Terrains.Terrain t = GameObject.FindObjectOfType<Terrains.Terrain>();

            Name = string.Format("Player_{0}",GameObject.FindObjectsOfType<Assets.Scripts.GameEngine.Units.Player>().Length+8);

            transform.position = NewPosition = t.SpawnPoints[Random.Range(0, t.SpawnPoints.Count)].transform.position + new Vector3(0, .5f, 0);

            UnityEngine.Camera.main.GetComponent<Camera>().player = this;

            Tentakel.OnEndShot = () =>
            {
                switch(NationType)
                {
                    case NationType.Red:
                        {
                            NationType = NationType.Blue;
                            GameGUI.PlayerImage.color = light.color = Color.blue;
                        }
                        break;

                    case NationType.Blue:
                        {
                            NationType = NationType.Red;
                            GameGUI.PlayerImage.color = light.color = Color.red;
                        }
                        break;
                    case NationType.None:
                        {
                            NationType = NationType.None;
                            GameGUI.PlayerImage.color = light.color = new Color(1, 1, 1, 0);
                        }
                        break;
                }
            };
        }
        private void FixedUpdate()
        {
            transform.position = NewPosition;
        }
        private void Update()
        {
            if (!isLocalPlayer)
            {
                Destroy(this);
                return;
            }

            NewPosition = transform.position;

            NewPosition += (Quaternion.Euler(transform.eulerAngles) * new Vector3(Input.GetAxis("Horizontal") / 500, 0, Input.GetAxis("Vertical")/100));

            if (Input.GetAxis("Vertical") != 0)
            {
                animator.SetBool("Run", true);
                if (animator.GetInteger("State") != 1)
                    animator.SetInteger("State", 2);
            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetInteger("State", 0);
            }


            if (Input.GetAxis("Fire2") != 0)
            {
                Vector3 euler = transform.eulerAngles;
                euler.y += Input.GetAxis("Rotate");
                transform.eulerAngles = euler;
            }

            if (!Tentakel.IsReady)
            {
                if (Input.GetAxis("Fire1") != 0)
                    CmdShot();
            }

            Jump();

            CameraUpdate();
        }
        private void Jump()
        {
            if (CanJump)
            {
                if (Input.GetAxis("Jump") != 0)
                {
                    Rigidbody.AddForce(new Vector3(0, 20, 0), ForceMode.Acceleration);
                    animator.SetInteger("State", 1);
                }
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            switch(collision.gameObject.layer)
            {
                case 13:
                    {
                        if(!CanJump)
                            StartCoroutine(JumpDelay());
                    }
                    break;
            }
        }
        private IEnumerator JumpDelay()
        {
            if (!CanJump)
            {
                yield return new WaitForSeconds(0.1f);
                CanJump = true;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            switch (collision.gameObject.layer)
            {
                case 13:
                    {
                        CanJump = false;
                    }
                    break;
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            switch (collision.gameObject.layer)
            {
                case 13:
                    {
                        Jump();
                    }
                    break;
            }
        }
        public void EndJump()
        {
            if(animator.GetBool("Run"))
                animator.SetInteger("State", 2);
            else
                animator.SetInteger("State", 0);

        }
        private void CameraUpdate()
        {
            var camera = UnityEngine.Camera.main.transform;
            camera.transform.position = transform.position + Quaternion.Euler(Angle) * Offset;
            camera.transform.eulerAngles = Angle;

            Offset.z += Input.GetAxis("Mouse ScrollWheel");
            if (Offset.z > -0.15f)
                Offset.z = -0.15f;

            if (Offset.z < -1.5f)
                Offset.z = -1.5f;

            Angle.y += Input.GetAxis("Mouse X") * 2;
            Angle.x += Input.GetAxis("Mouse Y") * 2;
            if (Angle.x > 90)
                Angle.x = 90;

            if (Angle.x < -90)
                Angle.x = -90;

            if (Input.GetAxis("Fire2") == 0)
            {
                Angle.y += Input.GetAxis("Rotate") * 2;

                Vector3 euler = transform.eulerAngles;
                transform.eulerAngles = new Vector3(euler.x, Mathf.SmoothDampAngle(euler.y, camera.transform.eulerAngles.y, ref currentVelocity, 0.1f), euler.z);
            }
        }

        public string ScoreString
        {
            get { return string.Format("{0,-35} {1,35} / {2,1}", Name, Kills, Deaths); }
        }

        public int Kills { get { return kills; } private set { kills = value; } }
        public int Deaths { get { return deaths; } private set {deaths = value; } }

        [SyncVar]
        private int kills;

        [SyncVar]
        private int deaths;



    }
}
