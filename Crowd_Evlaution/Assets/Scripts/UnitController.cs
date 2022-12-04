using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;

public class UnitController : MonoBehaviour
{
    [SerializeField] private float distanceFactor = 1;
    [SerializeField] private float radius = 1;
    [SerializeField] private float swipeSpeed = 8;
    [SerializeField] private float texAnimationSpeed = 10;
    [SerializeField] private float maxXRange = 0.94f;
    [SerializeField] private float minXRange = -0.94f;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float tweenTime = 0.5f;
    [SerializeField] private float rayFromCameraDistance = 100; 
    [SerializeField] private Transform characterSpwnPoint;
    [SerializeField] private List<Unit> unitLists;
     public List<Unit> UnitLists { get { return unitLists; } set { unitLists = value; } }

    [field:SerializeField] private int year { get; set; } = 100;
    [field:SerializeField] private int characterCount { get; set; } = 1;
    [SerializeField] private int targetYear;

    [SerializeField] protected ObjectCreater objectCreater;
    [SerializeField] private TextMeshProUGUI yearText;
    [SerializeField] private GameObject textCanvas; 
    private bool swipe = false;
    [SerializeField] private bool onlyIncreaseCharacters = false;
    [SerializeField] private bool isNegativeYearNumber = false;
    private Camera camera;
    private Vector3 targetPosition;
    private bool shouldAnimateYearText = false;
    [SerializeField] private bool hasIntialInstantiationHappened = false;
   
    private System.Random randomNumber = new System.Random();
    private void Awake()
    {
        unitLists = new List<Unit>();
        yearText.text ="    " +year + "\n    YEARS";
        targetYear = year;
    }
    private void Start()
    {
        
        SpawnCharacter(year,characterCount,onlyIncreaseCharacters);
        camera = Camera.main;
        hasIntialInstantiationHappened = true;
        FrontLinePoints.instance.OnFrontLÝneTakeActionEvent += Ýnstance_OnFrontLÝneTakeActionEvent;
    }

    private void Ýnstance_OnFrontLÝneTakeActionEvent(object sender, List<Transform> frontLinePoints)
    {
        unitLists = Shuffle(unitLists);
        moveSpeed = 0;
        GoToFrontLinePoints(frontLinePoints);
        textCanvas.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            swipe = true;
        }

        if (Input.GetMouseButton(0) && swipe)
        {

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayFromCameraDistance))
            {
                targetPosition = new Vector3(hit.point.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, swipeSpeed * Time.deltaTime);
                Vector3 lookDirection = (targetPosition - transform.position).normalized;

            }

            ClampXRange();
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipe = false;
        }
      
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        ClampXRange();
        
        if (shouldAnimateYearText)
        {
            //StartCoroutine(AnimateTextCoroutine());
            year = (int)Mathf.Lerp(year, targetYear, Time.deltaTime * texAnimationSpeed);
            yearText.text = "    " + year + "\n    YEARS";
        }

    }
    private IEnumerator AnimateTextCoroutine()
    {
        while (targetYear!=year)
        {
            year = (int)Mathf.Lerp(year, targetYear, Time.deltaTime * texAnimationSpeed);
            yearText.text = "    " + year + "\n    YEARS";
            yearText.color = Color.green;
            yield return null;
        }
        yearText.color = Color.white;
    }
    private void ClampXRange()
    {
        if (transform.position.x >= maxXRange)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = maxXRange;
            currentPosition.y = transform.position.y; 
            currentPosition.z = transform.position.z;
            transform.position = currentPosition;
        }

        else if(transform.position.x < minXRange)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = minXRange;
            currentPosition.y = transform.position.y; 
            currentPosition.z = transform.position.z;
            transform.position = currentPosition;
        }
    }
    public void SpawnCharacter(int _year,int chararcterCount,bool onlyIncreaseCharactesrNumber)
    {
        if (_year <= objectCreater.currentYearMax && hasIntialInstantiationHappened)
        {
            if (!onlyIncreaseCharactesrNumber && !isNegativeYearNumber)
            {
                print("Return");
                return;
            }
            else
            {
                print("Not Return");
            }

        }
        if (!onlyIncreaseCharactesrNumber)
        {
            print("Clear");
            ClearLisOfUnits();
        }
       
        for (int i = 0; i < chararcterCount; i++)
        {
            Transform spawnedUnit =Instantiate(objectCreater.CreateObjec(_year), characterSpwnPoint.position,transform.rotation,transform);
            Unit unit = spawnedUnit.GetComponent<Unit>();
            unitLists.Add(unit);
        }
        FormaCharactersOrderly();
      
    }
    public void FormaCharactersOrderly()
    {
        for (int i = 0; i < unitLists.Count; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var NewPos = new Vector3(x, 0, z);

            unitLists[i].transform.DOLocalMove(NewPos, tweenTime).SetEase(Ease.InOutBack);
        }
    }
    private void ClearLisOfUnits()
    {
        for (int i = 0; i < unitLists.Count; i++)
        {
            unitLists[i].InformBulletBeforeDestroyingThisGameObject();

            Destroy(unitLists[i].gameObject);
        }
        unitLists.Clear();
    }   
    public void IncreaseOrDecreaseYear(int _targetYear)
    {
        onlyIncreaseCharacters = false;
        isNegativeYearNumber = _targetYear <0;
        objectCreater.currentYearMax = _targetYear < 0 ? objectCreater.currentYearMax -Mathf.Abs(_targetYear) : objectCreater.currentYearMax * 1;
        targetYear = _targetYear+year;
        shouldAnimateYearText =true;
        SpawnCharacter(targetYear, characterCount, onlyIncreaseCharacters);
    }
    public void IncreaseCharacter(int _characterNumbers)
    {
        onlyIncreaseCharacters = targetYear <= objectCreater.currentYearMax;
        int  targetCharacterCount = onlyIncreaseCharacters? _characterNumbers:characterCount+ _characterNumbers;
        characterCount += _characterNumbers;
        SpawnCharacter(year, targetCharacterCount, onlyIncreaseCharacters);
    }
    
    public  List<T> Shuffle<T>( List<T> list)
    {
        var source = list.ToList();
        int n = source.Count;
        var shuffled = new List<T>(n);
        shuffled.AddRange(source);
        while (n > 1)
        {
            n--;
            int k = randomNumber.Next(n + 1);
            print(k);
            T value = shuffled[k];
            shuffled[k] = shuffled[n];
            shuffled[n] = value;
        }
        return shuffled;
    }
    public void GoToFrontLinePoints(List<Transform> frontLinePoints)
    {
        for (int i = 0; i < unitLists.Count; i++)
        {
            unitLists[i].GoToFrontLinePoint(frontLinePoints[i]);
       

        }
    }
   
}
