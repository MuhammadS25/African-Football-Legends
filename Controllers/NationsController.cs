using African_Football_Legends.Data;
using African_Football_Legends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace African_Football_Legends.Controllers
{
	public class NationsController : Controller
	{
		ApplicationDbContext _context;
		IWebHostEnvironment _env;

		public NationsController(ApplicationDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}

		[HttpGet]
		public IActionResult GetIndexView()
		{
			return View("Index", _context.Nations.OrderBy(n => n.FifaRanking).ToList());
		}

		[HttpGet]
		public IActionResult GetDetailsView(int id)
		{
			Nation natID = _context.Nations.Include(n => n.Players).FirstOrDefault(n => n.Id == id);
			return View("Details", natID);
		}


		[HttpGet]
		public IActionResult GetCreateView()
		{
			return View("Create");
		}

		[HttpPost]
		public IActionResult PostCreate(Nation nation, IFormFile? imageFormFile)
		{
			if (imageFormFile is not null)
			{
				Guid guid = Guid.NewGuid();// 1- generate unique id
				string extension = Path.GetExtension(imageFormFile.FileName);// 2- get extension
				string fileName = $"{guid}{extension}";// 3- generate file name
				nation.ImageUrl = $"\\images\\{fileName}";
				string imagePath = Path.Combine(_env.WebRootPath, "images", fileName);// 4- generate path

				using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))// 5- create file
				{
					imageFormFile.CopyTo(fileStream);// 6- copy image to file

				}
			}

			else
				nation.ImageUrl = "\\images\\No_Image.png";

			if (ModelState.IsValid)
			{
				_context.Nations.Add(nation);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			return View("Create");
		}

		[HttpGet]
		public IActionResult GetEditView(int id)
		{
			Nation natID = _context.Nations.FirstOrDefault(n => n.Id == id);

			if (natID is null)
				return NotFound($"Nation with {id} is not found.");

			return View("Edit", natID);
		}

		[HttpPost]
		public IActionResult PostEdit(Nation nation, IFormFile? imageFormFile)
		{
			if (imageFormFile is not null)
			{
				if (nation.ImageUrl != "\\images\\No_Image.png")
				{
					string oldImgPath = _env.WebRootPath + nation.ImageUrl;

					if (System.IO.File.Exists(oldImgPath))
						System.IO.File.Delete(oldImgPath);
				}

				Guid guid = Guid.NewGuid();// 1- generate unique id
				string extension = Path.GetExtension(imageFormFile.FileName);// 2- get extension
				string fileName = $"{guid}{extension}";// 3- generate file name
				nation.ImageUrl = $"\\images\\{fileName}";
				string imagePath = Path.Combine(_env.WebRootPath, "images", fileName);// 4- generate path

				using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))// 5- create file
				{
					imageFormFile.CopyTo(fileStream);// 6- copy image to file

				}
			}

			if (ModelState.IsValid)
			{
				_context.Nations.Update(nation);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
				return View("Edit");
		}

		[HttpGet]
		public IActionResult GetDeleteView(int id)
		{
			Nation natID = _context.Nations.Include(n => n.Players).FirstOrDefault(n => n.Id == id);

			if (natID is null)
				return NotFound($"Nation with {id} is not found.");

			return View("Delete", natID);
		}

		[HttpPost]
		public IActionResult PostDelete(int id)
		{
			Nation natID = _context.Nations.FirstOrDefault(n => n.Id == id);
			if (natID is null)
				return NotFound($"Nation with {id} is not found.");

			else
			{
				if (natID.ImageUrl is not "\\images\\No_Image.png")
				{
					string imagePath = _env.WebRootPath + natID.ImageUrl;
					if (System.IO.File.Exists(imagePath))
						System.IO.File.Delete(imagePath);
				}

				_context.Nations.Remove(natID);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
		}
	}
}
