using African_Football_Legends.Data;
using African_Football_Legends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace African_Football_Legends.Controllers
{
    public class PlayersController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _env;

        public PlayersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _env = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetIndexView()
        {
            var viewModel = new PlayerSearchViewModel
            {
                Players = _context.Players.OrderByDescending(p => p.International_Caps).ToList(),
            };

            ViewBag.Positions = Enum.GetValues(typeof(Player.PlayerPosition))
                                    .Cast<Player.PlayerPosition>()
                                    .Select(p => new SelectListItem
                                    {
                                        Value = p.ToString(),
                                        Text = p.ToString()
                                    });

            ViewBag.Nations = _context.Nations.ToList();

            return View("Index", viewModel);
        }

        public IActionResult FilteredIndexView(PlayerSearchViewModel searchModel)
        {
            var players = _context.Players.Include(p => p.Nation).AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                players = players.Where(p => p.FullName.Contains(searchModel.SearchTerm));
            }

            if (searchModel.PositionFilter != 0) // Enum values start from 0
            {
                players = players.Where(p => p.Position == searchModel.PositionFilter);
            }

            if (searchModel.NationFilter > 0)
            {
                players = players.Where(p => p.NationId == searchModel.NationFilter);
            }

            if (searchModel.byGoals)
            {
				players = players.OrderByDescending(p => p.International_Goals);
			}
			else
            {
				players = players.OrderByDescending(p => p.International_Caps);
			}

            var viewModel = new PlayerSearchViewModel
            {
                Players = players.ToList(),
                SearchTerm = searchModel.SearchTerm,
                PositionFilter = searchModel.PositionFilter,
                NationFilter = searchModel.NationFilter
            };

            ViewBag.Positions = Enum.GetValues(typeof(Player.PlayerPosition))
                                    .Cast<Player.PlayerPosition>()
                                    .Select(p => new SelectListItem
                                    {
                                        Value = p.ToString(),
                                        Text = p.ToString()
                                    });

            ViewBag.Nations = _context.Nations.ToList();

            return View("Index", viewModel);
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Player playerID = _context.Players.Include(p => p.Nation).FirstOrDefault(p => p.Id == id);
            return View("Details", playerID);
        }

        [HttpGet]
        public IActionResult GetCreateView()
        {
            ViewBag.Nations = _context.Nations.ToList();
            return View("Create");
        }

        [HttpPost]
        public IActionResult PostCreate(Player player, IFormFile? imageFormFile)
        {
            if (imageFormFile is not null)
            {
                Guid guid = Guid.NewGuid();// 1- generate unique id
                string extension = Path.GetExtension(imageFormFile.FileName);// 2- get extension
                string fileName = $"{guid}{extension}";// 3- generate file name
                player.ImageUrl = $"\\images\\{fileName}";
                string imagePath = Path.Combine(_env.WebRootPath, "images", fileName);// 4- generate path

                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))// 5- create file
                {
                    imageFormFile.CopyTo(fileStream);// 6- copy image to file

                }
            }

            else
                player.ImageUrl = "\\images\\No_Image.png";

            if (player.International_Caps > 200 || player.International_Caps < 1)
                ModelState.AddModelError(string.Empty, "International caps must be between 1 and 200");

            if (player.RetireDate > 2023 || player.RetireDate < 1900)
                ModelState.AddModelError(string.Empty, "Retire Year must be between 1900 and 2023");

            if (player.Shirt_Number < 1 || player.Shirt_Number > 99)
                ModelState.AddModelError(string.Empty, "Shirt Number must be between 1 and 99");

            if (player.International_Goals < 0 || player.International_Goals > 100)
                ModelState.AddModelError(string.Empty, "Goals must be between 0 and 99");



            if (ModelState.IsValid)
            {
                _context.Players.Add(player);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }

            ViewBag.Nations = _context.Nations.ToList();
            return View("Create");
        }

        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Player playerID = _context.Players.FirstOrDefault(e => e.Id == id);

            if (playerID is null)
                return NotFound($"Player with {id} is not found.");

            ViewBag.Nations = _context.Nations.ToList();
            return View("Edit", playerID);
        }

        [HttpPost]
        public IActionResult PostEdit(Player player, IFormFile? imageFormFile)
        {
            if (imageFormFile is not null)
            {
                if (player.ImageUrl != "\\images\\No_Image.png")
                {
                    string oldImgPath = _env.WebRootPath + player.ImageUrl;

                    if (System.IO.File.Exists(oldImgPath))
                        System.IO.File.Delete(oldImgPath);
                }

                Guid guid = Guid.NewGuid();// 1- generate unique id
                string extension = Path.GetExtension(imageFormFile.FileName);// 2- get extension
                string fileName = $"{guid}{extension}";// 3- generate file name
                player.ImageUrl = $"\\images\\{fileName}";
                string imagePath = Path.Combine(_env.WebRootPath, "images", fileName);// 4- generate path

                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))// 5- create file
                {
                    imageFormFile.CopyTo(fileStream);// 6- copy image to file

                }
            }

            if (player.International_Caps > 200 || player.International_Caps < 1)
                ModelState.AddModelError(string.Empty, "International caps must be between 1 and 200");

            if (player.RetireDate > 2023 || player.RetireDate < 1900)
                ModelState.AddModelError(string.Empty, "Retire Year must be between 1900 and 2023");

            if (player.Shirt_Number < 1 || player.Shirt_Number > 99)
                ModelState.AddModelError(string.Empty, "Shirt Number must be between 1 and 99");

            if (player.International_Goals < 0 || player.International_Goals > 100)
                ModelState.AddModelError(string.Empty, "Goals must be between 0 and 99");

            if (ModelState.IsValid)
            {
                _context.Players.Update(player);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.Nations = _context.Nations.ToList();
                return View("Edit");
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Player playerID = _context.Players.FirstOrDefault(e => e.Id == id);

            if (playerID is null)
                return NotFound($"Player with {id} is not found.");

            return View("Delete", playerID);
        }

        [HttpPost]
        public IActionResult PostDelete(int id)
        {
            Player playerID = _context.Players.FirstOrDefault(e => e.Id == id);
            if (playerID is null)
                return NotFound($"Player with {id} is not found.");

            else
            {
                if (playerID.ImageUrl is not "\\images\\No_Image.png")
                {
                    string imagePath = _env.WebRootPath + playerID.ImageUrl;
                    if (System.IO.File.Exists(imagePath))
                        System.IO.File.Delete(imagePath);
                }

                _context.Players.Remove(playerID);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
        }

    }
}
