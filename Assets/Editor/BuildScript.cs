using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

public static class BuildScript
{
    private const string OutputDir = "Build/Windows";
    private const string OutputExe = "FPSPrototype.exe";

    /// <summary>
    /// Build a Windows 64-bit standalone player to Build/Windows/FPSPrototype.exe.
    /// Run via Unity CLI:
    /// unity -quit -batchmode -projectPath . -executeMethod BuildScript.BuildWindows
    /// </summary>
    public static void BuildWindows()
    {
        if (!Directory.Exists(OutputDir))
        {
            Directory.CreateDirectory(OutputDir);
        }

        var options = new BuildPlayerOptions
        {
            scenes = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes),
            locationPathName = Path.Combine(OutputDir, OutputExe),
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };

        var report = BuildPipeline.BuildPlayer(options);
        var summary = report.summary;

        if (summary.result != BuildResult.Succeeded)
        {
            throw new System.Exception($"Build failed: {summary.result} (errors: {summary.totalErrors})");
        }

        EditorUtility.RevealInFinder(options.locationPathName);
    }
}
