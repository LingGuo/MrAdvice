﻿#region Mr. Advice
// Mr. Advice
// A simple post build weaving package
// http://mradvice.arxone.com/
// Released under MIT license http://opensource.org/licenses/mit-license.php
#endregion

using System;
using System.Reflection;
using ArxOne.MrAdvice;

// ReSharper disable once CheckNamespace
public class MrAdviceTask : StitcherTask<MrAdviceStitcher>
{
    /// <summary>
    /// Entry point for nested process (for isolation).
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns></returns>
    public static int Main(string[] args)
    {
        SetupBlobber();
        return Run(new MrAdviceTask(), args);
    }

    private static Type GetLoader() => Assembly.GetExecutingAssembly().GetType("\u2302");

    public static void SetupBlobber()
    {
        // this is Blobber's official method to execute setup on task assemblies
        GetLoader().GetMethod("Register").Invoke(null, new object[0]);
    }

    public static Assembly AssemblyResolve(Assembly assembly, ResolveEventArgs e)
    {
        return (Assembly)GetLoader().GetMethod("Resolve").Invoke(null, new object[] { assembly, e.Name });
    }
}