using NetArchTest.Rules;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SKB.App.Domain;

namespace SKB.App.Architecture.Tests;

/// <summary>
/// Checks project dependencies if it does not violate the architectural restrictions
/// </summary>
[TestClass]
public class ProjectDependencyTests
{
	private const string PresentationNamespace = "SKB.App.CleanWebApi.Http";
	private const string ApplicationNamespace = "SKB.App.Application";
	private const string DomainNamespace = "SKB.App.Domain";

	/// <summary>
	/// Domain Layer should not violate the conditional boundary
	/// </summary>
	[TestMethod]
	public void DomainProject_DoesNotHaveAnyDependenciesOnPresentationLayer_ReturnsSuccess()
	{
		var assembly = typeof(IDomainAssemblyMarker).Assembly;

		var results = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOn(ApplicationNamespace)
			.Or()
			.HaveDependencyOn(PresentationNamespace)
			.GetResult();

		results.IsSuccessful.Should().BeTrue();
	}

	/// <summary>
	/// Application Layer should not have any dependencies from Presentation Layer
	/// </summary>
	[TestMethod]
	public void ApplicationProject_DoesNotHaveAnyDependenciesOnPresentationLayer_ReturnsSuccess()
	{
		var assembly = typeof(IDomainAssemblyMarker).Assembly;
		var results = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOn(PresentationNamespace)
			.GetResult();
		results.IsSuccessful.Should().BeTrue();
	}
}
