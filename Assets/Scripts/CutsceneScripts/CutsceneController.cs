using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    [Header("Timeline:")]
    [SerializeField] private PlayableDirector timeline;

    [Header("Player Staging:")]
    [SerializeField] private Transform stagingPosition;
    [SerializeField] private float movementSpeed = 5f;

    [Header("Actors:")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] actors;

    private PlayerInputController inputController;
    private Animator playerAnimator;
    private PlayerAnimationController animationController;
    private DialogueHandler dialogueHandler;

    private bool cutsceneActive;
    private bool cutscenePlaying;

    private void Awake()
    {
        inputController = FindAnyObjectByType<PlayerInputController>();
        animationController = FindAnyObjectByType<PlayerAnimationController>();
        playerAnimator = player.GetComponent<Animator>();
        dialogueHandler = FindAnyObjectByType<DialogueHandler>();
    }

    public void SetCutsceneActive(){
        cutsceneActive = true;

        SpawnActors();
    }

    private void SpawnActors(){
        foreach (GameObject actor in actors)
        {
            actor.SetActive(true);

            Animator animator = actor.GetComponent<Animator>();
        }
    }

    public void EnteredTrigger()
    {
        if (!cutsceneActive || cutscenePlaying){
            return;
        }

        cutscenePlaying = true;
        StartCoroutine(StartCutscene());

    }

    private IEnumerator MovePlayerToStagingPosition()
    {
        animationController.enabled = false;
        playerAnimator.SetBool("isMoving", true);
        
        while (player.transform.position.y != stagingPosition.position.y)
        {
            playerAnimator.SetFloat("Horizontal", 0f);
            playerAnimator.SetFloat("Vertical", Mathf.Sign(stagingPosition.position.y - player.transform.position.y));
            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                new Vector3(player.transform.position.x,stagingPosition.position.y, player.transform.position.z),
                movementSpeed * Time.deltaTime
            );

            yield return null;
        }

        while (player.transform.position.x != stagingPosition.position.x)
        {
            playerAnimator.SetFloat("Vertical", 0f);
            playerAnimator.SetFloat("Horizontal", Mathf.Sign(stagingPosition.position.x - player.transform.position.x));
            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                new Vector3(stagingPosition.position.x, player.transform.position.y, player.transform.position.z),
                movementSpeed * Time.deltaTime
            );

            yield return null;
        }

        player.transform.position = stagingPosition.position;
        playerAnimator.SetBool("isMoving", false);
        animationController.enabled = true;
    }

    private IEnumerator StartCutscene(){
        StopPlayerControl();
        yield return StartCoroutine(MovePlayerToStagingPosition());
        timeline.Play();
    }

    private void StopPlayerControl()
    {
        //animationController.enabled = false;
        //inputController.enabled = false;
        inputController.StopPlayerMovement();
    }

    private void ResumePlayerControl()
    {
        //animationController.enabled = true;
        //inputController.enabled = true;
        inputController.ResumePlayerMovement();
    }

    private void OnEnable(){
        timeline.stopped += OnTimelineStopped;
    }

    private void OnDisable(){
        timeline.stopped -= OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (!dialogueHandler.dialogueBox.activeSelf) 
        {
            ResumePlayerControl();
        }
    }
}
