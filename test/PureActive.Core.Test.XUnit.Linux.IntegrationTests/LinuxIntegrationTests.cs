// ***********************************************************************
// Assembly         : PureActive.Core.Test.XUnit.Linux.IntegrationTests
// Author           : SteveBu
// Created          : 04-20-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="LinuxIntegrationTests.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PureActive.Core.Test.XUnit.Linux.IntegrationTests
{
    using System;
    using FluentAssertions;
    using PureActive.Core.Test.XUnit.Extensions;
    using PureActive.Core.Test.XUnit.Traits;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Class LinuxIntegrationTests.
    /// </summary>
    [PlatformTrait(typeof(LinuxIntegrationTests))]
    public class LinuxIntegrationTests
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly Type testType = typeof(LinuxIntegrationTests);

        public LinuxIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void LinuxIntegrationTests_HasPlatformTraitAttribute()
        {
            this.testType.HasPlatformTraitAttribute().Should().BeTrue();
        }

        [Fact]
        public void LinuxIntegrationTests_GetTraitOperatingSystem()
        {
            this.testType.GetTraitOperatingSystem().Should().Be(TraitOperatingSystem.Linux);
        }

        [Fact]
        public void LinuxIntegrationTests_GetTraitTestType()
        {
            this.testType.GetTraitTestType().Should().Be(TraitTestType.Integration);
        }
    }
}
