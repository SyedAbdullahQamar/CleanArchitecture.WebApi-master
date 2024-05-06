using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Funds
{
    public class AccountDetails
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string WalletAccount { get; set; }
    }
}
