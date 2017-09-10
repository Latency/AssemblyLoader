//  *****************************************************************************
//  File:       Proxy.cs
//  Solution:   AssemblyInfo
//  Project:    AssemblyInfo
//  Date:       09/07/2017
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2017
//  *****************************************************************************

using System;
using System.Runtime.InteropServices;

namespace AssemblyInfo {
  [Serializable, ClassInterface(ClassInterfaceType.AutoDual)]
  internal class ProxyClass : MarshalByRefObject, IDisposable {
    public enum InstanceStatus {
      Loaded,
      Unloaded,
      Error
    }


    // some fields that require cleanup
    private bool _disposed; // to detect redundant calls


    #region .ctor
    // ---------------------------------------------------------------

    public ProxyClass() {
    }


    public ProxyClass(string assemblyFile, string domainName, string configFile = null) {
      Status = InstanceStatus.Unloaded;
      try {
        AssemblyFile = assemblyFile; //full path to assembly
        AssemblyFileName = System.IO.Path.GetFileName(AssemblyFile); //assmbly file name
        Path = System.IO.Path.GetDirectoryName(AssemblyFile); //get root directory from assembly path

        //start to configure domain
        var appDomainInfo = new AppDomainSetup {
          ApplicationBase = Path,
          PrivateBinPath = Path,
          PrivateBinPathProbe = Path
        };
        if (!string.IsNullOrEmpty(configFile)) {
          appDomainInfo.ConfigurationFile = configFile;
        }
        //lets create the domain
        AtomicAppDomain = AppDomain.CreateDomain(domainName, null, appDomainInfo);

        // Instanciate the class.
        InstancedObject = (ProxyClass)AtomicAppDomain.CreateInstanceFromAndUnwrap(AssemblyFile, typeof(ProxyClass).ToString());
        Status = InstanceStatus.Loaded;
      } catch (Exception exception) {
        //There was a problema setting up the new appDomain
        Status = InstanceStatus.Error;
        LoadingErrors = exception.ToString();
      }
    }
    #endregion


    #region Properties
    // ---------------------------------------------------------------

    public string AssemblyFile { get; }

    public string AssemblyFileName { get; }

    public AppDomain AtomicAppDomain { get; }

    public dynamic InstancedObject { get; }

    public string LoadingErrors { get; }

    public InstanceStatus Status { get; }

    public string Path { get; }

    // ---------------------------------------------------------------
    #endregion Properties


    #region Methods
    // ---------------------------------------------------------------

    protected virtual void Dispose(bool disposing) {
      if (!_disposed) {
        if (disposing) {
          // Dispose managed resources.
          AppDomain.Unload(AtomicAppDomain);
        }
        
        _disposed = true;
      }
    }


    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    // ---------------------------------------------------------------
    #endregion Methods
  }
}