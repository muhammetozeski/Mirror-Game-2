using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO: this script came from "Unity particle pack". detect that if this script is necessary. if not, delete it.
/// </summary>
    [CreateAssetMenu]
    public class RampAsset : ScriptableObject
    {
        public Gradient gradient = new Gradient();
        public int size = 16;
        public bool up = false;
        public bool overwriteExisting = true;
    }
