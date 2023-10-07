using System;
using System.Collections.Generic;
using System.Drawing;
using CriWare;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class CriAudioPlayer : AbstractSingleton<CriAudioPlayer>
{
    public int Radius => _radius;

    public Point Center => _center;

    private int _radius;
    private readonly Point _center;
}