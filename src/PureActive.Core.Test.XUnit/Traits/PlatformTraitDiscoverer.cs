﻿// ***********************************************************************
// Assembly         : PureActive.Core.Test.Abstractions
// Author           : SteveBu
// Created          : 04-11-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-11-2020
// ***********************************************************************
// <copyright file="PlatformTraitDiscoverer.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PureActive.Core.Test.XUnit.Traits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PureActive.Core.Test.XUnit.Extensions;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    /// The implementation of <see cref="ITraitDiscoverer" /> which returns the trait values
    /// for <see cref="PlatformTraitAttribute" />.
    /// </summary>
    public class PlatformTraitDiscoverer : ITraitDiscoverer
    {
        /// <inheritdoc/>
        public virtual IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            if (traitAttribute is null)
            {
                throw new ArgumentNullException(nameof(traitAttribute));
            }

            yield return new KeyValuePair<string, string>("Category", GetOperatingSystemString(traitAttribute));
            yield return new KeyValuePair<string, string>("Category", GetTraitTestTypeString(traitAttribute));
        }

        /// <summary>Gets the operating system string.</summary>
        /// <param name="traitAttribute">The trait attribute.</param>
        /// <returns>System.String.</returns>
        private static string GetOperatingSystemString(IAttributeInfo traitAttribute) => GetOperatingSystemTrait(traitAttribute).ToString();

        /// <summary>
        /// Platforms the type.
        /// </summary>
        /// <param name="traitAttribute">The trait attribute.</param>
        /// <returns>System.String.</returns>
        /// <autogeneratedoc />
        private static TraitOperatingSystem GetOperatingSystemTrait(IAttributeInfo traitAttribute)
        {
            var ctorArgs = traitAttribute.GetConstructorArguments().Cast<Type>().ToList();

            return ctorArgs[0].GetTraitOperatingSystem();
        }

        /// <summary>Gets the trait test type string.</summary>
        /// <param name="traitAttribute">The trait attribute.</param>
        /// <returns>System.String.</returns>
        private static string GetTraitTestTypeString(IAttributeInfo traitAttribute) =>
            GetTraitTestType(traitAttribute).ToString();

        /// <summary>
        /// Tests the type.
        /// </summary>
        /// <param name="traitAttribute">The trait attribute.</param>
        /// <returns>System.String.</returns>
        /// <autogeneratedoc />
        private static TraitTestType GetTraitTestType(IAttributeInfo traitAttribute)
        {
            var ctorArgs = traitAttribute.GetConstructorArguments().Cast<Type>().ToList();

            return ctorArgs[0].GetTraitTestType();
        }
    }
}
