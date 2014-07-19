//  *****************************************************************************
//  File:      Util.cs
//  Solution:  AssemblyInfo
//  Project:   DLL
//  Date:      01/03/2016
//  Author:    Latency McLaughlin
//  Copywrite: Bio-Hazard Industries - 1998-2016
//  *****************************************************************************

using System;
using System.Reflection;

namespace AssemblyInfo {
  public static class Util {
    /// <summary>
    ///   Retrieves a single value from a Custom <see cref="Attribute" /> associated with an <see cref="Assembly" />.
    /// </summary>
    /// <typeparam name="TAttribute">
    ///   The type of Custom <see cref="Attribute" /> associated with the
    ///   <paramref name="assembly" /> to retrieve.
    /// </typeparam>
    /// <typeparam name="TValue">The type of the value to retrieve from the <typeparamref name="TAttribute" />.</typeparam>
    /// <param name="assembly">The <see cref="Assembly" /> to retrieve the <typeparamref name="TAttribute" /> from.</param>
    /// <param name="valueRetrieval">
    ///   A callback used to retrieve a <typeparamref name="TValue" /> from a
    ///   <typeparamref name="TAttribute" />.
    /// </param>
    /// <returns>The retrieved value or <see langword="null" /> if no <typeparamref name="TAttribute" /> was found.</returns>
    public static TValue GetAttributeValue<TAttribute, TValue>(this Assembly assembly,
      Func<TAttribute, TValue> valueRetrieval) where TAttribute : Attribute {
      #region Sanity checks

      if (assembly == null)
        throw new ArgumentNullException(nameof(assembly));
      if (valueRetrieval == null)
        throw new ArgumentNullException(nameof(valueRetrieval));

      #endregion

      var attributes = assembly.GetCustomAttributes(typeof (TAttribute), false);
      return attributes.Length > 0 ? valueRetrieval((TAttribute) attributes[0]) : default(TValue);
    }

    /// <summary>
    ///   Gets the title property
    /// </summary>
    public static string ProductTitle(this Assembly asm) {
      return asm != null && asm.IsDefined(typeof (AssemblyTitleAttribute), false)
        ? asm.GetAttributeValue<AssemblyTitleAttribute, string>(a => a.Title)
        : string.Empty;
    }

    /// <summary>
    ///   Gets the application's assembly version
    /// </summary>
    public static string AssemblyVersion(this Assembly asm) {
      return asm?.GetName().Version.ToString() ?? string.Empty;
    }

    /// <summary>
    ///   Gets the application's file version
    /// </summary>
    public static string AssemblyFileVersion(this Assembly asm) {
      return asm != null && asm.IsDefined(typeof (AssemblyFileVersionAttribute), false)
        ? asm.GetAttributeValue<AssemblyFileVersionAttribute, string>(a => a.Version)
        : string.Empty;
    }

    /// <summary>
    ///   Gets the description about the application.
    /// </summary>
    public static string Description(this Assembly asm) {
      return asm != null && asm.IsDefined(typeof (AssemblyDescriptionAttribute), false)
        ? asm.GetAttributeValue<AssemblyDescriptionAttribute, string>(a => a.Description)
        : string.Empty;
    }

    /// <summary>
    ///   Gets the product's full name.
    /// </summary>
    public static string Product(this Assembly asm) {
      return asm != null && asm.IsDefined(typeof (AssemblyProductAttribute), false)
        ? asm.GetAttributeValue<AssemblyProductAttribute, string>(a => a.Product)
        : string.Empty;
    }

    /// <summary>
    ///   Gets the copyright information for the product.
    /// </summary>
    public static string Copyright(this Assembly asm) {
      return asm != null && asm.IsDefined(typeof (AssemblyCopyrightAttribute), false)
        ? asm.GetAttributeValue<AssemblyCopyrightAttribute, string>(a => a.Copyright)
        : string.Empty;
    }

    /// <summary>
    ///   Gets the company information for the product.
    /// </summary>
    public static string Company(this Assembly asm) {
      return asm != null && asm.IsDefined(typeof (AssemblyCompanyAttribute), false)
        ? asm.GetAttributeValue<AssemblyCompanyAttribute, string>(a => a.Company)
        : string.Empty;
    }

    /// <summary>
    ///   Gets the trademark information for the product.
    /// </summary>
    public static string Trademark(this Assembly asm) {
      return asm != null && asm.IsDefined(typeof (AssemblyTrademarkAttribute), false)
        ? asm.GetAttributeValue<AssemblyTrademarkAttribute, string>(a => a.Trademark)
        : string.Empty;
    }
  }
}