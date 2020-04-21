// ***********************************************************************
// Assembly         : PureActive.Core.Test.XUnit.Windows.UnitTests
// Author           : SteveBu
// Created          : 04-20-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="AndroidUnitTests.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PureActive.Core.Test.XUnit.Android.UnitTests
{
    using System;
    using FluentAssertions;
    using PureActive.Core.Test.XUnit.Extensions;
    using PureActive.Core.Test.XUnit.Traits;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Class AndroidUnitTests.
    /// </summary>
    [PlatformTrait(typeof(AndroidUnitTests))]
    public class AndroidUnitTests
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly Type testType = typeof(AndroidUnitTests);

        public AndroidUnitTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AndroidUnitTests_HasPlatformTraitAttribute()
        {
            this.testType.HasPlatformTraitAttribute().Should().BeTrue();
        }

        [Fact]
        public void AndroidUnitTests_GetTraitOperatingSystem()
        {
            this.testType.GetTraitOperatingSystem().Should().Be(TraitOperatingSystem.Android);
        }

        [Fact]
        public void AndroidUnitTests_GetTraitTestType()
        {
            this.testType.GetTraitTestType().Should().Be(TraitTestType.Unit);
        }
    }
}
