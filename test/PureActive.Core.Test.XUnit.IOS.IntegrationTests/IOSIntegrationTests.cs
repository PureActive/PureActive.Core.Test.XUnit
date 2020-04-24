// ***********************************************************************
// Assembly         : PureActive.Core.Test.XUnit.IOS.IntegrationTests
// Author           : SteveBu
// Created          : 04-20-2020
//
// Last Modified By : SteveBu
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="IOSIntegrationTests.cs" company="BushChang Corporation">
//     Copyright (c) BushChang Corporation. All rights reserved.
//     Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PureActive.Core.Test.XUnit.IOS.IntegrationTests
{
    using System;
    using FluentAssertions;
    using PureActive.Core.Test.XUnit.Extensions;
    using PureActive.Core.Test.XUnit.Traits;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Class IOSIntegrationTests.
    /// </summary>
    [PlatformTrait(typeof(IOSIntegrationTests))]
    public class IOSIntegrationTests
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly Type testType = typeof(IOSIntegrationTests);

        public IOSIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void IOSIntegrationTests_HasPlatformTraitAttribute()
        {
            this.testType.HasPlatformTraitAttribute().Should().BeTrue();
        }

        [Fact]
        public void IOSIntegrationTests_GetTraitOperatingSystem()
        {
            this.testType.GetTraitOperatingSystem().Should().Be(TraitOperatingSystem.IOS);
        }

        [Fact]
        public void IOSIntegrationTests_GetTraitTestType()
        {
            this.testType.GetTraitTestType().Should().Be(TraitTestType.Integration);
        }
    }
}
