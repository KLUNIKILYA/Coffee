using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Coffee.Pages.Contacts
{
    public class ContactsModel : PageModel
    {
        [BindProperty]
        public ContactInputModel Input { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Здесь логика отправки Email или сохранения в базу данных
            // Например: _emailService.SendEmailAsync("admin@site.com", Input.Subject, Input.Message);

            TempData["SuccessMessage"] = "Спасибо! Ваше сообщение отправлено. Мы свяжемся с вами в ближайшее время.";

            ModelState.Clear();
            Input = new ContactInputModel();

            return Page();
        }

        public class ContactInputModel
        {
            [Required(ErrorMessage = "Пожалуйста, представьтесь")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Email обязателен")]
            [EmailAddress(ErrorMessage = "Некорректный Email")]
            public string Email { get; set; }

            public string Subject { get; set; }

            [Required(ErrorMessage = "Введите текст сообщения")]
            [MinLength(10, ErrorMessage = "Сообщение слишком короткое")]
            public string Message { get; set; }
        }
    }
}
