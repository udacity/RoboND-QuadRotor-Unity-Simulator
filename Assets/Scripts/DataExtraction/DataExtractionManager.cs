using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

public class DataExtractionManager : MonoBehaviour 
{
    // Singleton for global access
    public static DataExtractionManager INSTANCE;

    // public editor-accesible properties *******
    public LineRenderer linePrefabMain;
    public LineRenderer linePrefab;
    public Transform nodePrefabPatrol;
    public Transform nodePrefabHero;
    public Transform nodePrefabSpawn;

    // private properties ***********************
    private int m_currentExtractionIndx;
    private int m_numSchedulesFinished;
    private int m_numSchedulesToRun;
    private bool m_isExtractionRunning;
    private bool m_isExtractionTypeBatch;
    private string m_batchsLocation;
    private int m_currentExtractionMode;
    private IList<DataExtractionSchedule> m_schedules;

    private bool m_isEditionEnabled;

	// Use this for initialization
	void Awake() 
    {
        DataExtractionManager.INSTANCE = this;
        // Initialize some stuff
        m_currentExtractionMode = PathSampleCompound.MODE_FREE;
        m_currentExtractionIndx = -1;
        m_numSchedulesFinished = 0;
        m_numSchedulesToRun = 0;
        m_isExtractionRunning = false;
        m_isExtractionTypeBatch = false;
        m_isEditionEnabled = false;
        m_batchsLocation = "";
        m_schedules = new List<DataExtractionSchedule>();
        // try to load the schedules from default location
		// loadDataSchedules( false );
	}

    public int currentExtractionIndx() { return m_currentExtractionIndx; }
    public int numSchedulesFinished() { return m_numSchedulesFinished; }
    public int numSchedulesToRun() { return m_numSchedulesToRun; }
    public bool isExtractionRunning() { return m_isExtractionRunning; }
    public bool isExtractionTypeBatch() { return m_isExtractionTypeBatch; }
    public int currentExtractionMode() { return m_currentExtractionMode; }
    public bool isEditionEnabled() { return m_isEditionEnabled; }

    public void editionAddPatrolNode( Vector3 position, Quaternion orientation ) 
    {
        if ( !validateExtracting() ) { return; }
        if ( !validateCurrentSchedule() ) { return; }

        var _schedule = m_schedules[ m_currentExtractionIndx ];
        Transform _nodePatrol = Instantiate( nodePrefabPatrol,
                                             position,
                                             orientation,
                                             transform );
        _nodePatrol.localScale = new Vector3( 0.5f, 0.5f, 0.5f );
        _schedule.addNodeObjectPatrol( _nodePatrol );
        _schedule.appendToPathPatrol( position, orientation );

        _updateScheduleLinesViz( _schedule, true );
    }

    public void editionAddHeroNode( Vector3 position, Quaternion orientation )
    {
        if ( !validateExtracting() ) { return; }
        if ( !validateCurrentSchedule() ) { return; }

        var _schedule = m_schedules[ m_currentExtractionIndx ];
        Transform _nodeHero = Instantiate( nodePrefabHero,
                                           position,
                                           orientation,
                                           transform );
        _nodeHero.localScale = new Vector3( 0.5f, 0.5f, 0.5f );
        _schedule.addNodeObjectHero( _nodeHero );
        _schedule.appendToPathHero( position, orientation );

        _updateScheduleLinesViz( _schedule, true );
    }

    public void editionAddSpawnNode( Vector3 position, Quaternion orientation )
    {
        if ( !validateExtracting() ) { return; }
        if ( !validateCurrentSchedule() ) { return; }

        var _schedule = m_schedules[ m_currentExtractionIndx ];
        Transform _nodeSpawn = Instantiate( nodePrefabSpawn,
                                            position,
                                            orientation,
                                            transform );
        _nodeSpawn.localScale = new Vector3( 0.5f, 0.5f, 0.5f );
        _schedule.addNodeObjectSpawn( _nodeSpawn );

        _updateScheduleLinesViz( _schedule, true );
    }

