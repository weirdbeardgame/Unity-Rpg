using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menu
{

    enum menuUse { OPEN, USE };
    enum menuType { MAIN, SUB };

    public interface IMenu
    {
        void Open();

        void SetShader(Material m);
        Material GetShader();

    }
}