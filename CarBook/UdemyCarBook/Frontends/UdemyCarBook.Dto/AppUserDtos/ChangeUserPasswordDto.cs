using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Dto.AppUserDtos
{
    public class ChangeUserPasswordDto
    {
        public int AppUserId { get; set; }
        public string CurrentPassword { get; set; } // Eski şifreyi doğrulamak için şart
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; } // Yeni şifre tekrarı
    }
}
