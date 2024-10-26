using Microsoft.AspNetCore.Mvc;
using MVCmodel.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVCmodel.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact/Index
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Contact/Index
        [HttpPost]
        public async Task<ActionResult> Index(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Địa chỉ email gửi và nhận được chỉ định sẵn
                string senderEmail = "vuongkhiem56@gmail.com"; // Địa chỉ email gửi
                string recipientEmail = "khiemyy18@gmail.com"; // Địa chỉ email nhận

                // Gửi email
                await SendEmailAsync(model, senderEmail, recipientEmail);
                ViewBag.Message = "Email đã được gửi thành công!";
                ModelState.Clear(); // Làm sạch ModelState để chuẩn bị cho lần gửi tiếp theo
            }
            return View(model); // Trả về model để giữ thông tin đã nhập
        }

        // Phương thức gửi email sử dụng SMTP
        private async Task SendEmailAsync(ContactFormModel model, string senderEmail, string recipientEmail)
        {
            const string senderPassword = "efrk ijix gqob hwsr"; // Mật khẩu email gửi

            var fromAddress = new MailAddress(senderEmail, "Custormer Info");
            var toAddress = new MailAddress(recipientEmail, "UITEL");
            const string subject = "New Contact Form Submission";
            var plainTextContent = $"Name: {model.Name}\nPhone: {model.Phone}\nEmail: {model.Email}\nRoom: {model.Room}\nMessage: {model.Message}";
            var htmlContent = "<strong>" + plainTextContent.Replace("\n", "<br>") + "</strong>";

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, senderPassword)
            })
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = htmlContent,
                IsBodyHtml = true
            })
            {
                try
                {
                    await smtp.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (ví dụ: ghi log hoặc thông báo cho người dùng)
                    Console.WriteLine("Error sending email: " + ex.Message);
                }
            }
        }
    }
}
