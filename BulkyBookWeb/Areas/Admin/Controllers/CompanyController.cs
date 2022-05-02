﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;
    public CompanyController(IUnitOfWork db)
    {
        _unitOfWork = db;
       
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Upsert(int? id)
    {
        Company company = new();
        if (id == null || id == 0)
        {
            return View(company);
        }
        else
        {
            company=_unitOfWork.Company.GetFirstOrDefault(u=>u.Id == id);
            return View(company);
        }
        
    }
    //Post
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public IActionResult Upsert(Company obj, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            if (obj.Id == 0)
            {
                _unitOfWork.Company.Add(obj);
                TempData["success"] = " Product added successfully";
            }
            else
            {
                _unitOfWork.Company.Update(obj);
                TempData["success"] = " Product added successfully";
            }

            _unitOfWork.Save();
           
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    #region API CALLS

    [HttpGet]

    public IActionResult GetAll()
    {
        var companyList = _unitOfWork.Company.GetAll();
        return Json(new { data = companyList });

    }
    [HttpDelete]
    public IActionResult Delete(int? id) 
    {
        var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id); 
        if (obj == null)
        {
            return Json(new {success=false, message = "Error while deleting"});
        }
       
        _unitOfWork.Company.Remove(obj); ;
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
    }
    #endregion
}