    public void editionRemovePatrolNode()
    {
        if ( !validateExtracting() ) { return; }
        if ( !validateCurrentSchedule() ) { return; }
        
       var _schedule = m_schedules[ m_currentExtractionIndx ];
       _schedule.getNodeObjectsPatrol().RemoveAt( _schedule.getNodeObjectsPatrol().Count - 1 );

       _updateScheduleLinesViz( _schedule, true );
    }

    public void editionRemoveHeroNode()
    {
        if ( !validateExtracting() ) { return; }
        if ( !validateCurrentSchedule() ) { return; }

       var _schedule = m_schedules[ m_currentExtractionIndx ];
       _schedule.getNodeObjectsHero().RemoveAt( _schedule.getNodeObjectsHero().Count - 1 );

       _updateScheduleLinesViz( _schedule, true );
    }

    public void editionRemoveSpawnNode()
    {
        if ( !validateExtracting() ) { return; }
        if ( !validateCurrentSchedule() ) { return; }

       var _schedule = m_schedules[ m_currentExtractionIndx ];
       _schedule.getNodeObjectsSpawn().RemoveAt( _schedule.getNodeObjectsSpawn().Count - 1 );

       _updateScheduleLinesViz( _schedule, true );
    }

    public bool validateExtracting( bool useLogs = true )
    {
        if ( m_isExtractionRunning )
        {
            if ( useLogs )
            {
                Debug.Log("LOG-validateExtracting> there is an extraction " +
                        "running, wait till it finishes");
            }
            return false;
        }

        return true;
    }

    public bool validateCurrentSchedule( bool useLogs = true )
    {
        if ( m_schedules.Count < 1 )
        {
            if ( useLogs )
            {
                Debug.Log( "LOG-validateCurrentSchedule> there are no schedules" );
            }
            return false;
        }
        if ( m_currentExtractionIndx < 0 || 
             m_currentExtractionIndx >= m_schedules.Count )
        {
            if ( useLogs )
            {
                Debug.Log( "LOG-validateCurrentSchedule> there is no valid current schedule" );
            }
            return false;
        }

        return true;
    }

    private void saveDataSchedules( bool requestPath = false )
    {
        if ( !validateExtracting() ) { return; }
        if ( !validateCurrentSchedule() ) { return; }

        if ( requestPath )
        {
            SimpleFileBrowser.ShowSaveDialog( _onSaveSchedules,
                                              null,
                                              true,
                                              null,
                                              "Select location to save the schedules",
                                              "Select" );
        }
        else
        {
            _onSaveSchedules( "./schedules/" );
        }
    }

    private void _onSaveSchedules( string location )
    {
        if ( !Directory.Exists( location ) )
        {
            Debug.Log( "LOG> The requested path does not exist: " + location );
            return;
        }

        for ( int q = 0; q < m_schedules.Count; q++ )
        {
            m_schedules[q].toFile( location, "sv_schedule" );
        }
    }

    // Load the paths into the corresponding ...
    // types of data extraction schedule
    private void loadDataSchedules( bool requestPath = false )
    {
        if ( !validateExtracting() ) { return; }

        if ( requestPath )
        {
            // Open dialog to find the folder where the schedules are
            SimpleFileBrowser.ShowLoadDialog( _onLoadSchedules, 
                                              null, 
                                              true, 
                                              null, 
                                              "Select folder with schedules", 
                                              "Select" );
        }
        else
        {
            // Just try to load from the default location
            _onLoadSchedules( "./schedules/" );
        }
    }

    private void _onLoadSchedules( string location )
    {
        if ( !Directory.Exists( location ) )
        {
            Debug.Log( "LOG> The requested path does not exist: " + location );
            return;
        }

        if ( _loadAllSchedules( location ) )
        {
            Debug.Log( "LOG> There seems to be no schedule files " + 
                       "inside this folder : " + location );
        }
    }

    private bool _loadAllSchedules( string location )
    {
        DirectoryInfo _dinfo = new DirectoryInfo( location );
        FileInfo[] _files = _dinfo.GetFiles( "*.json" );

        bool _success = false;

        foreach ( FileInfo _file in _files )
        {
            string _flocation = System.IO.Path.Combine( location, _file.Name );
            _loadSingleSchedule( _flocation );
        }

        return _success;
    }

