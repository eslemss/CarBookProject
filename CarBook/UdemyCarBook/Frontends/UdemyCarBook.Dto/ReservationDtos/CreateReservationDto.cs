using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Bu satırı mutlaka ekle!
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Dto.ReservationDtos
{
    public class CreateReservationDto
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen email adresinizi giriniz")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen telefon numaranızı giriniz")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Telefon numarası sadece rakamlardan oluşmalıdır")]
        public string Phone { get; set; }

        public int PickUpLocationID { get; set; }
        public int DropOffLocationID { get; set; }
        public int CarID { get; set; }

        [Required(ErrorMessage = "Lütfen yaşınızı giriniz")]
        [Range(18, 99, ErrorMessage = "Yaşınız 18 ile 99 arasında olmalıdır")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Lütfen ehliyet numaranızı giriniz")]
        public string DriverLicenseNumber { get; set; }

        public string Description { get; set; }
        public DateTime PickUpFull { get; set; }
        public DateTime DropOffFull { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public int? AppUserId { get; set; }
        public string? Status { get; set; } // Bu satırı ekleyerek hatayı gideriyoruz
    }
}