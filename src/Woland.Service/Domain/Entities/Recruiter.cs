// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Recruiter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Recruiter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Service.Domain.Entities
{
    /// <summary>
    /// The agency recruiter details.
    /// </summary>
    public class Recruiter : BaseEntity
    {
        /// <summary>
        /// Gets or sets the agency.
        /// </summary>
        /// <remarks>
        /// If the recruiter changes the job we treat him as a new contact
        /// as he may have a different contact details position etc.
        /// </remarks>
        public virtual Agency Agency { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the telephone.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }
    }
}