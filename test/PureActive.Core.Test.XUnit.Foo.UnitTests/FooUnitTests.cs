// ***********************************************************************
// Assembly         : PureActive.Core.Test.XUnit.Foo.UnitTests
// Author           : SteveBu
// Created          : 04-20-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="FooUnitTests.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PureActive.Core.Test.XUnit.Foo.UnitTests
{
    using System;
    using FluentAssertions;
    using PureActive.Core.Test.XUnit.Extensions;
    using PureActive.Core.Test.XUnit.Traits;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Class FooUnitTests.
    /// </summary>
    [PlatformTrait(typeof(FooUnitTests))]
    public class FooUnitTests
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly Type testType = typeof(FooUnitTests);

        public FooUnitTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void FooUnitTests_HasPlatformTraitAttribute()
        {
            this.testType.HasPlatformTraitAttribute().Should().BeTrue();
        }

        [Fact]
        public void FooUnitTests_GetTraitOperatingSystem()
        {
            this.testType.GetTraitOperatingSystem().Should().Be(TraitOperatingSystem.Core);
        }

        [Fact]
        public void FooUnitTests_GetTraitTestType()
        {
            this.testType.GetTraitTestType().Should().Be(TraitTestType.Unit);
        }
    }
}
