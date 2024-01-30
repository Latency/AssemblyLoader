// 1****************************************************************************
// Project:  UnitTests
// File:     Tests.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

#if NETFRAMEWORK

#else
#if LAUNCH_SETTINGS
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
#endif
#endif

namespace AssemblyLoader_Unit_Tests
{
    public partial class Tests(ITestOutputHelper console) : Base(console)
    {
        #region Launch Settings

        #if !NETFRAMEWORK && LAUNCH_SETTINGS
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
    using var file = File.OpenText("Properties\\launchSettings.json");
    var       reader = new JsonTextReader(file);
    var jObject = JObject.Load(reader);
    var variables = (jObject.GetValue("profiles") ?? throw new InvalidOperationException("Unable to get value 'profiles'."))
                           //select a proper profile here
                           .SelectMany(profiles => profiles.Children())
                           .SelectMany(profile => profile.Children<JProperty>())
                           .Where(prop => prop.Name == "environmentVariables")
                           .SelectMany(prop => prop.Value.Children<JProperty>())
                           .ToList();

    foreach (var variable in variables)
        Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
   //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        #endif

        #endregion Launch Settings
    }
}