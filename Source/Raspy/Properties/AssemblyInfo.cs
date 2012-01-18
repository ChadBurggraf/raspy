//-----------------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Raspy")]
[assembly: AssemblyDescription("Raspy arithmetic parsing and evaluation library.")]
[assembly: Guid("783a6de0-c40c-40cc-b094-93a945eb036c")]

#if DEBUG
[assembly: InternalsVisibleTo("Raspy.Test")]
#endif