// ***********************************************************************
// Assembly         : PureActive.Core.Test.XUnit.Android.IntegrationTests
// Author           : SteveBu
// Created          : 04-20-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="AndroidIntegrationTests.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PureActive.Core.Test.XUnit.Android.IntegrationTests
{
    using System;
    using FluentAssertions;
    using PureActive.Core.Test.XUnit.Extensions;
    using PureActive.Core.Test.XUnit.Traits;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Class AndroidIntegrationTests.
    /// </summary>
    [PlatformTrait(typeof(AndroidIntegrationTests))]
    public class AndroidIntegrationTests
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly Type testType = typeof(AndroidIntegrationTests);

        public AndroidIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AndroidIntegrationTests_HasPlatformTraitAttribute()
        {
            this.testType.HasPlatformTraitAttribute().Should().BeTrue();
        }

        [Fact]
        public void AndroidIntegrationTests_GetTraitOperatingSystem()
        {
            this.testType.GetTraitOperatingSystem().Should().Be(TraitOperatingSystem.Android);
        }

        [Fact]
        public void AndroidIntegrationTests_GetTraitTestType()
        {
            this.testType.GetTraitTestType().Should().Be(TraitTestType.Integration);
        }
    }
}
