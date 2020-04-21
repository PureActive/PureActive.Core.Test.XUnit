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
    using System.Linq;
    using System.Reflection;
    using PureActive.Core.Test.XUnit.Traits;
    using Xunit.Sdk;

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

        /// <summary>Gets the platform traits.</summary>
        /// <param name="type">The type.</param>
        /// <returns>IEnumerable&lt;KeyValuePair&lt;System.String, System.String&gt;&gt;.</returns>
        public static IEnumerable<KeyValuePair<string, string>> GetPlatformTraits(this Type type)
        {
            CustomAttributeData platformTraitAttribute = CustomAttributeData.GetCustomAttributes(type).FirstOrDefault(cad =>
                cad.AttributeType.Equals(typeof(PlatformTraitAttribute)));

            if (platformTraitAttribute is null)
            {
                yield break;
            }

            var platformTraitDiscovery = new PlatformTraitDiscoverer();

            foreach (KeyValuePair<string, string> pair in platformTraitDiscovery.GetTraits(new ReflectionAttributeInfo(platformTraitAttribute)))
            {
                yield return pair;
            }
        }

        /// <summary>Determines whether [has platform attribute] [the specified type].</summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [has platform attribute] [the specified type]; otherwise, <c>false</c>.</returns>
        public static bool HasPlatformTraitAttribute(this Type type) => !(type.GetPlatformTraitAttribute() is null);

        /// <summary>Gets the operating system trait.</summary>
        /// <param name="type">The type.</param>
        /// <returns>TraitOperatingSystem.</returns>
        public static TraitOperatingSystem GetTraitOperatingSystem(this Type type)
        {
            foreach (KeyValuePair<string, string> pair in GetPlatformTraits(type))
            {
                if (Enum.TryParse(pair.Value, true, out TraitOperatingSystem traitOperatingSystem))
                {
                    return traitOperatingSystem;
                }
            }

            return TraitOperatingSystem.Unknown;
        }

        /// <summary>Gets the type of test.</summary>
        /// <param name="type">The type.</param>
        /// <returns>TraitTestType.</returns>
        public static TraitTestType GetTraitTestType(this Type type)
        {
            foreach (KeyValuePair<string, string> pair in GetPlatformTraits(type))
            {
                if (Enum.TryParse(pair.Value, true, out TraitTestType traitTestType))
                {
                    return traitTestType;
                }
            }

            return TraitTestType.Unknown;
        }

        /// <summary>
        /// Gets the display name of the full.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.String.</returns>
        internal static string GetDisplayFullName(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

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

                    var backTickIndex = partName.IndexOf('`');
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
            else if (BuiltInTypeNames.ContainsKey(type))
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

        /// <summary>
        /// Gets the assembly namespace.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.String.</returns>
        internal static string GetAssemblyNamespace(this Type type)
        {
            return type?.Assembly.GetNamespace() ?? string.Empty;
        }

        /// <summary>Returns the PlatformTraitAttribute associated with this type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>System.Nullable&lt;PlatformTraitAttribute&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">type.</exception>
        internal static PlatformTraitAttribute? GetPlatformTraitAttribute(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
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
        internal static bool Contains(this string input, string test, StringComparison stringComparison)
        {
            if (input == null || test == null)
            {
                return false;
            }

            return input.IndexOf(test, stringComparison) != -1;
        }

        private static string GetNamespace(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
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
        private static string[] SplitOnFirstDelim(this string str, char chDelim)
        {
            return string.IsNullOrEmpty(str) ? new string[2] : ProcessSplits(str, str.IndexOf(chDelim));
        }

        /// <summary>
        /// Splits the on last delimiter.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="chDelim">The ch delimiter.</param>
        /// <returns>System.String[].</returns>
        private static string[] SplitOnLastDelim(this string str, char chDelim)
        {
            return string.IsNullOrEmpty(str) ? new string[2] : ProcessSplits(str, str.LastIndexOf(chDelim));
        }

        /// <summary>
        /// Strings the after last delimiter.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="chDelim">The ch delimiter.</param>
        /// <returns>System.String.</returns>
        private static string? StringAfterLastDelim(this string str, char chDelim)
        {
            var strings = SplitOnLastDelim(str, chDelim);

            return strings.Length == 2 ? strings[1] : null;
        }

        /// <summary>
        /// Processes the passed in string into an array of strings based on indexDelim.
        /// </summary>
        /// <param name="str">The string to split.</param>
        /// <param name="indexDelim">The index delimiter.</param>
        /// <returns>System.String[].</returns>
        private static string[] ProcessSplits(string str, int indexDelim)
        {
            var strings = new string[2];

            if (string.IsNullOrEmpty(str))
            {
                return strings;
            }

            if (indexDelim != -1 && indexDelim <= str.Length - 1)
            {
                strings[0] = str.Substring(0, indexDelim).Trim();
                strings[1] = str.Substring(indexDelim + 1).Trim();
            }
            else
            {
                strings[0] = str.Trim();
                strings[1] = string.Empty;
            }

            return strings;
        }
    }
}
