// ***********************************************************************
// Assembly         : PureActive.Core.Test.Abstractions
// Author           : SteveBu
// Created          : 04-11-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-11-2020
// ***********************************************************************
// <copyright file="PlatformTraitAttribute.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Implements an XUnit Trait Attribute.</summary>
// ***********************************************************************
namespace PureActive.Core.Test.XUnit.Traits
{
    using System;
    using Xunit.Sdk;

    /// <summary>
    /// Attribute used to decorate a test method with arbitrary name/value pairs ("traits").
    /// </summary>
    [TraitDiscoverer("PureActive.Core.Test.XUnit.Traits.PlatformTraitDiscoverer", "PureActive.Core.Test.XUnit")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class PlatformTraitAttribute : Attribute, ITraitAttribute
    {
        private readonly Type? type;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformTraitAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public PlatformTraitAttribute(Type? type)
        {
            this.type = type;
        }
    }
}
