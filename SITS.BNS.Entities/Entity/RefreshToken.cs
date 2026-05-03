using SITS.BNS.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SITS.BNS.Entities.Entity
{
    public class RefreshToken : CommonEntity
    {
        [Key]

        public string? Token { get; set; }
        public DateTime? Expires { get; set; }
        public bool IsRevoked { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; } = null!;
    }

}
