using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Stages

    public static Stage stage1;
    public static Stage stage2;
    public static Stage stage3;

    public class Stage
    {
        /// Lock = 0
        /// UnClear = 1
        /// Clear = 2
        public int isLock = 0;


    }

    #endregion
}
