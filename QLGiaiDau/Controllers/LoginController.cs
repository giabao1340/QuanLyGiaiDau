using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using QLGiaiDau.Models;

namespace QLGiaiDau.Controllers
{
    public class LoginController : Controller
    {
        private QLGiaiDauEntities db = new QLGiaiDauEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Xác thực người dùng
                var user = db.MANAGERs
                    .FirstOrDefault(u => u.UserName == model.Username && u.PassWord == model.Password);

                if (user != null)
                {
                    // Đăng nhập thành công
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("TrangChu", "Home"); // Chuyển hướng đến TrangChu sau khi đăng nhập thành công
                }
                else
                {
                    ModelState.AddModelError("", "Username hoặc Password không đúng.");
                }
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            // Đăng xuất người dùng
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login"); // Chuyển hướng về trang đăng nhập sau khi đăng xuất
        }
    }
}
