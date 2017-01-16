using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Creatures
{
    class CreatureManager : NetworkBehaviour
    {
        [SerializeField] private GameObject RedCreatureFab;
        [SerializeField] private GameObject BlueCreatureFab;
        private List<GameObject> creatures;

        [ServerCallback]
        public void CmdDestroy()
        {
            if (!isServer)
            {
                Destroy(this);
                return;
            }

            NetworkServer.Reset();
        }


        [ServerCallback]
        public void CmdCreate()
        {
            if (!isServer)
            {
                Destroy(this);
                return;
            }

            creatures = new List<GameObject>();

            for (int i = 0; i < 100; ++i)
            {
                var posBlue = Vector3.zero;
                posBlue.y = 10;
                posBlue.x = i / 100f;
                posBlue.z = i / 100f;
                var blueCreature = Instantiate(BlueCreatureFab, posBlue, Quaternion.identity);

                var posRed = Vector3.zero;
                posBlue.y = 10;
                posRed.z = -i / 100f;
                posRed.x = -i / 100f;
                var redCreature = Instantiate(RedCreatureFab, posRed, Quaternion.identity);

                creatures.Add(blueCreature);
                creatures.Add(redCreature);
            }

            foreach (var creature in creatures)
            {
                NetworkServer.Spawn(creature);
            }

            creatures = null;
        }
    }
}
