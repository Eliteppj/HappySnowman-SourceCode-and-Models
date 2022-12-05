using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UniData
{

    

    public enum PlayMode
    {
        TitleUI,
        PauseUI,
        SubUI,
        Gameplay
    }
    public enum ControlMode
    {
        _2D,
        _3D,
        _2DAxis,
        _2DAxisRaw,
        _3DAxis,
        _3DAxisRaw
    }

    public enum CharacterLevel
    {
        Lv1,
        Lv2,
        Lv3,
    }

    public struct GetRandomInfo
    {
        public int Length;
        public double[] array;
    }

    [Serializable]
    public class GMDATA_PlayerData
    {
        public int cookie;
        public int presentbox;
        public int stage;
        public Vector2 hp;

        [Header("PlayerKeySettings")]
        public KeyCode kForward = KeyCode.W;
        public KeyCode kBackward = KeyCode.S;
        public KeyCode kLeft = KeyCode.A;
        public KeyCode kRight = KeyCode.D;
        public KeyCode kJump = KeyCode.Space;
        public KeyCode kFire = KeyCode.LeftControl;
        private KeyCode kExitMenu = KeyCode.Escape;
        public KeyCode kInteraction = KeyCode.E;


        [Header("CharacterData")]
        public CharacterLevel characterLevel;

    }



}
