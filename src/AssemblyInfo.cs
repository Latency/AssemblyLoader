// 1****************************************************************************
// Project:  AssemblyLoader
// File:     AssemblyInfo.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************
// ReSharper disable UnusedMember.Global

using System.Diagnostics;
using System.Reflection;

namespace AssemblyLoader;

public static class AssemblyInfo
{
    public static string? DefaultLangVersion
    {
        get
        {
            // Start the child process.
            var p = new Process();
            p.StartInfo = new()
            {
                UseShellExecute        = false,
                RedirectStandardOutput = true,
                FileName               = $@"{Environment.GetEnvironmentVariable("VSAPPIDDIR")}..\..\MSBuild\Current\Bin\Roslyn\csc.exe",
                Arguments              = "-langversion:?"
            };

            p.Start();

            var output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return output.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries)
                         .Where(line => char.IsNumber(line[0]))
                         .SingleOrDefault(line => line.Substring(line.Length - 10, line.Length).EndsWith(" (default)"));
        }
    }

    public static string?  Authors(this         Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyAuthorsAttribute>(a => a.Authors);
    public static string?  Company(this         Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company);
    public static string?  Configuration(this   Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyConfigurationAttribute>(a => a.Configuration);
    public static string?  Copyright(this       Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright);
    public static string?  Description(this     Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);
    public static string?  FileVersion(this     Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version);
    public static string?  LangVersion(this     Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyLangVersionAttribute>(a => a.LangVersion);
    public static string?  Product(this         Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyProductAttribute>(a => a.Product);
    public static string?  ProjectUrl(this      Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyPackageProjectUrlAttribute>(a => a.PackageProjectUrl);
    public static string?  SupportEmail(this    Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblySupportEmailAttribute>(a => a.SupportEmail);
    public static string?  Title(this           Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title);
    public static string?  Trademark(this       Assembly asm) => asm.GetExecutingAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark);
    public static string?  VersionBuild(this    Assembly asm) => Version(asm)?.Build.ToString();
    public static string?  VersionFull(this     Assembly asm) => Version(asm)?.ToString();
    public static string?  VersionMajor(this    Assembly asm) => Version(asm)?.Major.ToString();
    public static string?  VersionMinor(this    Assembly asm) => Version(asm)?.Minor.ToString();
    public static string?  VersionRevision(this Assembly asm) => Version(asm)?.Revision.ToString();
    public static Version? Version(this         Assembly asm) => asm.GetName().Version;

    private static string? GetExecutingAssemblyAttribute<T>(this Assembly asm, Func<T, string> value)
        where T : Attribute => Attribute.GetCustomAttribute(asm, typeof(T)) is T attribute ? value.Invoke(attribute) : null;
}