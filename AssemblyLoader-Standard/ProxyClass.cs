//  *****************************************************************************
//  File:       ProxyClass.cs
//  Solution:   AssemblyLoader
//  Project:    AssemblyLoader
//  Date:       09/10/2017
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2017
//  *****************************************************************************

using System;
using System.Runtime.InteropServices;

namespace AssemblyLoader {
  [Serializable]
  [ClassInterface(ClassInterfaceType.AutoDual)]
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


    public ProxyClass(string assemblyFile) {
      Status = InstanceStatus.Unloaded;
      try {
        AssemblyFile = assemblyFile; //full path to assembly
        AssemblyFileName = System.IO.Path.GetFileName(AssemblyFile); //assmbly file name
        Path = System.IO.Path.GetDirectoryName(AssemblyFile); //get root directory from assembly path

        //lets create the domain
        AtomicAppDomain = AppDomain.CreateDomain("Proxy");

        // Instanciate the class.
        InstancedObject = AtomicAppDomain.Load(AssemblyFile);
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