    private void _loadSingleSchedule( string location )
    {
        Debug.Log( "LOG-_loadSingleSchedule> loading a schedule from location : " + location );

        // Get the data back from the file
        string _schText = File.ReadAllText( location );
        PathSampleCompound _compound = JsonUtility.FromJson<PathSampleCompound>( _schText );

        // Create the schedule
        addSchedule( ( PathSample[] ) _compound.patrol.Clone(),
                     ( PathSample[] ) _compound.hero.Clone(),
                     ( PathSample[] ) _compound.spawns.Clone(),
                     _compound.mode,
                     _compound.name );
        
        // Set an initial current index
        if ( m_currentExtractionIndx == -1 )
        {
            m_currentExtractionIndx = 0;
            changeCurrentScheduleViz( true );
        }
    }

    public void _hideSchedules()
    {
        for ( int q = 0; q < m_schedules.Count; q++ )
        {
            m_schedules[q].getLineRendererHero().positionCount = 0;
            m_schedules[q].getLineRendererHeroMain().positionCount = 0;
            m_schedules[q].getLineRendererPatrol().positionCount = 0;
            m_schedules[q].getLineRendererPatrolMain().positionCount = 0;

            var _nodesPatrol = m_schedules[q].getNodeObjectsPatrol();
            var _nodesHero = m_schedules[q].getNodeObjectsHero();
            var _nodeSpawn = m_schedules[q].getNodeObjectsSpawn();

            foreach ( var _node in _nodesPatrol ) 
            { _node.localScale = new Vector3( 0.01f, 0.01f, 0.01f ); }
            foreach ( var _node in _nodesHero ) 
            { _node.localScale = new Vector3( 0.01f, 0.01f, 0.01f ); }
            foreach ( var _node in _nodeSpawn ) 
            { _node.localScale = new Vector3( 0.01f, 0.01f, 0.01f ); }
        }
    }

    public void _showSchedules()
    {
        for ( int q = 0; q < m_schedules.Count; q++ )
        {
            _updateScheduleLinesViz( m_schedules[q], 
                                     q == m_currentExtractionIndx );

            var _nodesPatrol = m_schedules[q].getNodeObjectsPatrol();
            var _nodesHero = m_schedules[q].getNodeObjectsHero();
            var _nodeSpawn = m_schedules[q].getNodeObjectsSpawn();

            float _size = ( q == m_currentExtractionIndx ) ? 0.5f : 0.25f;

            foreach ( var _node in _nodesPatrol ) 
            { _node.localScale = new Vector3( _size, _size, _size ); }
            foreach ( var _node in _nodesHero ) 
            { _node.localScale = new Vector3( _size, _size, _size ); }
            foreach ( var _node in _nodeSpawn ) 
            { _node.localScale = new Vector3( _size, _size, _size ); }
        }
    }

    public void beginExtraction()
    {
        if ( !validateExtracting() ) { return; }
        if ( !validateCurrentSchedule() ) { return; }

        Debug.Log( "LOG-beginExtraction> starting extraction schedule" );

        // Ask user for save location to this place
        SimpleFileBrowser.ShowSaveDialog( onRequestBatchRecording, 
                                          null, true, null, 
                                          "Select Output Folder", "Select" );
    }

    public void onRequestBatchRecording( string location )
    {
        m_isExtractionRunning = true;
        m_isExtractionTypeBatch = true;
        m_isEditionEnabled = false;
        m_batchsLocation = location;
        m_numSchedulesFinished = 0;
        m_numSchedulesToRun = m_schedules.Count;

        extractFromSingleNoUser( m_schedules[ m_currentExtractionIndx ] );
    }

    public void extractFromSingleNoUser( DataExtractionSchedule schedule )
    {
        // Make path to the folder in which we will store the images for this schedule
        string _dirResource = System.IO.Path.Combine( m_batchsLocation, 
                                                      schedule.getName() );

        // Make extraction directory for this schedule
        if ( !Directory.Exists( _dirResource ) )
        {
            Directory.CreateDirectory( _dirResource );
        }
        onBeginRecordingSingle();
        RecordingController.INSTANCE.forceSaveLocation( _dirResource );
        RecordingController.INSTANCE.OnBeginRecord();
    }

