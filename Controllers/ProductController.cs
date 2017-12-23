using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DINGSOMETHING.Models.BusinessAccess;
using DINGSOMETHING.Models.Helper;


namespace DINGSOMETHING.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private Product productOperation;

        public ProductController(){
            productOperation = new Product();
        }

        public IActionResult Index(){

            ViewData["List"] = productOperation.Get();

            
            return View();
        }


        public IActionResult Create(){

            return View();
        }

        [HttpPost]
        public IActionResult Save(Product product){

            var imgFile = HttpContext.Request.Form.Files[0];
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Upload");
            string file = Path.Combine(dirPath , imgFile.FileName);

            if(!Directory.Exists(dirPath)) {
                Directory.CreateDirectory(dirPath);
            }

            if(System.IO.File.Exists(file)){
                System.IO.File.Delete(file);
            }

            using (FileStream stream = new FileStream(file , FileMode.CreateNew ,FileAccess.Write)) {
                imgFile.CopyTo(stream);
            }
            
            Product data = product;
            data.CatalogId = EncryptHelper.DecryptString(product.CatalogId);
            data.CatalogName = 
                product.GetCatalog().SingleOrDefault(p => p.Id == new Guid(data.CatalogId)).Name;
            data.Img = Path.GetFileName(imgFile.FileName);
            productOperation.Create(data);

            return Redirect("Index");
        }

        public IActionResult GetFile(string id) {
            
            var item = productOperation.GetById(id);
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Upload", item.Img);

            FileStream stream = System.IO.File.OpenRead(path);

            return File(stream, GetContentType(path), Path.GetFileName(path));
        }

        public IActionResult GetCatalog() {

            return Json(productOperation.GetCatalogSelectList());
        }

        public IActionResult GetProductCatalog(string CatalogId){
            if(string.IsNullOrEmpty(CatalogId)){
                return Json(productOperation.GetEmptyList());
            }

            string productId = EncryptHelper.DecryptString(CatalogId);

            return Json(productOperation.GetProductByCatalog(productId));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes() {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
            };
        }


        private string WriteFile(IFormFile imgFile){
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Upload");
            string file = Path.Combine(dirPath , imgFile.FileName);

            if(!Directory.Exists(dirPath)) {
                Directory.CreateDirectory(dirPath);
            }

            try
            {
                using (var stream = new FileStream(file, FileMode.Create))
                {
                    imgFile.CopyToAsync(stream);
                }

                return file;
            }
            catch (Exception err)
            {
                throw err;
            }

        }



    }

}