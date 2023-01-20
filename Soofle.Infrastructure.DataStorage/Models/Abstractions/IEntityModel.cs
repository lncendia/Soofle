﻿using System.ComponentModel.DataAnnotations;

namespace Soofle.Infrastructure.DataStorage.Models.Abstractions;

public interface IEntityModel
{
    [Key]
    public int Id { get; set; }
    public int EntityId { get; set; }
}