    public void onSingleBatchRecordingEnded()
    {
        m_numSchedulesFinished++;
        if ( m_numSchedulesFinished >= m_numSchedulesToRun )
        {
            onFullExtractionEnded();
        }
        else
        {
            m_currentExtractionIndx = ( m_currentExtractionIndx + 1 ) % m_schedules.Count;
            extractFromSingleNoUser( m_schedules[ m_currentExtractionIndx ] );
        }
    }

    private void onFullExtractionEnded()
    {
        m_isExtractionRunning = false;
        m_isExtractionTypeBatch = false;
    }

    public void extractFromCurrentSchedule()
    {
        if ( !m_isExtractionRunning )
        {
            // Make sure no events are receiving while recording
            m_isExtractionRunning = true;

            // Reuse the logic of the recording controller
            if ( RecordingController.INSTANCE.CheckSaveLocation() )
            {
                RecordingController.INSTANCE.OnBeginRecord();
            }
        }
        else
        {
            RecordingController.INSTANCE.OnEndRecord();
        }
    }

    public void onFrameSaved()
    {
        m_schedules[ m_currentExtractionIndx ].onFrameSaved();
        if ( m_schedules[ m_currentExtractionIndx ].hasFinishedExecution() )
        {
            // Stop current recording to force some callbacks
            RecordingController.INSTANCE.ToggleRecording();
            // Notify that the recording has ended
            onSingleBatchRecordingEnded();
        }
    }

    public void onBeginRecordingSingle()
    {
        // hide everything to focus only on the actual plan
        _hideSchedules();
        // Remove temporal paths
        PatrolPathManager.Clear( false );
        HeroPathManager.Clear( false );
        SpawnPointManager.Clear( false );
        // load current schedule
        m_schedules[ m_currentExtractionIndx ].loadForExecution();

        SimpleQuadController.ActiveController.stateController.SetState( "Patrol" );
        PeopleSpawner.instance.gameObject.SetActive( true );
    }

    public void onCancelRecordingSingle()
    {
        m_isExtractionRunning = false;
        _showSchedules();

        SimpleQuadController.ActiveController.stateController.SetState( "Local" );
    }

    public void onEndRecordingSingle()
    {
        m_isExtractionRunning = false;
        _showSchedules();

        PeopleSpawner.instance.gameObject.SetActive( false );

        PatrolPathManager.Clear( true );
        HeroPathManager.Clear( true );
        SpawnPointManager.Clear( true );

        SimpleQuadController.ActiveController.stateController.SetState( "Local" );
    }

    public void changeCurrentScheduleViz( bool isMain )
    {
        if ( !validateExtracting( false ) ) { return; }
        if ( !validateCurrentSchedule( false ) ) { return; }

        // Change the sizes of the nodes to make it look bigger
        var _schedule = m_schedules[ m_currentExtractionIndx ];
        float _size = isMain ? 0.5f : 0.25f;
        for ( int q = 0; q < _schedule.getNodeObjectsPatrol().Count; q++ )
        {
            _schedule.getNodeObjectsPatrol()[q].localScale = new Vector3( _size, _size, _size );
        }
        for ( int q = 0; q < _schedule.getNodeObjectsHero().Count; q++ )
        {
            _schedule.getNodeObjectsHero()[q].localScale = new Vector3( _size, _size, _size );
        }
        for ( int q = 0; q < _schedule.getNodeObjectsSpawn().Count; q++ )
        {
            _schedule.getNodeObjectsSpawn()[q].localScale = new Vector3( _size, _size, _size );
        }
        // Change the lines' visualization as well
        _updateScheduleLinesViz( _schedule, isMain );
    }

