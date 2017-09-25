using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

public class PeopleSpawner : MonoBehaviour
{
	public static PeopleSpawner instance;
	public Transform[] spawnTargets;
	public PathSample[] spawnPoints;
	public Transform targetInstance;
	public OrbitCamera followCam;
	public bool spawnNewPeople;
	public float spawnTimer = 60;
	public int spawnCount = 1;

	public AppearancePreset heroPreset;

	List<PersonBehavior> activePeople;
	List<float> spawnTimers;
	float nextSpawnTime;
	int peopleLayer;
	Transform peopleParent;
	int timerLength = 1;
	float[] timerValues = { 5f, 7f, 45f, 55f, 130f, 150f };

	void Awake ()
	{
		instance = this;
		activePeople = new List<PersonBehavior> ();
		spawnTimers = new List<float> ();
		spawnPoints = SpawnPointManager.GetPath();
		peopleLayer = LayerMask.NameToLayer ( "People" );
		peopleParent = new GameObject ( "People Instances" ).transform;
	}

	void Start ()
	{
		SpawnPerson ();
		nextSpawnTime = Time.time + spawnTimer;
		if ( ModeController.spawnNonHero || ModeController.isTrainingMode )
		{
			for ( int i = 0; i < spawnCount - 1; i++ )
			{
				SpawnPerson ( false );
			}
		}
	}

	void Update ()
	{
		if ( spawnNewPeople )
		{
			for ( int i = spawnTimers.Count - 1; i > 0; i-- )
			{
				if ( Time.time > spawnTimers[i] )
				{
					SpawnPerson ( false, i );
				}
			}
		}
	}

	void OnGUI ()
	{
		if ( !ModeController.isTrainingMode )
			return;

		string timerText = timerLength == 0 ? "Spawn timer: short" :
			timerLength == 1 ? "Spawn timer: medium" :
			"Spawn timer: long";

		GUIStyle button = GUI.skin.button;
//		button.clipping = TextClipping.Overflow;
//		button.wordWrap = false;
//		button.fontSize = (int) ( 22f * Screen.height / 1080 );

		Vector2 size = button.CalcSize ( new GUIContent ( timerText ) );
		Rect r = new Rect ( Screen.width/4, Screen.height - size.y - 10, size.x, size.y );
		if ( GUI.Button ( new Rect ( r.x - 5, r.y, r.width + 10, r.height ), timerText ) )
		{
			timerLength = ++timerLength % 3;
			int timerIndex = timerLength * 2;
			for ( int i = spawnTimers.Count - 1; i >= 0; i-- )
				spawnTimers[i] = Time.time + Random.Range ( timerValues [ timerIndex ], timerValues [ timerIndex + 1 ] );
		}
	}

	void SpawnPerson (bool isTarget = true, int activeIndex = -1)
	{
		Transform target = isTarget ? ( spawnTargets [ heroPreset.female ? 1 : 0 ] ) : spawnTargets [ Random.Range ( 2, spawnTargets.Length ) ];
		if ( isTarget )
		{
			PathSample[] path = HeroPathManager.GetPath ();
			if ( targetInstance != null )
				Destroy ( targetInstance.gameObject );
			targetInstance = Instantiate ( target );
			targetInstance.position = path[0].position;
			targetInstance.GetComponent<CharacterCustomization> ().SetAppearance ( heroPreset );
			targetInstance.gameObject.SetActive ( true );
			followCam.target = targetInstance;
			targetInstance.name = "Hero";
			targetInstance.GetComponent<PersonBehavior> ().UsePath ( path, OnPersonEndedPath );
			
		} else
		{
			PathSample spawn = GetRandomPoint ();
			Transform person = Instantiate ( target );
			person.position = spawn.position;
			person.gameObject.SetActive ( true );
			PersonBehavior behavior = person.GetComponent<PersonBehavior> ();
			int timerIndex = timerLength * 2;
			if ( activeIndex != -1 )
			{
				Destroy ( activePeople [ activeIndex ].gameObject );
				activePeople [ activeIndex ] = behavior;
				spawnTimers [ activeIndex ] = Time.time + Random.Range ( timerValues [ timerIndex ], timerValues [ timerIndex + 1 ] );
				
			} else
			{
				activePeople.Add ( behavior );
				spawnTimers.Add ( Time.time + Random.Range ( timerValues [ timerIndex ], timerValues [ timerIndex + 1 ] ) );
			}
			SetLayerRecursively ( person, peopleLayer );
			person.GetComponent<PersonBehavior> ().Wander ();
			person.parent = peopleParent;
		}
	}

	PathSample GetRandomPoint ()
	{
		return spawnPoints [ Random.Range ( 0, spawnPoints.Length ) ];
	}

	void SetLayerRecursively (Transform t, int layer)
	{
		t.gameObject.layer = layer;
		if ( t.childCount > 0 )
		{
			foreach ( Transform c in t )
			{
				SetLayerRecursively ( c, layer );
			}
		}
	}

	void OnPersonEndedPath (PersonBehavior person)
	{
		if ( person.transform == targetInstance && gameObject.activeSelf )
		{
			SpawnPerson ();
		}
	}
}
