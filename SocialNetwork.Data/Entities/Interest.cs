using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Entities
{
    public class Interest
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxInterestLength)]
        [MinLength(DataConstants.MinInterestLength)]
        public string Tag { get; set; }

        public ICollection<UserInterest> Users { get; set; } = new List<UserInterest>();
    }
}