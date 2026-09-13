using CRT_Core.Interface;
using CRT_Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using CRT_Core;
using CRT_Entity;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ChayanikaRecruitTech.Controllers
{
      
    public class UserController : Controller
    {
        private readonly ICoreRecords _CoreRecords;
        private   SKContextFile _sKContextFile;
         
        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public UserController(ICoreRecords coreRecords, IHostingEnvironment environment, SKContextFile sKContextFile)
        {
            _CoreRecords = coreRecords; 
            _environment = environment;
            _sKContextFile = sKContextFile;
        }

        public IActionResult Users()
        {
            var sessionchk = HttpContext.Session.GetString("UserRId");
            if (sessionchk != null)
            {


                return View();

            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
      
        public async Task<IActionResult> Home()
        {
            var sessionchk = HttpContext.Session.GetString("UserRId");
            if (sessionchk != null)
            { 
                var UserRId = Convert.ToInt32(HttpContext.Session.GetString("UserRId"));
                dynamic menumode = new ExpandoObject();
                menumode = await _CoreRecords.GetDynamiMenu(UserRId);

                return View(menumode);
                 
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

           
        }
        public IActionResult Panel()
        {
            var sessionchk = HttpContext.Session.GetString("UserRId");
            if (sessionchk != null)
            {
                 

                return View();

            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
        }
        public IActionResult Menus()
        {

            var sessionchk = HttpContext.Session.GetString("UserRId");
            if (sessionchk != null)
            {


                return View();

            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetRolls()
        {
            try
            {
                var DataCourse = await _CoreRecords.GetRolls();
                var ResponseResult = DataCourse.Select(y => new
                {
                    y.Rollid,
                    y.Rolltitle
                    // userName = y.CreatedBy != 0 ? CLEmployee.GetEmployeeById(y.CreatedBy.Value)?.Result.Name : "",
                    // createdDate = y.CreatedDate != null ? y.CreatedDate.Value.ToString("dd-MMM-yyyy") : ""


                }).ToList();
                return Json(new { headtype = ResponseResult });
            }
            catch (Exception ex)
            {
                return Json(new { data = "" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetRolls2()
        {
            try
            {
                var DataCourse = await _CoreRecords.GetRolls2();
                var ResponseResult = DataCourse.Select(y => new
                {
                    y.Rollid,
                    y.Rolltitle
                    // userName = y.CreatedBy != 0 ? CLEmployee.GetEmployeeById(y.CreatedBy.Value)?.Result.Name : "",
                    // createdDate = y.CreatedDate != null ? y.CreatedDate.Value.ToString("dd-MMM-yyyy") : ""


                }).ToList();
                return Json(new { headtype = ResponseResult });
            }
            catch (Exception ex)
            {
                return Json(new { data = "" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> NewAdmin([FromBody] AdminAccess course)
        {
            try
            {
                if (course != null)
                {

                    if (course.UserRId != 0)
                    {
                        // course.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("Userid"));
                        course.CreatedDate = System.DateTime.Now;
                        var CollegeTypeResponse = await _CoreRecords.NewAdmin(course);
                        if (CollegeTypeResponse != 0)
                        {
                            return Json(new { message = "Admin details Updated Successfully", statusType = "success" });
                        }
                        else
                        {
                            return Json(new { message = "Please check entries type", statusType = "notsuccess" });
                        }
                    }
                    else
                    {

                        var CollegeRegistrationChecked = await _CoreRecords.AdminCheckedName(course.UserName);
                        if (CollegeRegistrationChecked == false)
                        {
                            // course.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("Userid"));
                            course.CreatedDate = System.DateTime.Now;
                            var CollegeTypeResponse = await _CoreRecords.NewAdmin(course);
                            if (CollegeTypeResponse != 0)
                            {
                                return Json(new { message = "Admin Details Added Successfully", statusType = "success" });
                            }
                            else
                            {
                                return Json(new { message = "Admin was not created ", statusType = "notsuccess" });
                            }
                        }
                        else
                        {
                            return Json(new { message = "This Email Id is already exists", statusType = "exists" });
                        }

                    }

                }
                else
                {
                    return Json(new { message = "Data not Found", statusType = "error" });
                }

            }
            catch (Exception ex)
            {

                return Json(new { message = "error" + ex.Message, statusType = "error" });

            }
        }
        
        [HttpPost]
        public JsonResult validateuser(string userid, string password)
        {
             
            var data = from c in _sKContextFile.AdminAccesses where c.UserName == userid && c.Password == password select c;
            if (data.Count() > 0)
                return Json(new { Success = true }, System.Web.Mvc.JsonRequestBehavior.AllowGet);
            else
                return Json(new { Success = false }, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
        [EnableCors("AnotherPolicy")]
        public IActionResult Login()
        {
            var sessionchk = HttpContext.Session.GetString("UserRId");
            if (sessionchk != null)
            {


                return RedirectToAction("Home", "User"); ;

            }
            else
            {
                return View();
            };
        }
        
        //public async Task<IActionResult> Panel(int id)
        //{
        //    var UserRId = Convert.ToInt32(HttpContext.Session.GetString("UserRId"));
        //    dynamic menumode = new ExpandoObject();
        //    menumode = await _CoreRecords.GetDynamiMenu(UserRId);
        //    return View(menumode);
        //}
        public async Task<IActionResult> SRSoftwareUserLogin(AdminAccess obj)
        { 
            try
            {
                var employeeLogin = await _CoreRecords.UserLogin(obj);
                if (employeeLogin != null && employeeLogin.Rollid==1 && employeeLogin.IsAccount==true)
                {
                    HttpContext.Session.SetString("UserRId", employeeLogin.UserRId.ToString());
                    //return RedirectToAction("Home","User", new { id = 7840012+employeeLogin.UserRId });
                    return RedirectToAction("Home","User");
                }
                else if (employeeLogin != null && employeeLogin.Rollid==2 && employeeLogin.IsAccount == true)
                {
                    HttpContext.Session.SetString("UserRId", employeeLogin.UserRId.ToString());
                    return RedirectToAction("Home", "User");
                }
                else
                {
                    ViewBag.Message = "User Name & Password not Valid";
                    return RedirectToAction("Login", "User");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "User Name & Password not Valid" + ex.Message;
                return RedirectToAction("Login", "User");
            }
        }

        //public async Task<IActionResult> Master(int id)
        //{
        //    var UserId = Convert.ToInt64(HttpContext.Session.GetString("Userid"));
        //    dynamic menumode = new ExpandoObject();
        //    menumode = await ISRSoftwareModule.GetDynamiMenu(UserId, id);
        //    return View(menumode);
        //}
        //public async Task<IActionResult> WelcomeAdminPanel(int id)
        //{
        //    dynamic menumode = new ExpandoObject();
        //    var UserName = Convert.ToInt64(HttpContext.Session.GetString("Userid"));
        //    menumode = await ISRSoftwareModule.GetDynamiMenu(UserName, id);
        //    return View(menumode);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAdminList()
        {
            try
            {
                var DataCourse = await _CoreRecords.GetAdminList();
                var ResponseResult = DataCourse.Select(y => new
                {
                    y.IsActive,
                    y.UserRId,
                    y.UserName, 
                    y.Password,
                    y.CreatedBy,
                    y.CreatedDate, 
                    y.Rollid,
                    y.IsAccount
                    // userName = y.CreatedBy != 0 ? CLEmployee.GetEmployeeById(y.CreatedBy.Value)?.Result.Name : "",
                    // createdDate = y.CreatedDate != null ? y.CreatedDate.Value.ToString("dd-MMM-yyyy") : ""


                }).ToList();
                //var gg = ResponseResult.ToList();
                return Json(new { draw = 1, recordsTotal = ResponseResult.Count, recordsFiltered = 10, data = ResponseResult });
            }
            catch (Exception ex)
            {
                return Json(new { draw = 1, recordsTotal = 0, recordsFiltered = 1, data = ex.Message });
            }

        }
        //ActiveAccount
      
        
        [HttpGet]
        public async Task<IActionResult> ActiveAccount(int UserRId, bool IsActive)
        {
            try
            {
                
                    if (UserRId != 0)
                    {
                        
                        var CollegeTypeResponse = await _CoreRecords.ActiveAccount(UserRId, IsActive);
                        if (CollegeTypeResponse != 0)
                        {
                            return Json(new { message = "Admin has now access credential", statusType = "success" });
                        }
                        else
                        {
                            return Json(new { message = "Please try later", statusType = "notsuccess" });
                        }
                    }
                    else
                    {

                        var CollegeRegistrationChecked=true;
                        if (CollegeRegistrationChecked == false)
                        {

                            var CollegeTypeResponse = 0;
                            if (CollegeTypeResponse != 0)
                            {
                                return Json(new { message = "Admin Added Successfully", statusType = "success" });
                            }
                            else
                            {
                                return Json(new { message = "Admin Not Added Successfully ", statusType = "notsuccess" });
                            }
                        }
                        else
                        {
                            return Json(new { message = "This Admin is already exists", statusType = "exists" });
                        }

                    }

                

            }
            catch (Exception ex)
            {

                return Json(new { message = "error" + ex.Message, statusType = "error" });

            }
        }

        [HttpGet] 
        public async Task<IActionResult> AccountStatus(int UserRId, bool IsActive)
        {
            try
            {

                if (UserRId != 0)
                {

                    var CollegeTypeResponse = await _CoreRecords.AccountStatus(UserRId, IsActive);
                    if (CollegeTypeResponse != 0)
                    {
                        return Json(new { message = "Admin has now access credential", statusType = "success" });
                    }
                    else
                    {
                        return Json(new { message = "Please try later", statusType = "notsuccess" });
                    }
                }
                else
                {

                    var CollegeRegistrationChecked = true;
                    if (CollegeRegistrationChecked == false)
                    {

                        var CollegeTypeResponse = 0;
                        if (CollegeTypeResponse != 0)
                        {
                            return Json(new { message = "Admin Added Successfully", statusType = "success" });
                        }
                        else
                        {
                            return Json(new { message = "Admin Not Added Successfully ", statusType = "notsuccess" });
                        }
                    }
                    else
                    {
                        return Json(new { message = "This Admin is already exists", statusType = "exists" });
                    }

                }



            }
            catch (Exception ex)
            {

                return Json(new { message = "error" + ex.Message, statusType = "error" });

            }
        }

    }
}
