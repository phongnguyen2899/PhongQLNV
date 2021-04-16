using DATA.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNV.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QLNV.Controllers
{
    public class HomeController : Controller
    {

        private IWebHostEnvironment _hostingEnvironment;
        public readonly dbcontext _context;
        public HomeController(IWebHostEnvironment environment)
        {
            _context = new dbcontext();
            _hostingEnvironment = environment;
        }
       
        public IActionResult Index(string search)
        {
            IQueryable<NhanvienViewModel> data=null;
            data= from x in _context.Nhanviens
                  join c in _context.Chucvus on x.ChucvuId equals c.Id
                  join v in _context.Vitris on x.VitriId equals v.Id
                  select new NhanvienViewModel
                  {
                      Id = x.Id,
                      Tenchucvu = c.Tenchucvu,
                      Tenvitri = v.Tenvitri,
                      Ten = x.Ten,
                      Ngaysinhh = x.Ngaysinhh,
                      SDT = x.SDT,
                      CV = x.CV,
                      Diachi = x.Diachi,
                      Email = x.Email,
                      Nguoigioithieu = _context.Nhanviens.Where(a => a.Id == x.Nguoigioithieu).FirstOrDefault().Ten
                  };

            if (search == null)
            {
                data.ToList();
            }
            else
            {
               data= data.Where(x => x.Ten.Contains(""+search+""));
            }

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.tenNV = _context.Nhanviens.ToList();
            ViewBag.vitri = _context.Vitris.ToList();
            ViewBag.chucvu = _context.Chucvus.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Nhanvien model, int vitri, int nguoigioithieu, int chucvu, IFormFile filecv)
        {
           
            if (filecv != null)
            {
                //Get path
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                string filePath = Path.Combine(uploads, filecv.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    filecv.CopyToAsync(fileStream);
                }
                model.CV = filecv.FileName;
            }
            
            model.Nguoigioithieu = nguoigioithieu;
            
            model.VitriId = vitri;
            model.ChucvuId = chucvu;

            _context.Nhanviens.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Edit(int id)
        {

            ViewBag.vitri = _context.Vitris.ToList();
            ViewBag.chucvu = _context.Chucvus.ToList();
            var data = _context.Nhanviens.Where(x=>x.Id==id).FirstOrDefault();
            ViewBag.duoifile = Path.GetExtension(data.CV);
            //luu ten file ra session
            if (data.CV != null)
            {
                HttpContext.Session.SetString("oldfile", data.CV);
            }

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Nhanvien model,int vitri,int chucvu, IFormFile filecv)
        {

            //nếu có thay đổi file
            if (filecv != null)
            {
                // Lưu file mới
                Savefile(filecv);

                var data = _context.Nhanviens.Where(x => x.Id == model.Id).FirstOrDefault();
                if (data != null)
                {
                    if (HttpContext.Session.GetString("oldfile") != null)
                    {  
                        //xóa file cũ
                        Deletefile(HttpContext.Session.GetString("oldfile"));
                    }
                    if (data != null)
                    {
                        data.VitriId = vitri;
                        data.ChucvuId = chucvu;
                        data.CV = filecv.FileName;
                        data.Diachi = model.Diachi;
                        data.Email = model.Email;
                        data.Ngaysinhh = model.Ngaysinhh;
                        data.SDT = model.SDT;
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }
            else  //không thay đổi file
            {
                var data = _context.Nhanviens.Where(x => x.Id == model.Id).FirstOrDefault();
                if (data != null)
                {
                    data.VitriId = vitri;
                    data.ChucvuId = chucvu;
                    data.Diachi = model.Diachi;
                    data.Email = model.Email;
                    data.Ngaysinhh = model.Ngaysinhh;
                    data.SDT = model.SDT;
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            
        }

        public FileResult Dowloadfile(string id)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "uploads/") + id;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", id);
        }

        public JsonResult Delete(int id)
        {
            var data = _context.Nhanviens.Where(x => x.Id == id).FirstOrDefault();
            _context.Nhanviens.Remove(data);
            try
            {
                Deletefile(data.CV);
            }
            catch
            {

            }
            var result = _context.SaveChanges();


            if (result > 0)
            {
                return new JsonResult(
                        new
                        {
                            mes = "thanh cong"
                        }
                    ); 
            }
                return new JsonResult(
                        new
                        {
                            mes = "That bai"
                        }
                    );

        }



        public  void Savefile(IFormFile filecv)
        {
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            string filePath = Path.Combine(uploads, filecv.FileName);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                filecv.CopyToAsync(fileStream);
            }
        }
        public void Deletefile(string filecv)
        {
            string fileDirectory = Path.Combine(
                      Directory.GetCurrentDirectory(), "wwwroot/uploads/");

            string webRootPath = _hostingEnvironment.WebRootPath;

            var fileName = "";
            fileName = filecv;
            var fullPath = webRootPath + "/uploads/" + filecv;

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
