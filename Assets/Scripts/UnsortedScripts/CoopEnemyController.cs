using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CoopEnemyController : MonoBehaviour
{
    private CoopManager coopManager;
    
    [HideInInspector] 
    public PhotonView photonComponent;

    private void OnEnable()
    {
        coopManager = CoopManager.instance;
        photonComponent = coopManager.photonComponent;
    }

    public void SendEnemySpawnInfo(int roomID)
    {
        for (int i = 0; i < EnemyTrackerController.Instance.spawnedEnemies.Count; i++)
        {
            EnemyController enemy = EnemyTrackerController.Instance.spawnedEnemies[i].GetComponent<EnemyController>();

            int enemyID = enemy.enemyID;
            int enemyLevel = enemy.enemyLevel;

            photonComponent.RPC("EnemySpawner", RpcTarget.Others, enemy.spawnID, roomID, enemyID, enemyLevel);
        }
    }

    [PunRPC]
    public void EnemySpawner(int ID, int roomID, int enemy, int enemyLevel)
    {
        EnemyTrackerController.Instance.GetNewEnemy(transform, true, enemy, enemyLevel, ID, roomID);
    }

    public void SendTrackingInfo(int ID, Vector3 position, Vector3 localRot, bool agro)
    {
        photonComponent.RPC("EnemyPostionTracking", RpcTarget.Others, ID, position.x, position.y, position.z, localRot.x, localRot.y, localRot.z, agro);
    }

    [PunRPC]
    public void EnemyPostionTracking(int ID, int posX, int posY, int posZ, int rotX, int rotY, int rotZ, bool agro)
    { 
        Vector3 position = new Vector3(posX, posY, posZ);
        Vector3 rotation = new Vector3(rotX, rotY, rotZ);

        for (int i = 0; i < EnemyTrackerController.Instance.otherPlayerSpawnedEnemies.Count; i++)
        {
            if (EnemyTrackerController.Instance.otherPlayerSpawnedEnemies[i].GetComponent<EnemyController>().spawnID == ID)
            {
                EnemyController enemy = EnemyTrackerController.Instance.otherPlayerSpawnedEnemies[i].GetComponent<EnemyController>();
                enemy.transform.position = position;
                enemy.transform.localEulerAngles = rotation;
                if (agro) { enemy.agroCurrentPlayer = false; }
                return;
            }
        }
    }

    public void ChangeAnimation(int spawnID, string animationClipName)
    {
        photonComponent.RPC("ChangeEnemyAnimation", RpcTarget.Others, spawnID, animationClipName);
    }

    [PunRPC]
    public void ChangeEnemyAnimation(int spawnID, string animationClipName)
    {
        if (EnemyTrackerController.Instance.GetEnemy(spawnID, true))
        {
            EnemyTrackerController.Instance.GetEnemy(spawnID, true).animationController.AnimationClip(animationClipName);
            return;
        }
    }

    public void AdjustEnemyHealth(float adjustmentValue, int spawnID, bool isBoss)
    {
        photonComponent.RPC("SyncEnemyHealth", RpcTarget.Others, adjustmentValue, spawnID, isBoss);
    }

    [PunRPC]
    public void SyncEnemyHealth(float adjustmentValue, int spawnID, bool isBoss)
    {
        if (!isBoss)
        {
            if (EnemyTrackerController.Instance.GetEnemy(spawnID, false))
            {
                EnemyTrackerController.Instance.GetEnemy(spawnID, false).enemyHealth.AdjustHealth(adjustmentValue, true);
                return;
            }

            else if (EnemyTrackerController.Instance.GetEnemy(spawnID, true))
            {
                EnemyTrackerController.Instance.GetEnemy(spawnID, true).enemyHealth.AdjustHealth(adjustmentValue, true);
                return;
            }
        }

        else
            EnemyTrackerController.Instance.spawnedBoss.GetComponentInChildren<EnemyHealth>().AdjustHealth(adjustmentValue, true);
    }

    public void EnteredRoom(int roomID)
    {
        photonComponent.RPC("OtherPlayerEnteredRoom", RpcTarget.Others, roomID);
    }

    [PunRPC]
    public void OtherPlayerEnteredRoom(int roomID)
    {
        RoomModel room = coopManager.coopDungeonBuild.GetLocalRoom(roomID);
        room.otherPlayerEnteredRoom = true;
    }

    public void RoomCleared(int roomID)
    {
        photonComponent.RPC("ARoomWasCleared", RpcTarget.Others, roomID);
    }

    [PunRPC]
    public void ARoomWasCleared(int roomID)
    {
        RoomModel room = coopManager.coopDungeonBuild.GetLocalRoom(roomID);
        room.roomCleared = true;
    }
}
