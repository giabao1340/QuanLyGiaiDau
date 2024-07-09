using QLGiaiDau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLGiaiDau.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TrangChu() { return View(); }
        public ActionResult DangKyDoiBong()
        {
            return View();
        }

        public ActionResult LapLichThiDau() { return View(); }
        public ActionResult GhiNhanKetQua() { return View(); }
        public ActionResult LapBangDau() { return View(); }
        public ActionResult LapBangXepHang() { return View(); }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private readonly YourDbContext _context;

    public HomeController()
    {
        _context = new YourDbContext();
    }

    // GET: DangKyDoiBong
    public IActionResult DangKyDoiBong()
    {
        var players = _context.Players.ToList();
        return View(players);
    }

    // POST: DangKyDoiBong
    [HttpPost]
    public IActionResult DangKyDoiBong(string teamName, string university, string coach, int foundedYear, string stadium)
    {
        if (ModelState.IsValid)
        {
            // Tạo mới đội bóng
            var team = new Team
            {
                TeamName = teamName,
                University = university,
                Coach = coach,
                FoundedYear = foundedYear,
                Stadium = stadium
            };

            _context.Teams.Add(team);
            _context.SaveChanges();

            // Lấy danh sách cầu thủ đã chọn và thêm vào đội bóng
            var playerIds = Request.Form["playerIds"];
            var playerIdList = playerIds.Select(int.Parse).ToList();

            foreach (var playerId in playerIdList)
            {
                var player = _context.Players.Find(playerId);
                if (player != null)
                {
                    var teamPlayer = new TeamPlayer
                    {
                        TeamId = team.TeamID,
                        PlayerId = player.PlayerID
                    };

                    _context.TeamPlayers.Add(teamPlayer);
                }
            }

            _context.SaveChanges();

            // Redirect đến view để xem danh sách cầu thủ sau khi đăng ký đội bóng
            return RedirectToAction("DangKyDoiBong");
        }

        // Nếu không hợp lệ, hiển thị lại view với lỗi
        var players = _context.Players.ToList();
        return View(players);
    }

    // GET: ThemCauThu
    public IActionResult ThemCauThu()
    {
        return View();
    }

    // POST: ThemCauThu
    [HttpPost]
    public IActionResult ThemCauThu(Player player)
    {
        if (ModelState.IsValid)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            return RedirectToAction("DangKyDoiBong");
        }

        return View(player);
    }

    // GET: DangKy
    public IActionResult DangKy()
    {
        return View();
    }

    }
}