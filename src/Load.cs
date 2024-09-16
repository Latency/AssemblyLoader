// 1****************************************************************************
// Project:  AssemblyLoader
// File:     Load.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

using System.Reflection;

namespace AssemblyLoader;

public static class Load
{
    /// <summary>
    ///     Load`
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static Assembly? Assembly<T>(T assembly)
    {
        Assembly? myAssembly = null;

        var type = typeof(T);
        if (type == typeof(string))
        {
            var filePath = assembly as string;
            if (string.IsNullOrEmpty(filePath))
                return null;

            try
            {
                var data = File.ReadAllBytes(filePath);
                myAssembly = Assembly(data);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        else if (type == typeof(byte[]))
        {
            if (assembly is not byte[] data)
                return null;

            if (data.Length == 0)
                return null;

            try
            {
                myAssembly = System.Reflection.Assembly.Load(data);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        else if (type == typeof(AssemblyName))
        {
            // This should always be null, since there are no embedded assemblies within AssemblyInfo or contains the name as that found in Tests. 
            // Since we are running these with VST in a proxy rather than assemblies loaded in the executing assembly, loaded assemblies can not be unloaded
            // without bringing down the domain.   There must be a host domain to call our methods.
            //
            // Leaving this one in for backwards compatibility, which will mean that any local referenced assembly will need to be converted from <string>
            // to <AssemblyName> in order to be used.
            //
            // This should always fail as no dependencies exist.
            //
            // Therefore, I will overload the Assenbly.Load(<string>) to be an explicit file path rather than namespace display name assembly reference.
            if (assembly is not AssemblyName assemblyName)
                return null;

            try
            {
                myAssembly = System.Reflection.Assembly.Load(assemblyName);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        else if (type == typeof(Stream))
        {
            if (assembly is not Stream asmStream)
                return null;
            var data = new byte[asmStream.Length];
            _          = asmStream.Read(data, 0, data.Length);
            myAssembly = Assembly(data);
        }

        return myAssembly;
    }


    public static IEnumerable<Assembly?> Assembly<T>(IEnumerable<T> obj) => obj.Select(Assembly);
}