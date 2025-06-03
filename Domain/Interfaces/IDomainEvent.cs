using MediatR;

namespace Domain.Interfaces;

/// <summary>
/// Интерфейс доменных событий.
/// </summary>
public interface IDomainEvent : INotification;