// ***********************************************************************
// Assembly         : PureActive.Core.Test.XUnit.WPF.UnitTests
// Author           : SteveBu
// Created          : 04-20-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="WPFIntegrationTests.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PureActive.Core.Test.XUnit.WPF.IntegrationTests
{
    using System;
    using FluentAssertions;
    using PureActive.Core.Test.XUnit.Extensions;
    using PureActive.Core.Test.XUnit.Traits;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Class WPFIntegrationTests.
    /// </summary>
    [PlatformTrait(typeof(WPFIntegrationTests))]
    public class WPFIntegrationTests
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly Type testType = typeof(WPFIntegrationTests);

        public WPFIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void WPFIntegrationTests_HasPlatformTraitAttribute()
        {
            this.testType.HasPlatformTraitAttribute().Should().BeTrue();
        }

        [Fact]
        public void WPFIntegrationTests_GetTraitOperatingSystem()
        {
            this.testType.GetTraitOperatingSystem().Should().Be(TraitOperatingSystem.WPF);
        }

        [Fact]
        public void WPFIntegrationTests_GetTraitTestType()
        {
            this.testType.GetTraitTestType().Should().Be(TraitTestType.Integration);
        }
    }
}
