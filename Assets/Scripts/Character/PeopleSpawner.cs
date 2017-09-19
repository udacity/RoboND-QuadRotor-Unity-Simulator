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

	[HideInInspector]
	public bool useHeroPreset;
	public AppearancePreset heroPreset;
	[HideInInspector]
	public bool othersWithHero;

	List<PersonBehavior> activePeople;
	List<float> spawnTimers;
	float nextSpawnTime;
	int peopleLayer;
	Transform peopleParent;

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
		if ( !useHeroPreset || othersWithHero )
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
			// respawn hero when they've completed a path instead
//			if ( Time.time > nextSpawnTime )
//			{
//				SpawnPerson ();
//				nextSpawnTime = Time.time + spawnTimer;
//			}
			for ( int i = spawnTimers.Count - 1; i > 0; i-- )
			{
				if ( Time.time > spawnTimers[i] )
				{
					SpawnPerson ( false, i );
				}
			}
		}
	}

	void SpawnPerson (bool isTarget = true, int activeIndex = -1)
	{
		Transform target = ( useHeroPreset && isTarget ) ? ( spawnTargets [ heroPreset.female ? 1 : 0 ] ) : spawnTargets [ Random.Range ( 0, spawnTargets.Length ) ];
//		Transform spawn = GetRandomPoint ();
		if ( isTarget )
		{
			PathSample[] path = HeroPathManager.GetPath ();
			if ( targetInstance != null )
				Destroy ( targetInstance.gameObject );
			targetInstance = Instantiate ( target );
			targetInstance.position = path[0].position;
			if ( useHeroPreset )
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
//			person.GetComponent<CharacterCustomization> ().SetAppearance ( presets [ 0 ] );
			person.gameObject.SetActive ( true );
			PersonBehavior behavior = person.GetComponent<PersonBehavior> ();
			if ( activeIndex != -1 )
			{
				Destroy ( activePeople [ activeIndex ].gameObject );
				activePeople [ activeIndex ] = behavior;
				spawnTimers [ activeIndex ] = Time.time + Random.Range ( 30f, 45f );
				
			} else
			{
				activePeople.Add ( behavior );
				spawnTimers.Add ( Time.time + Random.Range ( 30f, 45f ) );
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

	public void UseHero (bool hero, bool others)
	{
		useHeroPreset = hero;
		othersWithHero = others;
	}

	void OnPersonEndedPath (PersonBehavior person)
	{
		if ( person.transform == targetInstance )
		{
			SpawnPerson ();
		}
	}
}
