// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Agency.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Agency type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Service.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The employment agency details.
    /// </summary>
    public class Agency : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }
    }
}