    public void addSchedule( PathSample[] pPatrol,
                             PathSample[] pHero,
                             PathSample[] pSpawns,
                             int mode,
                             string name )
    {
        if ( m_isExtractionRunning )
        {
            Debug.Log( "LOG-addSchedule> there is an extraction running, wait till it finishes" );
            return;
        }

        DataExtractionSchedule _schedule = new DataExtractionSchedule( pPatrol,
                                                                       pHero,
                                                                       pSpawns,
                                                                       mode );
        // create patrol linerenderer and assign it to this schedule
        LineRenderer _lineRendererPatrol = Instantiate( linePrefab, transform );
        _lineRendererPatrol.positionCount = 0;
        LineRenderer _lineRendererPatrolMain = Instantiate( linePrefabMain, transform );
        _lineRendererPatrolMain.positionCount = 0;
        _schedule.setLineRendererPatrol( _lineRendererPatrol );
        _schedule.setLineRendererPatrolMain( _lineRendererPatrolMain );
        // create hero linerenderer and assign it to this schedule
        LineRenderer _lineRendererHero = Instantiate( linePrefab, transform );
        _lineRendererHero.positionCount = 0;
        LineRenderer _lineRendererHeroMain = Instantiate( linePrefabMain, transform );
        _lineRendererHeroMain.positionCount = 0;
        _schedule.setLineRendererHero( _lineRendererHero );
        _schedule.setLineRendererHeroMain( _lineRendererHeroMain );
        // Add schedule to list of references
        m_schedules.Add( _schedule );
        _makeScheduleViz( _schedule );
        // set a name with a default timestamp
        _schedule.setName( name );
    }

    // Update is called once per frame
    void Update() 
    {
        if ( !m_isExtractionRunning )
        {
            if ( Input.GetKeyDown( KeyCode.Home ) )
            {
                if ( m_schedules.Count > 0 )
                {
                    // Make previous current schedule to change its visuals
                    changeCurrentScheduleViz( false );
                    m_currentExtractionIndx++;
                    m_currentExtractionIndx = ( m_currentExtractionIndx > ( m_schedules.Count - 1 ) ) ? 
                                                        ( m_schedules.Count - 1 ) : m_currentExtractionIndx;
                    changeCurrentScheduleViz( true );
                }
            }
            if ( Input.GetKeyDown( KeyCode.End ) )
            {
                if ( m_schedules.Count > 0 )
                {
                    changeCurrentScheduleViz( false );
                    m_currentExtractionIndx--;
                    m_currentExtractionIndx = ( m_currentExtractionIndx < 0 ) ? 
                                                        0 : m_currentExtractionIndx;
                    changeCurrentScheduleViz( true );
                }
            }
            if ( Input.GetKeyDown( KeyCode.Z ) )
            {
                loadDataSchedules( true );
            }
            if ( Input.GetKeyDown( KeyCode.X ) )
            {
                saveDataSchedules( true );
            }
            if ( Input.GetKeyDown( KeyCode.U ) )
            {
                beginExtraction();
            }
            if ( Input.GetKeyDown( KeyCode.V ) )
            {
                extractFromCurrentSchedule();
            }
            if ( Input.GetKeyDown( KeyCode.PageDown ) )
            {
                // loadDataSchedules( false );
                if ( validateExtracting() && validateCurrentSchedule() )
                {
                    m_isEditionEnabled = !m_isEditionEnabled;
                }
            }
            if ( Input.GetKeyDown( KeyCode.Delete ) )
            {
                _removeCurrentSchedule();
            }
            if ( Input.GetKeyDown( KeyCode.Insert ) )
            {
                _addNewScheduleFromManagers();
            }
            if ( Input.GetKeyDown( KeyCode.Return ) )
            {
                m_currentExtractionMode = ( m_currentExtractionMode == PathSampleCompound.MODE_FREE ) ? 
                                                    PathSampleCompound.MODE_FORCE_FOLLOW : 
                                                    PathSampleCompound.MODE_FREE;
            }
        }
	}

    private void _removeCurrentSchedule()
    {
        if ( m_schedules.Count < 1 )
        {
            return;
        }

        // Clear visualization
        var _schedule = m_schedules[ m_currentExtractionIndx ];
        _schedule.getLineRendererPatrol().positionCount = 0;
        _schedule.getLineRendererPatrolMain().positionCount = 0;
        _schedule.getLineRendererHero().positionCount = 0;
        _schedule.getLineRendererHeroMain().positionCount = 0;
        for ( int q = 0; q < _schedule.getNodeObjectsPatrol().Count; q++ )
        {
            Destroy( _schedule.getNodeObjectsPatrol()[q].gameObject );
        }
        _schedule.getNodeObjectsPatrol().Clear();
        for ( int q = 0; q < _schedule.getNodeObjectsHero().Count; q++ )
        {
            Destroy( _schedule.getNodeObjectsHero()[q].gameObject );
        }
        _schedule.getNodeObjectsHero().Clear();
        for ( int q = 0; q < _schedule.getNodeObjectsSpawn().Count; q++ )
        {
            Destroy( _schedule.getNodeObjectsSpawn()[q].gameObject );
        }
        _schedule.getNodeObjectsSpawn().Clear();
        
        // remove this schedule from the list of schedules
        m_schedules.RemoveAt( m_currentExtractionIndx );
        // go to the previous one ( if there is one )
        if ( m_schedules.Count > 0 )
        {
            m_currentExtractionIndx--;
            m_currentExtractionIndx = ( m_currentExtractionIndx < 0 ) ? 
                                                0 : m_currentExtractionIndx;
            changeCurrentScheduleViz( true );
        }
        else
        {
            m_currentExtractionIndx = -1;
        }
    }

