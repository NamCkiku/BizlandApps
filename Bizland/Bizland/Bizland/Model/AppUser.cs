using System;

namespace Bizland.Model
{
    public class AppUser
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Avatar { get; set; }

        public DateTime? BirthDay { get; set; }

        public bool? Status { get; set; }

        public bool? Gender { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public int Coin { get; set; }

        public int RewardPoint { get; set; }

        public int RankStar { get; set; }
    }
}
