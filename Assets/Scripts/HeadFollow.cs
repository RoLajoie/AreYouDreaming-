using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollow : MonoBehaviour
{
    public Transform player;
    public Transform npcHead;

    //TODO: Add timer so the head movement is randomized and unpredictable

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        //finding the x, y and z of the player 
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, player.position.z);

        //direction of the player is the current position of player subtracted by the current npc head's position 
        Vector3 directionToPlayer = targetPosition - npcHead.position;

        //calculating the rotation that the npc's head should face towards the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        npcHead.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
    }
}
