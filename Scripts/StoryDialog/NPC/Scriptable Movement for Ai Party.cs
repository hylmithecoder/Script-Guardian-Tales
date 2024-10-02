using UnityEngine;
using System.Collections.Generic;

public class AiMember : MonoBehaviour, Interactable
{
    public GameObject leaderParty;
    public PlayerMovementAndroid leader;
    public bool eventSudahSelesai = false;
    public int followDelay = 15; // Increased delay for more RPG-like following
    public Animator animasi;
    public float followDistance = 1f; // Distance to maintain from the leader
    private Queue<Vector2> leaderPositions;
    private float walk;
    private Collider2D hitbox;
    [SerializeField] Dialog dialog;
    [SerializeField] Dialog clearEventDialog;
    private static int hitunganVariabel = 0;
    public int requeirementVariabel = hitunganVariabel;
    private bool isFollowLeader = false;
    private bool hasInteracted = false;
    private Transform self;

    private void Start()
    {
        animasi = GetComponent<Animator>();
        leader = leaderParty.GetComponent<PlayerMovementAndroid>();
        leaderPositions = new Queue<Vector2>();
        walk = leader.kecepatanGerak;
        hitbox = GetComponent<Collider2D>();
        self = GetComponent<Transform>();
    }

    private void Update()
    {
        if (isFollowLeader)
        {
            if (!hasInteracted)
            {
                hitunganVariabel += 1;
                hasInteracted = true;
                Debug.Log("Variabel Quest Sekarang " + requeirementVariabel);
            }
            TrackLeaderPosition();
            FollowLeader();
        }
        if (eventSudahSelesai)
        {
            StopFollowLeader();
            hasInteracted = false;
        }
    }

    public void Interaksi()
    {
        StartDialog();
    }

    private void StartDialog()
    {
        DialogManager.Instance.StartDialog(dialog, self);
        isFollowLeader = true;
        eventSudahSelesai = false;
    }

    private void TrackLeaderPosition()
    {
        leaderPositions.Enqueue(leaderParty.transform.position);
        if (leaderPositions.Count > followDelay)
        {
            leaderPositions.Dequeue();
        }
    }

    private void FollowLeader()
    {
        walk = leader.isSprinting ? leader.kecepatanSprint : leader.kecepatanGerak;
        hitbox.enabled = false;

        if (leaderPositions.Count > 0)
        {
            Vector2 targetPosition = leaderPositions.Peek();
            Vector2 currentPosition = transform.position;
            Vector2 direction = targetPosition - currentPosition;

            if (direction.magnitude > followDistance)
            {
                Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, walk * Time.deltaTime);
                transform.position = newPosition;

                // Update animation
                animasi.SetFloat("moveX", direction.x);
                animasi.SetFloat("moveY", direction.y);
                animasi.SetBool("isMoving", true);
            }
            else
            {
                animasi.SetBool("isMoving", false);
            }

            // Adjust facing direction even when not moving
            if (direction != Vector2.zero)
            {
                animasi.SetFloat("moveX", direction.x);
                animasi.SetFloat("moveY", direction.y);
            }
        }
    }

    private void StopFollowLeader()
    {
        isFollowLeader = false;
        leaderPositions.Clear();
        animasi.SetBool("isMoving", false);
        hitbox.enabled = true;
    }

    public int GetCountVariabelQuest()
    {
        return requeirementVariabel = hitunganVariabel;
    }
}