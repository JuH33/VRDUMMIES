using UnityEngine;
using UnityEngine.Assertions;

public class DoorsManager : MonoBehaviour {

    /// <summary>
    /// Manager as known by GameBoard
    /// </summary>
    public RoomManager Room;

    /// <summary>
    /// Doors references for physics & gameplay behaviors
    /// </summary>
    public Transform Door1;
    public Transform Door2;
    public Transform Door3;
    public Transform DoorToOpen;

    /// <summary>
    /// The closing doors speed
    /// </summary>
    [SerializeField] private float Speed;
    [SerializeField] private float DoorOpenElevation;

    public bool CloseTheDoor;

	void Start() {
        Assert.IsNotNull(Room);
        Room.OnEnter -= BehaviorOnEnterInTheRoom;
        Room.OnEnter += BehaviorOnEnterInTheRoom;
    }

	private void FixedUpdate() {
        if (CloseTheDoor)
            Close();

        if (DoorToOpen != null)
            Open();
	}

    /// <summary>
    /// Behaviors the on enter in the room. Used as a delegate.
    /// </summary>
    /// <param name="collider">Collider.</param>
	private void BehaviorOnEnterInTheRoom(Collider collider) {
        CloseTheDoor = true;
    }

    private void Close() {
        Door1.position = Vector3.MoveTowards(Door1.position, new Vector3(Door1.position.x, 0, Door1.position.z), Speed);
        Door2.position = Vector3.MoveTowards(Door2.position, new Vector3(Door2.position.x, 0, Door2.position.z), Speed);
        Door3.position = Vector3.MoveTowards(Door3.position, new Vector3(Door3.position.x, 0, Door3.position.z), Speed);
        if (Door3.position.y <= 0) 
            CloseTheDoor = false;
    }

    private void Open() {
        DoorToOpen.position = Vector3.MoveTowards(DoorToOpen.position, new Vector3(DoorToOpen.position.x, DoorOpenElevation, DoorToOpen.position.z), Speed);
        if (DoorToOpen.position.y >= DoorOpenElevation)
            DoorToOpen = null;
    }

    public void Open(GameBoard.Level level) {
        Debug.Log("level: " + level);
        switch(level) {
            case GameBoard.Level.ROOM1:
                DoorToOpen = Door1;
                DoorToOpen = Door2;
                break;
            case GameBoard.Level.ROOM2:
                DoorToOpen = Door3;
                break;
        }
    }

    void OnDestroy() {
        Room.OnEnter -= BehaviorOnEnterInTheRoom;
        Room = null;
    }
}
