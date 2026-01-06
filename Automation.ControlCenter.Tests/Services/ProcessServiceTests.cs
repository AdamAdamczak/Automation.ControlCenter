using Automation.ControlCenter.DTOs;
using Automation.ControlCenter.Infrastructure;
using Automation.ControlCenter.Services.Implementations;
using Automation.ControlCenter.Infrastructure.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Automation.ControlCenter.Tests.Infrastructure;
using Automation.ControlCenter.Domain;

public class ProcessServiceTests
{
    private readonly ProcessService _service;

    public ProcessServiceTests()
    {
        var repository = new FakeProcessRepository();
        var logger = NullLogger<ProcessService>.Instance;
        var stateService = new ProcessStateService();
        _service = new ProcessService(repository, logger, stateService);
    }

    [Fact]
    public void StartProcess_ShouldCreateProcess_WithStartedStatus()
    {
        // Arrange
        var request = new StartProcessRequest
        {
            ProcessName = "InvoiceBot",
            TriggeredBy = "Test"
        };

        // Act
        var result = _service.StartProcess(request);

        // Assert
        result.ProcessId.Should().NotBeEmpty();
        result.Status.Should().Be("Queued");
    }
    [Fact]
    public void GetStatus_ShouldReturnStarted_WhenProcessExists()
    {
        // Arrange
        var request = new StartProcessRequest
        {
            ProcessName = "InvoiceBot",
            TriggeredBy = "Test"
        };

        var result = _service.StartProcess(request);

        // Act
        var status = _service.GetStatus(result.ProcessId);

        // Assert
        status.Should().Be(ProcessStatus.Queued);
    }
    [Fact]
    public void GetStatus_ShouldThrow_WhenProcessDoesNotExist()
    {
        // Act
        Action act = () => _service.GetStatus(Guid.NewGuid());

        // Assert
        act.Should().Throw<ProcessNotFoundException>();
    }
    [Fact]
    public void UpdateStatus_ShouldAllow_StartedToRunning()
    {
        // Arrange
        var request = new StartProcessRequest
        {
            ProcessName = "InvoiceBot",
            TriggeredBy = "Test"
        };

        var result = _service.StartProcess(request);

        // Act
        _service.UpdateStatus(result.ProcessId, ProcessStatus.Running);

        // Assert
        var status = _service.GetStatus(result.ProcessId);
        status.Should().Be(ProcessStatus.Running);
    }
    [Fact]
    public void UpdateStatus_ShouldThrow_WhenTransitionIsInvalid()
    {
        // Arrange
        var request = new StartProcessRequest
        {
            ProcessName = "InvoiceBot",
            TriggeredBy = "Test"
        };

        var result = _service.StartProcess(request);

        // Act
        Action act = () =>
            _service.UpdateStatus(result.ProcessId, ProcessStatus.Completed);

        // Assert
        act.Should().Throw<InvalidStatusTransitionException>();
    }

}
