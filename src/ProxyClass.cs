// 1****************************************************************************
// Project:  AssemblyLoader
// File:     ProxyClass.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

using System;

namespace AssemblyLoader;

[Serializable]
internal class ProxyClass : MarshalByRefObject, IDisposable
{
    public enum InstanceStatus
    {
        Loaded,
        Unloaded,
        Error
    }


    // some fields that require cleanup
    private bool _disposed; // to detect redundant calls


    #region .ctor

    // ---------------------------------------------------------------

    public ProxyClass()
    { }


    public ProxyClass(string assemblyFile, string domainName, string configFile = null)
    {
        Status = InstanceStatus.Unloaded;
        try
        {
            AssemblyFile     = assemblyFile;                                  //full path to assembly
            AssemblyFileName = System.IO.Path.GetFileName(AssemblyFile);      //assmbly file name
            Path             = System.IO.Path.GetDirectoryName(AssemblyFile); //get root directory from assembly path

            //start to configure domain
            var appDomainInfo = new AppDomainSetup
            {
                ApplicationBase     = Path,
                PrivateBinPath      = Path,
                PrivateBinPathProbe = Path,
                ShadowCopyFiles     = "true",
                LoaderOptimization  = LoaderOptimization.MultiDomainHost
            };
            if (!string.IsNullOrEmpty(configFile))
                appDomainInfo.ConfigurationFile = configFile;

            //lets create the domain
            AtomicAppDomain = AppDomain.CreateDomain(domainName, null, appDomainInfo);

            // Instanciate the class.
            InstancedObject = (ProxyClass)AtomicAppDomain?.CreateInstanceFromAndUnwrap(AssemblyFile, typeof(ProxyClass).ToString());
            Status          = InstanceStatus.Loaded;
        }
        catch (Exception exception)
        {
            //There was a problema setting up the new appDomain
            Status        = InstanceStatus.Error;
            LoadingErrors = exception.ToString();
        }
    }

    #endregion


    #region Properties
    // ---------------------------------------------------------------

    public string AssemblyFile { get; } = string.Empty;

    public string AssemblyFileName { get; }

    public AppDomain AtomicAppDomain { get; }

    public dynamic InstancedObject { get; }

    public string LoadingErrors { get; }

    public InstanceStatus? Status { get; }

    public string Path { get; }

    // ---------------------------------------------------------------
    #endregion Properties


    #region Methods

    // ---------------------------------------------------------------
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        #if !NETCOREAPP
        if (disposing)
            AppDomain.Unload(AtomicAppDomain);
        #endif

        _disposed = true;
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // ---------------------------------------------------------------
    #endregion Methods
}