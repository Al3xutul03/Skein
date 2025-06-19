using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerInput : MonoBehaviour, IObserver<MonoBehaviour>
{
    private CombatCamera combatCamera;
    private TurnManager turnManager;
    private GridManager gridManager;
    private PlayerUI playerUI;

    private EnemyStatus enemyStatus;
    private PlayerStatus playerStatus;
    private ICharacter shownCharacter;

    private AttackInfo attackInfo;
    private BuffInfo buffInfo;

    private SelectionIndicator selectionIndicator;
    private GameObject currentSelectedObject;
    private ICharacter currentSelected;

    private Button currentAbilityButton;
    private IAbility currentAbility;

    private Pathfinder pathfinder;
    private List<Node> path = new List<Node>();
    private bool isPathFollowed = false;

    [SerializeField]
    private ProgressBar prototypeBar;

    private Timer statusTimer;
    private Timer attackInfoTimer;
    private Timer buffInfoTimer;

    [SerializeField]
    private float statusTime = 0.75f;
    [SerializeField]
    private float attackInfoTime = 3.0f;
    [SerializeField]
    private float buffInfoTime = 1.5f;

    // Action dictionary for different types of subscribers
    private Dictionary<Type, Action<MonoBehaviour>> onNextTypes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        combatCamera = GameObject.FindWithTag("CombatCamera").GetComponent<CombatCamera>();
        turnManager = GameObject.FindWithTag("TurnManager").GetComponent<TurnManager>();
        gridManager = GameObject.FindWithTag("GridManager").GetComponent<GridManager>();
        enemyStatus = GameObject.FindWithTag("CombatUI").GetComponentInChildren<EnemyStatus>();
        playerStatus = GameObject.FindWithTag("CombatUI").GetComponentInChildren<PlayerStatus>();
        attackInfo = GameObject.FindWithTag("CombatUI").GetComponentInChildren<AttackInfo>();
        buffInfo = GameObject.FindWithTag("CombatUI").GetComponentInChildren<BuffInfo>();
        playerUI = GameObject.FindWithTag("PlayerUI").GetComponent<PlayerUI>();
        selectionIndicator = GameObject.FindWithTag("SelectionIndicator").GetComponent<SelectionIndicator>();
        statusTimer = transform.GetChild(0).GetComponent<Timer>();
        attackInfoTimer = transform.GetChild(1).GetComponent<Timer>();
        buffInfoTimer = transform.GetChild(2).GetComponent<Timer>();

        statusTimer.Subscribe(this);
        statusTimer.SetTimer(statusTime);

        attackInfoTimer.Subscribe(this);
        attackInfoTimer.SetTimer(attackInfoTime);

        buffInfoTimer.Subscribe(this);
        buffInfoTimer.SetTimer(buffInfoTime);

        currentSelectedObject = null;
        currentAbilityButton = null;
        currentAbility = null;

        pathfinder = GameObject.FindWithTag("Pathfinder").GetComponent<Pathfinder>();
        onNextTypes = new Dictionary<Type, Action<MonoBehaviour>> { { typeof(Timer), OnNextTimer } };
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraMovement();
        HandleCameraRotation();

        if (turnManager.IsPlayersTurn())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                turnManager.ChangeTurn();
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            prototypeBar.CurrentValue -= 5.0f;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            prototypeBar.CurrentValue += 5.0f;
        }

        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);
            if (!hasHit) { return; }

            if (hit.transform.tag == "Tile" &&
                currentSelected.NoActions > 0)
            {
                Vector2Int targetCoords = hit.transform.GetComponent<Tile>().coords;
                if (turnManager.IsCurrentCharacter(currentSelectedObject) &&
                    currentSelectedObject.GetComponent<Player>() != null &&
                    gridManager.GetNode(targetCoords).isWalkable == true &&
                    !gridManager.PlayerCoords.ContainsKey(targetCoords) &&
                    !gridManager.EnemyCoords.ContainsKey(targetCoords) &&
                    !isPathFollowed)
                {
                    Vector2Int startCoords = new Vector2Int(
                        (int)currentSelectedObject.transform.position.x,
                        (int)currentSelectedObject.transform.position.z) / gridManager.UnityGridSize;

                    isPathFollowed = true;
                    pathfinder.SetNewDestination(startCoords, targetCoords);
                    RecalculatePath(true);

                    currentSelected.NoActions -= 1;
                    playerUI.UpdatePlayerUIElement(UIUpdateType.Actions);
                    //currentSelected.transform.position = new Vector3(targetCoords.x, currentSelected.transform.position.y, targetCoords.y);
                }
            }
            else if (hit.transform.GetComponent<Enemy>() != null) NotifySelection(hit.transform.GetComponent<Enemy>());
        }

        //if (Input.GetMouseButtonDown(1))
        //{
            //currentAbilityButton.interactable = true;
            //currentAbilityButton = null;
            //currentAbility = null;
        //}
    }

    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathfinder.StartCoords;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopCoroutine(FollowPath());
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        GameObject currentFollower = currentSelectedObject;
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = currentFollower.transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coords);
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * 5;
                currentFollower.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        } 
        isPathFollowed = false;
    }

    private void HandleCameraRotation()
    {
        float cameraRotation = 0.0f;
        if (Input.GetKey(KeyCode.Q))
        {
            cameraRotation += -1.0f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            cameraRotation += 1.0f;
        }
        if (cameraRotation != 0.0f)
        {
            combatCamera.RotateCamera(cameraRotation);
        }
    }

    private void HandleCameraMovement()
    {
        Vector3 cameraMovement = Vector3.zero;
        if (Input.GetKey(KeyCode.S))
        {
            cameraMovement += Vector3.back;
        }
        if (Input.GetKey(KeyCode.W))
        {
            cameraMovement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraMovement += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraMovement += Vector3.left;
        }
        //if (Input.mouseScrollDelta.y > 0)
        //{
        //    cameraMovement += Vector3.up;
        //}
        //if (Input.mouseScrollDelta.y < 0)
        //{
        //    cameraMovement += Vector3.down;
        //}
        if (cameraMovement != Vector3.zero)
        {
            cameraMovement.Normalize();
            combatCamera.MoveCamera(cameraMovement);
        }
    }

    public void ChangeSelectedAbility(Button button)
    {
        Debug.Log("Ability Button Pressed!");
        if (currentAbilityButton != null) currentAbilityButton.interactable = true;
        button.interactable = false;

        var abilityButton = button.GetComponent<AbilityButton>();
        this.currentAbilityButton = button;
        this.currentAbility = abilityButton.Ability;
        Debug.Log($"Button ID: {currentAbilityButton.GetInstanceID()}");
        Debug.Log($"Ability Name: {currentAbility.Name}");
    }

    public void NotifySelection(Enemy enemy)
    {
        if (currentSelectedObject == enemy.gameObject) { return; }

        selectionIndicator.ChangeTarget(enemy);
        Debug.Log("Selection Registered");
        if (currentAbility == null) Debug.Log("No Ability Selected!");

        if (currentSelected != null && currentAbility is StrikeAbility)
        {
            Debug.Log("Strike Ability detected");
            var strikeAbility = (StrikeAbility)currentAbility;

            Vector2Int startCoords = gridManager.GetCoordinatesFromPosition(currentSelectedObject.transform.position);
            Vector2Int targetCoords = gridManager.GetCoordinatesFromPosition(enemy.transform.position);
            pathfinder.SetNewDestination(startCoords, targetCoords);

            if (strikeAbility.NoTargets == 1 &&
                currentSelected.NoActions >= strikeAbility.ActionCost &&
                pathfinder.GetNewPath().Count - 1 <= strikeAbility.Range)
            {
                strikeAbility.Targets.Add(enemy);
                strikeAbility.Use();
                strikeAbility.Targets.Clear();
            }

            playerUI.UpdatePlayerUIElement(UIUpdateType.Actions);
        }

        currentSelectedObject = enemy.gameObject;
        currentSelected = enemy;
    }

    public void NotifySelection(Player player)
    {
        if (currentSelectedObject == player.gameObject) { return; }

        selectionIndicator.ChangeTarget(player);
        Debug.Log("Selection Registered");
        if (currentAbility == null) Debug.Log("No Ability Selected!");

        if (currentSelected != null && currentAbility is BuffAbility)
        {
            Debug.Log("Buff Ability detected");
            var buffAbility = (BuffAbility)currentAbility;

            Vector2Int startCoords = gridManager.GetCoordinatesFromPosition(currentSelectedObject.transform.position);
            Vector2Int targetCoords = gridManager.GetCoordinatesFromPosition(player.transform.position);
            pathfinder.SetNewDestination(startCoords, targetCoords);

            if (buffAbility.Owner.ID == currentSelected.ID &&
                buffAbility.NoTargets == 1 &&
                currentSelected.NoActions >= buffAbility.ActionCost &&
                pathfinder.GetNewPath().Count - 1 <= buffAbility.Range)
            {
                buffAbility.Targets.Add(player);
                buffAbility.Use();
                buffAbility.Targets.Clear();
            }

            playerUI.UpdatePlayerUIElement(UIUpdateType.Actions);
        }

        currentSelectedObject = player.gameObject;
        currentSelected = player;
    }

    public void NotifyMouseEnter(ICharacter character)
    {
        statusTimer.SetTimer(statusTime);
        statusTimer.StartTimer();
        shownCharacter = character;
    }

    public void NotifyMouseExit(ICharacter character)
    {
        statusTimer.StopTimer();
        statusTimer.ResetTimer();

        if (shownCharacter is Enemy) { enemyStatus.Show(false); }
        else { playerStatus.Show(false); }
    }

    public void NotifyStrike()
    {
        attackInfo.Show(true);
        attackInfoTimer.SetTimer(attackInfoTime);
        attackInfoTimer.StartTimer();
    }

    public void NotifyBuff()
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 localPoint;

        bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.FindWithTag("CombatUI").GetComponent<RectTransform>(),
            mouseScreenPos, null, out localPoint);

        var rectTransform = buffInfo.gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = localPoint + new Vector2(rectTransform.rect.width, rectTransform.rect.height) / 2;
        buffInfo.Show(true);
        buffInfoTimer.SetTimer(buffInfoTime);
        buffInfoTimer.StartTimer();
    }

    public void OnCompleted() { }

    public void OnError(Exception error) { Debug.LogError(error); }

    public void OnNext(MonoBehaviour monoBehaviour) { onNextTypes[monoBehaviour.GetType()](monoBehaviour); }

    public void OnNextTimer(MonoBehaviour timer)
    {
        if (timer == statusTimer)
        {
            Vector2 mouseScreenPos = Input.mousePosition;
            Vector2 localPoint;

            bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.FindWithTag("CombatUI").GetComponent<RectTransform>(),
                mouseScreenPos, null, out localPoint);

            if (shownCharacter is Enemy)
            {
                var rectTransform = enemyStatus.gameObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = localPoint + new Vector2(rectTransform.rect.width, rectTransform.rect.height) / 2;
                enemyStatus.SetText(shownCharacter.ID, shownCharacter.CurrentHP, shownCharacter.MaxHP);
                enemyStatus.Show(true);
            }
            else
            {
                var rectTransform = playerStatus.gameObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = localPoint + new Vector2(rectTransform.rect.width, rectTransform.rect.height) / 2;
                playerStatus.SetText(shownCharacter as Player);
                playerStatus.Show(true);
            }
        }

        if (timer == attackInfoTimer) { attackInfo.Show(false); }

        if (timer == buffInfoTimer) { buffInfo.Show(false); }
    }
}
