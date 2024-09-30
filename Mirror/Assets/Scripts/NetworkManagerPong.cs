using UnityEngine;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

namespace Mirror.Examples.car
{
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class NetworkManagerPong : NetworkManager
    {
        /*
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            
            // spawn ball if two players
            if (numPlayers == 1)
            {
                //poner tus spawns
                //spill = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Spill"));
                //NetworkServer.Spawn(ball);
            }
        }
        */

            /*
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            // destroy ball
            if (spill != null)
                NetworkServer.Destroy(spill);

            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
        */
    }
}
