﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Definitions
{
    // Animations
    public const float SMOOTH_TIME = 0.25f;
    public const float SMOOTH_DISTANCE = 0.01f;
    public const float SMOOTH_HEIGHT = 0.5f;
    public const float MAX_DEGREES_DELTA = 2.5f;

    //Rules
    public const int PGS_TO_WIN = 15;

    // Prices
    public const int PRECIO_COMPRA_CARTAS = 3000;
    public const int PRECIO_VENTA_CARTAS = 1500;
    public const int CANTIDAD_A_PERDER_MULTA = 2000;
    public const int PRECIO_COMPRA_PGS = 10000;
    public const int PRECIO_COMPRA_ARTESANAL = 6000;
    public const int PRECIO_COMPRA_ARRASTRE = 12000;
    public const int CANTIDAD_A_RECIBIR_SALIDA = 2000;

    // UI
    public const float NEWTURN_PANEL_BACKGROUND_OPACITY = 0.5f;

    // Camera
    public const float CAMERA_SPEED = 2.0f;
    public static Vector3 CAMERA_POSITION = new Vector3(4.5f, 9.0f, 5.5f);
    public static Quaternion CAMERA_ROTATION = Quaternion.Euler(90f, 0, 90f);

    // Lang
    public const string TRANSLATIONS_FILENAME = "strings";
    public const string TRANSLATIONS_FILEEXT = "txt";
    public const string ACCEPTED_LANGUAGES = "ES;EN";
    public const string DEFAULT_LANGUAGE = "ES";
}
