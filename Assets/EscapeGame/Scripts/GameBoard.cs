using UnityEngine;
using System.Collections.Generic;
using System;

public class GameBoard : MonoBehaviour {

    public enum PyloneType {
        RED = 0,
        BLACK = 1,
        GREEN = 2
    }

    public enum Level {
        ROOM1 = 1,
        ROOM2 = 2
    }

    /// <summary>
    /// The Entity that manage all the doors
    /// </summary>
    public DoorsManager Doors;

    /// <summary>
    /// The pylone nodes referenced for the first room
    /// </summary>
    public PyloneNode PyloneNodeR1;
    public PyloneNode PyloneNodeR2;
    public PyloneNode PyloneNodeR3;

    public PyloneNode[] listPylones = new PyloneNode[3];
    public List<int> pyloneActived = new List<int>();

	private void Awake() {
        listPylones[0] = PyloneNodeR1;
        listPylones[1] = PyloneNodeR2;
        listPylones[2] = PyloneNodeR3;
	}

	// Use this for initialization
	void Start() {
        PyloneNodeR1.Type = PyloneType.BLACK;
        PyloneNodeR2.Type = PyloneType.RED;
        PyloneNodeR3.Type = PyloneType.GREEN;
    }

    // Update is called once per frame
    void Update() {
        if (pyloneActived.Count > 1) {
            foreach (var i in pyloneActived) {
                if (!listPylones[i].HActivated)
                    listPylones[i].ShineH();
            }
        }

        if (pyloneActived.Count > 2) {
            OpenNextRoom(Level.ROOM2);
        }
    }

    public PyloneNode GetNearestPylone(Vector3 boxPosition) {
        PyloneNode currentPylone = PyloneNodeR1;
        float minDistance = float.MaxValue;

        foreach (PyloneNode p in listPylones) {
            float currentDistance = Vector3.Distance(boxPosition, p.transform.position);
            if (currentDistance < minDistance) {
                currentPylone = p;
                minDistance = currentDistance;
            }
        }

        return currentPylone;
    }

    public bool BoxMatchPylone(ColoredBox box, PyloneNode pylone) {
        if (pylone.Type == box.Type)
            return true;

        return false;
    }

    public bool SetBoxOnClosestPylone(ColoredBox box) {
        PyloneNode closest = GetNearestPylone(box.transform.position);

        if (Vector3.Distance(box.transform.position, closest.transform.position) < 1) {
            box.UnGrab();
            box.transform.position = new Vector3(closest.transform.position.x, closest.transform.position.y + 1.5f, closest.transform.position.z);
            bool isMatching = BoxMatchPylone(box, closest);

            if (isMatching) {
                closest.ShineV();
                pyloneActived.Add(Array.IndexOf(listPylones, closest));
            }
                
            return isMatching;
        }
        return false;
    }

    public void OpenNextRoom(Level lvl) {
        Doors.Open(lvl);
    }

}
