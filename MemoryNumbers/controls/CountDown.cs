﻿using System;
using System.ComponentModel;
using System.Timers;

namespace Controls;

[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
public partial class CountDown: Controls.RoundButton
{
    private readonly System.Timers.Timer t;
    private float _fStart = 0f;
    private float _fEnd = 0f;
    private double _dInterval = 1000; // 1 s
    private double _dCounter;
    private bool _sound;
    private readonly System.Media.SoundPlayer _soundPlayer;

    public event EventHandler<TimerEndingEventArgs> TimerEnding;

    #region Public properties
    /// <summary>
    /// Starting time (typically in seconds) of the countdown
    /// </summary>
    [Description("Starting time (typically in seconds) of the countdown"),
    Category("Countdown properties"),
    Browsable(true),
    EditorBrowsable(EditorBrowsableState.Always),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float StartingTime
    {
        get { return _fStart; }
        set { _fStart = value < 0 ? 0f : value; _dCounter = _fStart; this.Text = _fStart.ToString(); }
    }

    /// <summary>
    /// Ending time (typically in seconds) of the countdown
    /// </summary>
    [Description("Ending time (typically in seconds) of the countdown"),
    Category("Countdown properties"),
    Browsable(true),
    EditorBrowsable(EditorBrowsableState.Always),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float EndingTime
    {
        get { return _fEnd; }
        set { _fEnd = value < 0 ? 0f : value; }
    }

    /// <summary>
    /// Time interval of the countdown in seconds
    /// </summary>
    [Description("Time interval of the countdown in seconds. Typically it would be 1 second."),
    Category("Countdown properties"),
    Browsable(true),
    EditorBrowsable(EditorBrowsableState.Always),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double TimeInterval
    {
        get { return _dInterval; }
        set { _dInterval = value * 1000 < 0 ? 0f : value; t.Interval = _dInterval; }
    }

    /// <summary>
    /// Specifies whether game sounds are played (true) or not (false) during the countdown
    /// </summary>
    [Description("Specifies whether game sounds are played (true) or not (false)"),
    Category("Countdown properties"),
    Browsable(true),
    EditorBrowsable(EditorBrowsableState.Always),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool PlaySounds
    {
        get { return _sound; }
        set { _sound = value; }
    }

    #endregion Public properties

    public CountDown()
    {
        InitializeComponent();

        // Timer initialization
        t = new System.Timers.Timer();
        t.Elapsed += OnTimeEvent;

        string _path = System.IO.Path.GetDirectoryName(Environment.ProcessPath);
        _soundPlayer = System.IO.File.Exists(_path + @"\audio\Count down.wav") ? new(_path + @"\audio\Count down.wav") : null;
    }

    private void OnTimeEvent(object sender, ElapsedEventArgs e)
    {
        _dCounter -= _dInterval / 1000;

        if (_dCounter <= _fEnd)
        {
            //t.Stop();
            //System.Diagnostics.Debug.Write(_dCounter.ToString() + System.Environment.NewLine);
            if (TimerEnding is not null) OnTimerEnding(this, new TimerEndingEventArgs());
            t.Stop();
        }
        else
        {
            if (_sound == true && _soundPlayer is not null) _soundPlayer.Play();
            if (this.InvokeRequired)
            {
                Invoke(new Action(() =>
                    {
                        this.Text = _dCounter.ToString();
                    //roundButton1.Text = _dCounter.ToString();
                }));
            }
            else
            {
                this.Text = _dCounter.ToString();
            }
            this.Invalidate();
        }
    }

    public void Start()
    {
        t.Start();
        if (_sound == true && _soundPlayer is not null) _soundPlayer.Play();
        return;
    }

    public void Stop()
    {
        t.Stop();
        return;
    }

    // Events
    protected virtual void OnTimerEnding(object sender, TimerEndingEventArgs e)
    {
        TimerEnding?.Invoke(this, e);
    }
}

/// <summary>
/// Class to send the event data to the "listener"
/// </summary>
public class TimerEndingEventArgs : EventArgs
{
    /*public readonly bool Flash;
    public readonly Int32 ButtonValue;
    public TimerEndingEventArgs(bool flash, Int32 button)
    {
        Flash = flash;
        ButtonValue = button;
    }*/
}
