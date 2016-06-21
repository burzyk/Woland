// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="burzyk">
//   Copyright (c) burzyk. All rights reserved.
// </copyright>
// <summary>
//   Defines the BaseEntity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Service.Domain.Entities
{
    /// <summary>
    /// The base entity for all other entities.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <remarks>
        /// This is the surrogate id that identifies the entity.
        /// </remarks>
        public int Id { get; set; }
    }
}