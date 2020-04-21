// ***********************************************************************
// Assembly         : PureActive.Core.Test.XUnit.Windows.UnitTests
// Author           : SteveBu
// Created          : 04-20-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="IOSUnitTests.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PureActive.Core.Test.XUnit.IOS.UnitTests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using PureActive.Core.Test.XUnit.Extensions;
    using PureActive.Core.Test.XUnit.Traits;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Class IOSUnitTests.
    /// </summary>
    [PlatformTrait(typeof(IOSUnitTests))]
    public class IOSUnitTests
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly Type testType = typeof(IOSUnitTests);

        public IOSUnitTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void IOSUnitTests_HasPlatformTraitAttribute()
        {
            this.testType.HasPlatformTraitAttribute().Should().BeTrue();
        }

        [Fact]
        public void IOSUnitTests_GetPlatformTraits()
        {
            foreach (KeyValuePair<string, string> pair in this.testType.GetPlatformTraits())
            {
                this.testOutputHelper.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }

        [Fact]
        public void IOSUnitTests_GetTraitOperatingSystem()
        {
            this.testType.GetTraitOperatingSystem().Should().Be(TraitOperatingSystem.IOS);
        }

        [Fact]
        public void IOSUnitTests_GetTraitTestType()
        {
            this.testType.GetTraitTestType().Should().Be(TraitTestType.Unit);
        }
    }
}
