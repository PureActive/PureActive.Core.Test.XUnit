// ***********************************************************************
// Assembly         : PureActive.Core
// Author           : SteveBu
// Created          : 04-11-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-12-2020
// ***********************************************************************
// <copyright file="PlatformTraitExtensions.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Extensions to System Type class.</summary>
// ***********************************************************************

namespace PureActive.Core.Test.XUnit.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using PureActive.Core.Test.XUnit.Traits;

    /// <summary>
    /// Class TypeExtensions.
    /// </summary>
    public static class PlatformTraitExtensions
    {
        /// <summary>
        /// The built in type names. Extensions to System Type class.
        /// </summary>
        private static readonly Dictionary<Type, string> BuiltInTypeNames = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(object), "object" },
            { typeof(sbyte), "sbyte" },
            { typeof(short), "short" },
            { typeof(string), "string" },
            { typeof(uint), "uint" },
            { typeof(ulong), "ulong" },
            { typeof(ushort), "ushort" },
        };

        /// <summary>Gets the operating system trait.</summary>
        /// <param name="type">The type.</param>
        /// <returns>TraitOperatingSystem.</returns>
        /// <exception cref="ArgumentNullException">type of class.</exception>
        public static TraitOperatingSystem GetTraitOperatingSystem(this Type? type)
        {
            if (type is null)
            {
                return TraitOperatingSystem.Unknown;
            }

            try
            {
                var assemblyNamespace = type.GetAssemblyNamespace();

                if (string.IsNullOrEmpty(assemblyNamespace))
                {
                    return TraitOperatingSystem.Unknown;
                }

                if (assemblyNamespace.Contains(".Windows", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitOperatingSystem.Windows;
                }

                if (assemblyNamespace.Contains(".WPF", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitOperatingSystem.WPF;
                }

                if (assemblyNamespace.Contains(".MacOS", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitOperatingSystem.MacOS;
                }

                if (assemblyNamespace.Contains(".OSX", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitOperatingSystem.MacOS;
                }

                if (assemblyNamespace.Contains(".Android", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitOperatingSystem.Android;
                }

                if (assemblyNamespace.Contains(".iOS", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitOperatingSystem.IOS;
                }

                if (assemblyNamespace.Contains(".Linux", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitOperatingSystem.Linux;
                }

                if (assemblyNamespace.Contains(".Core", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitOperatingSystem.Core;
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                // ignored
            }

            return TraitOperatingSystem.Unknown;
        }

        /// <summary>Gets the type of the trait test.</summary>
        /// <param name="type">The type.</param>
        /// <returns>TraitTestType.</returns>
        /// <exception cref="ArgumentNullException">type of class.</exception>
        public static TraitTestType GetTraitTestType(this Type? type)
        {
            if (type is null)
            {
                return TraitTestType.Unknown;
            }

            try
            {
                var getDisplayFullName = type.GetDisplayFullName();

                if (string.IsNullOrEmpty(getDisplayFullName))
                {
                    return TraitTestType.Unknown;
                }

                if (getDisplayFullName.Contains(".Unit", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitTestType.Unit;
                }

                if (getDisplayFullName.Contains(".Integration", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitTestType.Integration;
                }

                if (getDisplayFullName.Contains(".Local", StringComparison.OrdinalIgnoreCase))
                {
                    return TraitTestType.Local;
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                // ignored
            }

            return TraitTestType.Unknown;
        }

        /// <summary>Determines whether [has platform attribute] [the specified type].</summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [has platform attribute] [the specified type]; otherwise, <c>false</c>.</returns>
        public static bool HasPlatformTraitAttribute(this Type? type) => !(type?.GetPlatformTraitAttribute() is null);

        /// <summary>
        /// Gets the display name of the full.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.String.</returns>
        internal static string GetDisplayFullName(this Type? type)
        {
            if (type is null)
            {
                return string.Empty;
            }

            try
            {
                if (type.GetTypeInfo().IsGenericType)
                {
                    var fullName = type.GetGenericTypeDefinition().FullName;

                    if (string.IsNullOrEmpty(fullName))
                    {
                        return string.Empty;
                    }

                    // Nested types (public or private) have a '+' in their full name
                    var parts = fullName.Split('+');

                    // Handle nested generic types
                    // Examples:
                    // ConsoleApp.Program+Foo`1+Bar
                    // ConsoleApp.Program+Foo`1+Bar`1
                    for (var i = 0; i < parts.Length; i++)
                    {
                        var partName = parts[i];

#pragma warning disable CA1307 // Specify StringComparison
                        var backTickIndex = partName.IndexOf('`');
#pragma warning restore CA1307 // Specify StringComparison
                        if (backTickIndex >= 0)
                        {
                            // Since '.' is typically used to filter log messages in a hierarchy kind of scenario,
                            // do not include any generic type information as part of the name.
                            // Example:
                            // Microsoft.AspNetCore.Mvc -> log level set as Warning
                            // Microsoft.AspNetCore.Mvc.ModelBinding -> log level set as Verbose
                            partName = partName.Substring(0, backTickIndex);
                        }

                        parts[i] = partName;
                    }

                    return string.Join(".", parts);
                }

                if (BuiltInTypeNames.ContainsKey(type))
                {
                    return BuiltInTypeNames[type];
                }
                else
                {
                    var fullName = type.FullName;

                    if (string.IsNullOrEmpty(fullName))
                    {
                        return string.Empty;
                    }

                    if (type.IsNested)
                    {
                        fullName = fullName.Replace('+', '.');
                    }

                    return fullName;
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                // ignored
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the assembly namespace.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.String.</returns>
        internal static string GetAssemblyNamespace(this Type? type)
        {
            return type?.Assembly.GetNamespace() ?? string.Empty;
        }

        /// <summary>Returns the PlatformTraitAttribute associated with this type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>System.Nullable&lt;PlatformTraitAttribute&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">type.</exception>
        internal static PlatformTraitAttribute? GetPlatformTraitAttribute(this Type? type)
        {
            if (type is null)
            {
                return null;
            }

            foreach (Attribute customAttributeData in type.GetCustomAttributes())
            {
                if (customAttributeData.ToString().Equals("PureActive.Core.Test.XUnit.Traits.PlatformTraitAttribute", StringComparison.InvariantCulture))
                {
                    return customAttributeData as PlatformTraitAttribute;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines whether this instance contains the test string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="test">The test.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns><c>true</c> if [contains] [the specified test]; otherwise, <c>false</c>.</returns>
        internal static bool Contains(this string? input, string? test, StringComparison stringComparison)
        {
            if (input is null || test is null)
            {
                return false;
            }

            return input.IndexOf(test, stringComparison) != -1;
        }

        private static string GetNamespace(this Assembly? assembly)
        {
            if (assembly is null)
            {
                return string.Empty;
            }

            var assemblyName = assembly.FullName;
            return assemblyName.SplitOnFirstDelim(',')[0];
        }

        /// <summary>
        /// Splits the on first delimiter.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="chDelim">The ch delimiter.</param>
        /// <returns>System.String[].</returns>
        private static string[] SplitOnFirstDelim(this string? str, char chDelim)
        {
#pragma warning disable CA1307 // Specify StringComparison
            return string.IsNullOrEmpty(str) ? new string[2] : ProcessSplits(str!, str!.IndexOf(chDelim));
#pragma warning restore CA1307 // Specify StringComparison
        }

        /// <summary>
        /// Processes the passed in string into an array of strings based on indexDelim.
        /// </summary>
        /// <param name="str">The string to split.</param>
        /// <param name="indexDelim">The index delimiter.</param>
        /// <returns>System.String[].</returns>
        private static string[] ProcessSplits(string? str, int indexDelim)
        {
            var strings = new string[2];

            if (string.IsNullOrEmpty(str))
            {
                return strings;
            }

            if (indexDelim != -1 && indexDelim <= str!.Length - 1)
            {
                strings[0] = str.Substring(0, indexDelim).Trim();
                strings[1] = str.Substring(indexDelim + 1).Trim();
            }
            else
            {
                strings[0] = str!.Trim();
                strings[1] = string.Empty;
            }

            return strings;
        }
    }
}
