// ***********************************************************************
// Assembly         : PureActive.Core.Test.XUnit
// Author           : SteveBu
// Created          : 04-20-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="TraitTestType.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Types of tests.</summary>
// ***********************************************************************
namespace PureActive.Core.Test.XUnit.Extensions
{
    /// <summary>
    /// Enum TraitTestType.
    /// </summary>
    public enum TraitTestType
    {
        /// <summary>
        /// Unknown test type
        /// </summary>
        Unknown,

        /// <summary>
        /// Unit tests
        /// </summary>
        Unit,

        /// <summary>
        /// Integration tests
        /// </summary>
        Integration,

        /// <summary>
        /// Local only tests
        /// </summary>
        Local,
    }
}