    private void _addNewScheduleFromManagers()
    {
        if ( PatrolPathManager.GetPath().Length < 2 ||
             SpawnPointManager.GetPath().Length < 1 ||
             HeroPathManager.GetPath().Length < 2 )
        {
            return;
        }

        // Copy the temporal schedule from the managers ...
        // into an appropiate schedule object
        addSchedule( ( PathSample[] ) PatrolPathManager.GetPath().Clone(),
                     ( PathSample[] ) HeroPathManager.GetPath().Clone(),
                     ( PathSample[] ) SpawnPointManager.GetPath().Clone(),
                     m_currentExtractionMode,
                     "default" + _createTimeStampId() );

        // Clear the temporal path from the managers
        PatrolPathManager.Clear( true );
        HeroPathManager.Clear( true );
        SpawnPointManager.Clear( true );

        if ( m_currentExtractionIndx == -1 )
        {
            m_currentExtractionIndx = 0;
            changeCurrentScheduleViz( true );
        }
    }

    public string _createTimeStampId()
    {
        string _sDateStamp = "_" + 
                             System.DateTime.Now.Day.ToString() +
                             System.DateTime.Now.Month.ToString() +
                             System.DateTime.Now.Hour.ToString() +
                             System.DateTime.Now.Minute.ToString() +
                             System.DateTime.Now.Second.ToString() + "_";
        
        return _sDateStamp;
    }

    void OnGUI()
    {
        _drawTextInfo();
        _drawScheduleNameEditor();
    }

    void _drawTextInfo()
    {
        string _smode = ( m_currentExtractionMode == PathSampleCompound.MODE_FREE ) ? "FREE" : "FORCE";
        string _sinfo = @"Number of schedules: " + ( m_schedules.Count.ToString() ) + "\n" +
                         "Current schedule indx: " + ( m_currentExtractionIndx.ToString() ) + "\n" +
                         "Current mode : " + _smode + "\n" +
                         ( m_isEditionEnabled ? "EDITION ENABLED!!!" : "Edition disabled" );
        if ( m_schedules.Count > 0 && 
             m_currentExtractionIndx >= 0 && 
             m_currentExtractionIndx < m_schedules.Count )
        {
            _sinfo += @"Patrol points in current schedule : " + ( m_schedules[ m_currentExtractionIndx ].compound().patrol.Length ) + "\n" +
                       "Hero points in current schedule : " + ( m_schedules[ m_currentExtractionIndx ].compound().hero.Length ) + "\n" +
                       "Spawn points in current schedule : " + ( m_schedules[ m_currentExtractionIndx ].compound().spawns.Length ) + "\n" +
                       "Current schedule name : " + m_schedules[ m_currentExtractionIndx ].getName();
        }

        Vector2 _size;
        GUIStyle _label;
        TextClipping _clipping;
        Rect _r;
        bool _wwrap;
        int _fontSize;

        _label = GUI.skin.label;
        _clipping = _label.clipping;
        _wwrap = _label.wordWrap;
        _fontSize = _label.fontSize;

        _label.fontSize = ( int )( 32f * Screen.height / 1080 );
        _label.wordWrap = false;
        _label.clipping = TextClipping.Overflow;
        GUI.color = ( m_isEditionEnabled ) ? Color.red : Color.white;

        _size = _label.CalcSize( new GUIContent( _sinfo ) );
        _r = new Rect( 100, Screen.height - _size.y - 20, _size.x, _size.y + 10 );
        GUI.Box( _r, "" );
        GUI.Box( _r, "" );
        _r.x += 5;

        GUILayout.BeginArea( _r );
        GUILayout.Label( _sinfo );
        GUILayout.EndArea();

        _label.clipping = _clipping;
        _label.wordWrap = _wwrap;
        _label.fontSize = _fontSize;
    }

