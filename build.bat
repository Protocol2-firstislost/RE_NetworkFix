@echo off
set CSC="C:\Program Files\dotnet\sdk\10.0.201\Roslyn\bincore\csc.exe"
set LIBS=libs
set OUT=RepoNetworkFix.dll

echo Building REPO Masterpiece v1.8.0...

%CSC% /target:library /out:%OUT% /nologo ^
/langversion:latest ^
/reference:%LIBS%\BepInEx.dll ^
/reference:%LIBS%\0Harmony.dll ^
/reference:%LIBS%\Facepunch.Steamworks.Win64.dll ^
/reference:%LIBS%\Assembly-CSharp.dll ^
/reference:%LIBS%\UnityEngine.dll ^
/reference:%LIBS%\UnityEngine.CoreModule.dll ^
/reference:%LIBS%\UnityEngine.IMGUIModule.dll ^
/reference:%LIBS%\UnityEngine.UIModule.dll ^
/reference:%LIBS%\UnityEngine.TextRenderingModule.dll ^
/reference:%LIBS%\UnityEngine.PhysicsModule.dll ^
/reference:%LIBS%\UnityEngine.AnimationModule.dll ^
/reference:%LIBS%\PhotonUnityNetworking.dll ^
/reference:%LIBS%\PhotonRealtime.dll ^
/reference:%LIBS%\Photon3Unity3D.dll ^
/reference:%LIBS%\mscorlib.dll ^
/reference:%LIBS%\System.dll ^
/reference:%LIBS%\System.Core.dll ^
/reference:%LIBS%\netstandard.dll ^
Plugin.cs

if %errorlevel% equ 0 (
    echo [SUCCESS] Mod v1.8.0 successfully compiled.
    copy /y %OUT% "..\REPO\BepInEx\plugins\%OUT%"
    echo [SUCCESS] Mod deployed to REPO\BepInEx\plugins\
) else (
    echo [ERROR] Build v1.8.0 failed.
)
