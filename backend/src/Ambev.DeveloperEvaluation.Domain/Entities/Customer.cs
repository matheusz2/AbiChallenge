using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
} 