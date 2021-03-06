using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers;
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;
    public ProductController(IUnitOfWork db, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = db;
        _hostEnvironment = hostEnvironment;
    }
    public IActionResult Index()
    {
        //  IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll(); 
        //  return View(objProductList);
        return View();
    }
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            product = new(),

            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            }),
            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.CoverType_Name,
                Value = i.CoverType_Id.ToString()
            }),
        };

        if (id == null || id == 0)
        {
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CoverTypeList"] = CoverTypeList;
            return View(productVM);
        }
        else
        {
            productVM.product=_unitOfWork.Product.GetFirstOrDefault(u=>u.Id == id);
            return View(productVM);
        }
        
    }
    //Post
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"Images\products");
                var extension = Path.GetExtension(file.FileName);
                if (obj.product.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                obj.product.ImageUrl = @"\Images\Products\" + fileName + extension;
            }
            if (obj.product.Id == 0)
            {
                _unitOfWork.Product.Add(obj.product);
            }
            else
            {
                _unitOfWork.Product.Update(obj.product);
            }

            _unitOfWork.Save();
            TempData["success"] = " Product added successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    #region API CALLS

    [HttpGet]

    public IActionResult GetAll()
    {
        var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
        return Json(new { data = productList });

    }
    [HttpDelete]
    public IActionResult Delete(int? id) 
    {
        var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id); 
        if (obj == null)
        {
            return Json(new {success=false, message = "Error while deleting"});
        }
        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }
        _unitOfWork.Product.Remove(obj); ;
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
    }
    #endregion
}