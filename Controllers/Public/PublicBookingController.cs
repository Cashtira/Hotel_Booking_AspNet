using Newtonsoft.Json.Linq;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicBookingController : Controller
    {

        private readonly QuanLyKhachSanDBContext _context;
        private readonly BookingRepository _bookingRepository;
        private readonly BookingServiceRepository _bookingServiceRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly UserRepository _userRepository;

        public PublicBookingController(
            BookingRepository bookingRepository,
            BookingServiceRepository bookingServiceRepository,
            ServiceRepository serviceRepository,
            UserRepository userRepository,
                        QuanLyKhachSanDBContext context
)
        {
            _bookingRepository = bookingRepository;
            _bookingServiceRepository = bookingServiceRepository;
            _userRepository = userRepository;
            _context = context;
        }

        // GET: PublicBooking
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetBookings(int id, string mess)
        {
            var listBookingFormUserId = await _userRepository.GetBookingsForUserAsync(id);

            var bookingWithServices = new List<BookingWithServicesViewModel>();

            foreach (var booking in listBookingFormUserId)
            {
                // Lấy danh sách BookingService
                var bookingServices = await _bookingRepository.GetBookingServicesAsync(booking.idBooking);

                // Chuyển đổi BookingService thành Service
                var services = bookingServices
                    .Select(bs => bs.Service) // Lấy thuộc tính Service từ từng BookingService
                    .ToList();

                bookingWithServices.Add(new BookingWithServicesViewModel
                {
                    Booking = booking,
                    Services = services
                });
            }

            ViewBag.active = "listBooking";
            ViewBag.bookingWithServices = bookingWithServices;
            ViewBag.mess = mess;

            return View();
        }


        public ActionResult CancelBooking(int id)
        {
            User user = (User)Session["USER"];
            Booking booking = _context.Bookings.FirstOrDefault(x => x.idBooking == id);
            booking.status = 2;
            _context.SaveChanges();
            return RedirectToAction("GetBookings", new {id = user.idUser, mess="1"});
        }
        public ActionResult PaymentMoMo(int id)
        {
            var obj = _context.Bookings.Where(x => x.idBooking == id).FirstOrDefault();
            int total = obj.totalMoney;
            string url = "https://localhost:44385/PublicBooking/ReturnUrl/" + id;

            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "Thanh toán cho phòng tại web";
            string returnUrl = url;
            string notifyurl = "http://ba1adf48beba.ngrok.io/Home/SavePayment"; // URL Ngrok

            string amount = total.ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            // Tạo chữ ký SHA256
            string rawHash = $"partnerCode={partnerCode}&accessKey={accessKey}&requestId={requestId}&amount={amount}&orderId={orderid}&orderInfo={orderInfo}&returnUrl={returnUrl}&notifyUrl={notifyurl}&extraData={extraData}";
            string signature = signSHA256(rawHash, serectkey);

            // Tạo payload JSON
            JObject message = new JObject
    {
        { "partnerCode", partnerCode },
        { "accessKey", accessKey },
        { "requestId", requestId },
        { "amount", amount },
        { "orderId", orderid },
        { "orderInfo", orderInfo },
        { "returnUrl", returnUrl },
        { "notifyUrl", notifyurl },
        { "extraData", extraData },
        { "requestType", "captureMoMoWallet" },
        { "signature", signature }
    };

            // Gửi yêu cầu đến MoMo
            using (var client = new HttpClient())
            {
                var content = new StringContent(message.ToString(), Encoding.UTF8, "application/json");
                var response = client.PostAsync(endpoint, content).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                JObject jmessage = JObject.Parse(responseContent);

                if (jmessage.ContainsKey("payUrl"))
                {
                    return Redirect(jmessage.GetValue("payUrl").ToString());
                }
                else
                {
                    // Xử lý lỗi nếu không nhận được payUrl
                    return Content("Error: " + jmessage.ToString());
                }
            }
        }

        public ActionResult ReturnUrl(int id)
        {
            User user = (User)Session["USER"];
            var obj = _context.Bookings.Where(x => x.idBooking == id).FirstOrDefault();
            obj.isPayment = true;
            _context.SaveChanges();
            return RedirectToAction("GetBookings", new { id = user.idUser, mess = "2" });
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public ActionResult ConfirmPaymentClient()
        {
            //hiển thị thông báo cho người dùng
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db
        }
        public string getHash(string partnerCode, string merchantRefId,
          string amount, string paymentCode, string storeId, string storeName, string publicKeyXML)
        {
            string json = "{\"partnerCode\":\"" +
                partnerCode + "\",\"partnerRefId\":\"" +
                merchantRefId + "\",\"amount\":" +
                amount + ",\"paymentCode\":\"" +
                paymentCode + "\",\"storeId\":\"" +
                storeId + "\",\"storeName\":\"" +
                storeName + "\"}";

            byte[] data = Encoding.UTF8.GetBytes(json);
            string result = null;
            using (var rsa = new RSACryptoServiceProvider(4096)) //KeySize
            {
                try
                {
                    // MoMo's public key has format PEM.
                    // You must convert it to XML format. Recommend tool: https://superdry.apphb.com/tools/online-rsa-key-converter
                    rsa.FromXmlString(publicKeyXML);
                    var encryptedData = rsa.Encrypt(data, false);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    result = base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }

            }

            return result;

        }
        public string buildQueryHash(string partnerCode, string merchantRefId,
            string requestid, string publicKey)
        {
            string json = "{\"partnerCode\":\"" +
                partnerCode + "\",\"partnerRefId\":\"" +
                merchantRefId + "\",\"requestId\":\"" +
                requestid + "\"}";

            byte[] data = Encoding.UTF8.GetBytes(json);
            string result = null;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    // client encrypting data with public key issued by server
                    rsa.FromXmlString(publicKey);
                    var encryptedData = rsa.Encrypt(data, false);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    result = base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }

            }

            return result;

        }

        public string buildRefundHash(string partnerCode, string merchantRefId,
            string momoTranId, long amount, string description, string publicKey)
        {
            string json = "{\"partnerCode\":\"" +
                partnerCode + "\",\"partnerRefId\":\"" +
                merchantRefId + "\",\"momoTransId\":\"" +
                momoTranId + "\",\"amount\":" +
                amount + ",\"description\":\"" +
                description + "\"}";

            byte[] data = Encoding.UTF8.GetBytes(json);
            string result = null;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    // client encrypting data with public key issued by server
                    rsa.FromXmlString(publicKey);
                    var encryptedData = rsa.Encrypt(data, false);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    result = base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }

            }

            return result;

        }
        public string signSHA256(string message, string key)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string hex = BitConverter.ToString(hashmessage);
                hex = hex.Replace("-", "").ToLower();
                return hex;

            }
        }

        public static string sendPaymentRequest(string endpoint, string postJsonString)
        {

            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);

                var postData = postJsonString;

                var data = Encoding.UTF8.GetBytes(postData);

                httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";

                httpWReq.ContentLength = data.Length;
                httpWReq.ReadWriteTimeout = 30000;
                httpWReq.Timeout = 15000;
                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

                string jsonresponse = "";

                using (var reader = new StreamReader(response.GetResponseStream()))
                {

                    string temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                }


                //todo parse it
                return jsonresponse;
                //return new MomoResponse(mtid, jsonresponse);

            }
            catch (WebException e)
            {
                return e.Message;
            }
        }
    }
}