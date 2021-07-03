using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.Events;

/**
 * Handles day and night shift and routine events that happen at a specific time in game.
 * @author Jiasheng Zhou
 */
public class TimeManager : MonoBehaviour
{
    /**
     * A routine event that starts between [startDay, endDay]
     */
    public class Routine
    {
        private int _startDay;
        private int _endDay;
        private UnityAction _routineEvent;

        public int StartDay
        {
            get => _startDay;
        }

        public int EndDay
        {
            get => _endDay;
        }

        public UnityAction RoutineEvent
        {
            get => _routineEvent;
        }

        public Routine(int startDay, int endDay, UnityAction routineEvent)
        {
            Debug.Assert(routineEvent != null);
            if (startDay >= 0 && endDay >= 0)
            {
                Debug.Assert(startDay <= endDay);
            }
            this._startDay = startDay;
            this._endDay = endDay;
            this._routineEvent = routineEvent;
        }
    }

    public float gameTimeRealTimeRatio = 120.0f;
    public Action<int, int, int> onTimeChange;
    public float startingTime = 21600.0f;
    public Light sunLight;
    public Light moonLight;

    public SortedDictionary<int, List<Routine>> routines;
    private const int kSecondsInDay = 24 * 60 * 60;

    private int _gameTime;
    private int _day;
    private int _hour;
    private int _minute;

    private static TimeManager _instance;

    public static TimeManager Instance
    {
        get => _instance;
    }

    public int GameTimeInDay
    {
        get => _gameTime % (24 * 60 * 60);
    }

    public int GameTime
    {
        get => _gameTime;
    }

    public int Day
    {
        get => _day;
    }

    public int Hour
    {
        get => _hour;
    }

    public int Minute
    {
        get => _minute;
    }

    // Start is called before the first frame update
    void Awake()
    {
        routines = new SortedDictionary<int, List<Routine>>();
        _instance = this;
    }

    public static int GetGameTime(int day, int hour, int minute)
    {
        int unit = 60;
        int startTime = 0;
        startTime += minute * unit;
        unit *= 60;
        startTime += hour * unit;
        unit *= 24;
        startTime += day * unit;
        return startTime;
    }

    public static int GetGameTimeInDay(int hour, int minute)
    {
        int unit = 60;
        int startTime = 0;
        startTime += minute * unit;
        unit *= 60;
        startTime += hour * unit;
        return startTime;
    }

    public void RegisterRoutine(int startDay, int endDay, int hour, int minute, UnityAction routineEvent)
    {
        int startTimeInDay = GetGameTimeInDay(hour, minute);
        if (!routines.ContainsKey(startTimeInDay)) 
        {
            routines[startTimeInDay] = new List<Routine>();
        }

        routines[startTimeInDay].Add(new Routine(startDay, endDay, routineEvent));
    }

    public void RegisterOnetimeRoutine(int day, int hour, int minute, UnityAction routineEvent) 
    {
        RegisterRoutine(day, day, hour, minute, routineEvent);
    }

    public void RegisterRoutineStartsAt(int startDay, int hour, int minute, UnityAction routineEvent)
    {
        RegisterRoutine(startDay, -1, hour, minute, routineEvent);
    }

    public void RegisterRoutineEndsAt(int endDay, int hour, int minute, UnityAction routineEvent)
    {
        RegisterRoutine(-1, endDay, hour, minute, routineEvent);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate game time
        _gameTime = (int)(gameTimeRealTimeRatio * Time.time + startingTime);
        _day = _gameTime / (24 * 60 * 60);
        _hour = (_gameTime % (24 * 60 * 60)) / (60 * 60);
        _minute = (_gameTime % (60 * 60)) / 60;

        if (onTimeChange != null)
        {
            onTimeChange(_day, _hour, _minute);
        }

        // Process routine events
        foreach (int startTimeInDay in routines.Keys)
        {
            int gameTimeInDay = GameTimeInDay;

            if (startTimeInDay <= gameTimeInDay)
            {
                List<Routine> routineList = routines[startTimeInDay];
                routineList.RemoveAll(
                    (routine) =>
                    {
                        bool expire = routine.EndDay >= 0 && Day > routine.EndDay;
                        bool shouldStart = routine.StartDay < 0 || Day >= routine.StartDay;

                        if (!expire && shouldStart)
                        {
                            routine.RoutineEvent();
                        }

                        return expire;
                    }
                );

                if (routineList.Count == 0)
                {
                    routines.Remove(startTimeInDay);
                }
            }
            else
            {
                break;
            }
        }

        // Day night shift
        sunLight.transform.rotation = Quaternion.Euler((270.0f + GameTimeInDay * 360.0f / kSecondsInDay) % 360.0f, 0.0f, 0.0f);
        moonLight.transform.rotation = Quaternion.Euler((90.0f + GameTimeInDay * 360.0f / kSecondsInDay) % 360.0f, 0.0f, 0.0f);
    }

    
}