    private void _drawScheduleNameEditor()
    {
        if ( !validateExtracting( false ) ) { return; }
        if ( !validateCurrentSchedule( false ) ) { return; }

        if ( m_isEditionEnabled )
        {
            string _nname = GUI.TextField( new Rect( 10, 10, 200, 20 ), 
                                           m_schedules[ m_currentExtractionIndx ].getName(),
                                           25 );
            m_schedules[ m_currentExtractionIndx ].setName( _nname );
        }
    }

    private void _updateScheduleLinesViz( DataExtractionSchedule schedule, bool isMain )
    {
        // clear all
        schedule.getLineRendererHero().positionCount = 0;
        schedule.getLineRendererHeroMain().positionCount = 0;
        schedule.getLineRendererPatrol().positionCount = 0;
        schedule.getLineRendererPatrolMain().positionCount = 0;

        // get the appropiate renderer
        LineRenderer _lineRendererHero = isMain ? 
                                            schedule.getLineRendererHeroMain() : 
                                            schedule.getLineRendererHero();
        LineRenderer _lineRendererPatrol = isMain ? 
                                            schedule.getLineRendererPatrolMain() :
                                            schedule.getLineRendererPatrol();
        
        // update the schedule's visualization
        for ( int q = 0; q < schedule.compound().patrol.Length; q++ )
        {
            _lineRendererPatrol.positionCount++;
            _lineRendererPatrol.SetPosition( _lineRendererPatrol.positionCount - 1,
                                             schedule.compound().patrol[q].position );
        }
        for ( int q = 0; q < schedule.compound().hero.Length; q++ )
        {
            _lineRendererHero.positionCount++;
            _lineRendererHero.SetPosition( _lineRendererHero.positionCount - 1,
                                           schedule.compound().hero[q].position );
        }
    }

    private void _makeScheduleViz( DataExtractionSchedule schedule )
    {
        // Make the patrol visualization
        for ( int q = 0; q < schedule.compound().patrol.Length; q++ )
        {
            // lines viz
            schedule.getLineRendererPatrol().positionCount++;
            schedule.getLineRendererPatrol().SetPosition( schedule.getLineRendererPatrol().positionCount - 1,
                                                          schedule.compound().patrol[q].position );
            // node viz
            Transform _nodePatrol = Instantiate( nodePrefabPatrol,
                                                 schedule.compound().patrol[q].position,
                                                 schedule.compound().patrol[q].orientation,
                                                 transform );
            _nodePatrol.localScale = new Vector3( 0.5f, 0.5f, 0.5f );
            schedule.addNodeObjectPatrol( _nodePatrol );
        }

        // Make the spawns visualization
        for ( int q = 0; q < schedule.compound().spawns.Length; q++ )
        {
            // node viz
            Transform _nodeSpawn = Instantiate( nodePrefabSpawn,
                                                schedule.compound().spawns[q].position,
                                                schedule.compound().spawns[q].orientation,
                                                transform );
            _nodeSpawn.localScale = new Vector3( 0.5f, 0.5f, 0.5f );
            schedule.addNodeObjectSpawn( _nodeSpawn );
        }

        // Make the hero visualization
        for ( int q = 0; q < schedule.compound().hero.Length; q++ )
        {
            // lines viz
            schedule.getLineRendererHero().positionCount++;
            schedule.getLineRendererHero().SetPosition( schedule.getLineRendererHero().positionCount - 1,
                                                        schedule.compound().hero[q].position );
            // node viz
            Transform _nodeHero = Instantiate( nodePrefabHero,
                                               schedule.compound().hero[q].position,
                                               schedule.compound().hero[q].orientation,
                                               transform );
            _nodeHero.localScale = new Vector3( 0.5f, 0.5f, 0.5f );
            schedule.addNodeObjectHero( _nodeHero );
        }
    }
}
