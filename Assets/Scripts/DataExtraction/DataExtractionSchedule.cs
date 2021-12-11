using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

public class DataExtractionSchedule
{
    private PathSampleCompound m_compound;
    private int m_samplesToTake;
    private int m_samplesTaken;
    private string m_name;
    private List<Transform> m_nodesPatrol;
    private List<Transform> m_nodesHero;
    private List<Transform> m_nodesSpawn;
    private LineRenderer m_lineRendererPatrol;
    private LineRenderer m_lineRendererPatrolMain;
    private LineRenderer m_lineRendererHero;
    private LineRenderer m_lineRendererHeroMain;
    
    public DataExtractionSchedule( PathSample[] pPatrol,
                                   PathSample[] pHero,
                                   PathSample[] pSpawns,
                                   int pMode )
    {
        // Create the compound data that we will need to hold
        m_compound = new PathSampleCompound( pPatrol, pHero, pSpawns, pMode );
        // Initialize the samples counters to a good state
        m_samplesTaken = 0;
        m_samplesToTake = 1000;// this many frames to be taken using this schedule
        // name to something useful
        m_name = "default";
        // Create the holder for the references of the node
        m_nodesPatrol = new List<Transform>();
        m_nodesHero = new List<Transform>();
        m_nodesSpawn = new List<Transform>();
    }

    public int getNumFramesTaken() { return m_samplesTaken; }
    public void setName( string newName ) { m_name = m_compound.name = newName; }
    public string getName() { return m_name; }
    public PathSampleCompound compound() { return m_compound; }
    public void addNodeObjectPatrol( Transform node ) { m_nodesPatrol.Add( node ); }
    public void addNodeObjectHero( Transform node ) { m_nodesHero.Add( node ); }
    public void addNodeObjectSpawn( Transform node ) { m_nodesSpawn.Add( node ); }
    public List<Transform> getNodeObjectsPatrol() { return m_nodesPatrol; }
    public List<Transform> getNodeObjectsHero() { return m_nodesHero; }
    public List<Transform> getNodeObjectsSpawn() { return m_nodesSpawn; }
    public void setLineRendererPatrol( LineRenderer lineRenderer ) { m_lineRendererPatrol = lineRenderer; }
    public void setLineRendererPatrolMain( LineRenderer lineRenderer ) { m_lineRendererPatrolMain = lineRenderer; }
    public void setLineRendererHero( LineRenderer lineRenderer ) { m_lineRendererHero = lineRenderer; }
    public void setLineRendererHeroMain( LineRenderer lineRenderer ) { m_lineRendererHeroMain = lineRenderer; }
    public LineRenderer getLineRendererPatrol() { return m_lineRendererPatrol; }
    public LineRenderer getLineRendererPatrolMain() { return m_lineRendererPatrolMain; }
    public LineRenderer getLineRendererHero() { return m_lineRendererHero; }
    public LineRenderer getLineRendererHeroMain() { return m_lineRendererHeroMain; }

    public void appendToPathPatrol( Vector3 position, Quaternion orientation )
    {
        List<PathSample> _patrol = new List<PathSample>( m_compound.patrol );
        _patrol.Add( new PathSample( position, orientation, Time.time ) );

        m_compound.patrol = _patrol.ToArray();
    }

    public void appendToPathHero( Vector3 position, Quaternion orientation )
    {
        List<PathSample> _hero = new List<PathSample>( m_compound.hero );
        _hero.Add( new PathSample( position, orientation, Time.time ) );

        m_compound.hero = _hero.ToArray();
    }

    // Loads the current schedule for execution by ...
    // setting the correct variables in the current global scene
    public void loadForExecution()
    {
        // Clear necessary data
        m_samplesTaken = 0;
        // load samples into the according managers
        PatrolPathManager.FromSamples( m_compound.patrol );
        HeroPathManager.FromSamples( m_compound.hero );
        SpawnPointManager.FromSamples( m_compound.spawns );
    }

    public void onFrameSaved()
    {
        m_samplesTaken++;
    }

    public bool hasFinishedExecution()
    {
        return m_samplesTaken >= m_samplesToTake;
    }

    public bool isHalfWayThere()
    {
        return m_samplesTaken > ( m_samplesToTake / 2 );
    }

    public void tick()
    {
        if ( m_compound.mode == PathSampleCompound.MODE_FORCE_FOLLOW_DYNAMIC )
        {
            if ( QuadMotor.ActiveController != null )
            {
                QuadMotor.ActiveController.followDistance = 2.0f  + 2.0f * ( Mathf.Sin( 0.25f * Time.time ) + 1.0f );
                QuadMotor.ActiveController.followHeight = 2.0f + 2.0f * ( Mathf.Sin( 0.25f * Time.time ) + 1.0f );
            }
        }
        else if ( m_compound.mode == PathSampleCompound.MODE_FORCE_FOLLOW_FIXED )
        {
            if ( QuadMotor.ActiveController != null )
            {
                QuadMotor.ActiveController.followDistance = 2.0f;
                QuadMotor.ActiveController.followHeight = 2.0f;
            }
        }
        else if ( m_compound.mode == PathSampleCompound.MODE_FORCE_FOLLOW_FIXED_FAR )
        {
            if ( QuadMotor.ActiveController != null )
            {
                QuadMotor.ActiveController.followDistance = 8.0f;
                QuadMotor.ActiveController.followHeight = 8.0f;
            }
        }
    }

    public static DataExtractionSchedule fromFile( string location )
    {
        // Get the data back from the file
        string _schText = File.ReadAllText( location );
        PathSampleCompound _compound = JsonUtility.FromJson<PathSampleCompound>( _schText );
        // Make the schedule object
        DataExtractionSchedule _schedule = new DataExtractionSchedule( _compound.patrol,
                                                                       _compound.hero,
                                                                       _compound.spawns,
                                                                       _compound.mode );
        return _schedule;
    }

    public void toFile( string location, string basename )
    {
        // serialize to json
        string _schjson = JsonUtility.ToJson( m_compound );
        // make the fullname for this schedule
        string _filename = basename + "_" + m_name + ".json";
        // save to disk
        File.WriteAllText( System.IO.Path.Combine( location, _filename ), _schjson );
    }
}
