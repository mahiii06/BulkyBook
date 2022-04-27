using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CoverTypeController : Controller
    {
        //private readonly ApplicationDbContext _db;
        //private readonly ICategoryRepository _db;
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> objCategoryList = _db.Categories; //retreive the category 
            //IEnumerable<Category> objCategoryList = _db.Category.GetAll(); //retreive the category 
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll(); //to use unityof work.
            return View(objCoverTypeList);
        }

        //Create Functionality
        //GET
        public IActionResult Create() 
        {
            return View();
        }
        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CoverType obj)  //in validation check model is valid or not (Require properties have or not)
        {
            //costom validation
           

            //check properties validation 
            if (ModelState.IsValid)     //havor on ModelState and check(Values>Result Values) if any propertie valid or not here 
            {
                //_db.Categories.Add(obj);   //add to category
                _unitOfWork.CoverType.Add(obj);   //after repo
                //_db.SaveChanges(); //simple use
                //_db.Save(); //after repo
                _unitOfWork.Save(); //after use unitofwork
                TempData["success"] = "CoverType created successfully";
                return RedirectToAction("Index"); 
            }
            return View(obj);
        }



        //Edit/Update Functionality 

        //GET
        public IActionResult Edit(int? id) 
        {
            if (id ==null || id==0)
            {
                return NotFound();
            }
            //way of retreive category
            //var categoryFromDb = _db.Categories.Find(id);  //based on the primary key it find  
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.ID == id); //not throw exception and return first element of the list 
            //var categoryFromDbFirst = _db.GetFirstOrDefault(u=>u.ID == id); //after repo
            var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u=>u.CoverType_Id== id); //after use unitofwork
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.ID == id); //throw an exception
            if (CoverTypeFromDb == null) 
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);
        }
        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken] 
        public IActionResult Edit(CoverType obj)  //in validation check model is valid or not (Require properties have or not)
        {
            //costom validation
          


            //check properties validation 
            if (ModelState.IsValid)     //havor on ModelState and check(Values>Result Values) if any propertie valid or not here 
            {
                //_db.Categories.Update(obj); //update for this 
                //_db.Update(obj); //after use repo
                _unitOfWork.CoverType.Update(obj); //after use unitofwork
                //_db.SaveChanges();
                //_db.Save(); //after use repo
                _unitOfWork.Save(); //after use unitofwork
                TempData["success"] = " CoverType updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //update functionality
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //way of retreive category
            //var categoryFromDb = _db.Categories.Find(id);  //based on the primary key it find  
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.ID == id); //not throw exception and return first element of the list 
            var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u=>u. CoverType_Id == id); //after use repo
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.ID == id); //throw an exception
            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);
        }

        //Post
        [HttpPost,ActionName("Delete")]
        [AutoValidateAntiforgeryToken]  
        public IActionResult DeletePOST(int? id)  //in validation check model is valid or not (Require properties have or not)
        {
            //var obj = _db.Categories.Find(id);
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.CoverType_Id == id); //add line after use repo
            if (obj == null)
            {
                return NotFound();
            }

            //_db.Categories.Remove(obj); //remove for this 
            _unitOfWork.CoverType.Remove(obj); //add after repo
            //_db.SaveChanges();
            _unitOfWork.Save(); //add after unitofwork
            TempData["success"] = " CoverType deleted successfully";
            return RedirectToAction("Index"); 
        }
    }
}
