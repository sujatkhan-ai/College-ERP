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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ChayanikaRecruitTech.Controllers
{
 
    public class RecruitController : Controller
    {
        private readonly ICoreRecords _CoreRecords;

        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public RecruitController(ICoreRecords coreRecords, IHostingEnvironment environment)
        {
            _CoreRecords = coreRecords; 
            _environment = environment;
        }
        public IActionResult Index()
        {
            var sessionchk = HttpContext.Session.GetString("UserRId");
            if (sessionchk != null)
            {


                return RedirectToAction("Home", "User"); ;

            }
            else
            {
                return RedirectToAction("Login", "User");
            };
        }

        public IActionResult Recruiters()
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

        public IActionResult Vacancies()
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
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> RecInsert([FromBody] Books books)
        {
            try
            {
                if (books.bookname != null)
                {

                    if (books.bookid != 0)
                    {

                        var CollegeTypeResponse = await _CoreRecords.RecInsert(books);
                        if (CollegeTypeResponse != 0)
                        {
                            return Json(new { message = "Book Information Updated Successfully", statusType = "success" });
                        }
                        else
                        {
                            return Json(new { message = "Please check entries type", statusType = "notsuccess" });
                        }
                    }
                    else
                    {

                        var CollegeTypeCheck = await _CoreRecords.CourseCheckedName(books.bookname);
                        //var filelist = HttpContext.Request.Form.Files;
                        //foreach(var file in filelist)
                        //{
                        //    var uploads = Path.Combine(_environment.WebRootPath, "files");
                        //    string filename = file.FileName;
                        //    using(var fileStream = new FileStream(Path.Combine(uploads,filename),FileMode.Create))
                        //    {
                        //        file.CopyToAsync(fileStream);
                        //    }


                        //}
                        if (CollegeTypeCheck == false)
                        {

                            var CollegeTypeResponse = await _CoreRecords.RecInsert(books);

                            if (CollegeTypeResponse != 0)
                            {

                                return Json(new { message = "Books Information Saved Successfully", statusType = "success" });
                            }
                            else
                            {
                                return Json(new { message = "Please check entries type", statusType = "notsuccess" });
                            }
                        }
                        else
                        {
                            return Json(new { message = "Books Information is already exists", statusType = "exists" });
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
        public IActionResult Uploadfile2()
        {
            string Result = string.Empty;
            // Iterate each files
            var Files = Request.Form.Files;
            foreach (var file in Files)
            {
                // Get the file name from the browser
                var fileName = System.IO.Path.GetFileName(file.FileName);

                // Get file path to be uploaded
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\books\\", fileName);

                // Check If file with same name exists and delete it
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Create a new local file and copy contents of uploaded file
                using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                    Result = "Pass";
                }
            }
            return Ok(Result);
 
        }
        public async Task<ActionResult> Uploadfile()
        {
            string Result = string.Empty;
            var Files = Request.Form.Files;
            foreach (IFormFile source in Files)
            {
                string fileName = source.Name + ".pdf";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\books\\", fileName);

                // string imagepath = GetActualPath(fileName);
                try
                {
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                    using (FileStream stream = System.IO.File.Create(filePath))
                    {
                        await source.CopyToAsync(stream);
                        Result = "Pass";
                        //  return Json(new { message = "Insert", statusType = "success" });
                    }
                }
                catch (Exception er)
                {
                    return Json(new { message = er });
                }
            }

            return Ok(Result);
        }
        public string GetActualPath(string fileName)
        {
            return Path.Combine(_environment.WebRootPath + "\\books\\cover\\", fileName);
        }

        [HttpPost()]
        public async Task<IActionResult> GetImage(string profile)
        {

            Byte[] b;
            b = await System.IO.File.ReadAllBytesAsync(Path.Combine(_environment.ContentRootPath, "books", $"{profile}"));
            return File(b, "image/jpeg");
        }
         
        [HttpGet]
        public async Task<IActionResult> GetFile(int? id)
        {
            //string fname = "tk@gmail.com.pdf";
            //Set the File Folder Path.
           
                var CourseObj = await _CoreRecords.GetEditEmp(id.Value);
                var filename = CourseObj.empResume;
                
               
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" +"\\books\\",filename);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path,FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                var contentType = "application/octet-stream";
                var fname = Path.GetFileName(path);

                return File(memory,contentType,fname);


                    //System.IO.File.WriteAllBytes(fileReadPath,filename);

                //byte[] filedata = System.IO.File.ReadAllBytes(OutputPath);
                //string contentType = GetContentType(OutputPath);



                //HttpContext.Response.Headers.Add("Content-Disposition", $"attachment;filename=" + FileName);

                //var content = new System.IO.MemoryStream(filedata);

                //return File(content, "application/pdf", OutputPath);
                //var image = System.IO.File.OpenRead(fileReadPath);
                //return File(image, "application/octet-stream", Path.GetFileName(fileReadPath));
                //return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));


             
        }
       
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        
        [HttpPost]  
        public async Task<IActionResult> EmpInsert([FromBody]Employee employee)
        {
            try
            {
                if (employee != null)
                {

                    if (employee.empId != 0)
                    {
                        // course.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("Userid"));
                        employee.empDate = System.DateTime.Now;
                        var CollegeTypeResponse = await _CoreRecords.EmpInsert(employee);
                        if (CollegeTypeResponse != 0)
                        {
                            return Json(new { message = "Employee Details Updated Successfully", statusType = "success" });
                        }
                        else
                        {
                            return Json(new { message = "Please check entries type", statusType = "notsuccess" });
                        }
                    }
                    else
                    {

                        var CollegeRegistrationChecked = await _CoreRecords.EmpCheckedName(employee.empEmail);
                        if (CollegeRegistrationChecked == false)
                        {
                            // course.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("Userid"));
                            employee.empDate = System.DateTime.Now; 
                            var CollegeTypeResponse = await _CoreRecords.EmpInsert(employee);
                            if (CollegeTypeResponse != 0)
                            {
                                return Json(new { message = "Employee Details Added Successfully", statusType = "success" });
                            }
                            else
                            {
                                return Json(new { message = "Employee Name Not Added Successfully ", statusType = "notsuccess" });
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

        [HttpGet]
        public async Task<IActionResult> GetEmpList()
        {
            try
            {
                var DataCourse = await _CoreRecords.GetEmpList();
                var ResponseResult = DataCourse.Select(y => new
                {
                    y.empId,
                    y.empName,
                    y.empEmail,
                    y.empMobileNo,
                    y.empGender,
                    y.empState,
                    y.empCity,
                    y.empEmpTypes,
                    y.empExpYears,
                    y.empQualification,
                    y.empApplyFor,
                    y.empResume,
                    y.empDate,
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

        [HttpGet]
        public async Task<IActionResult> GetEditEmp(int? id)
        {
            try
            {
                var CourseObj = await _CoreRecords.GetEditEmp(id.Value);

                return Json(new { datalist = CourseObj, statusType = "success" });

            }
            catch (Exception ex)
            {
                return Json(new { datalist = "Error" + ex.Message, statusType = "failded" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeletedEmp(int? id)
        {
            try
            {
                var CourseDeletedObj = await _CoreRecords.DeletedEmp(id.Value);

                if (CourseDeletedObj != 0)
                {
                    return Json(new { message = "Employee Data Deleted Successfully", statusType = "success" });
                }
                else
                {
                    return Json(new { message = "Please Check to use another records", statusType = "failded" });
                }



            }
            catch (Exception ex)
            {
                return Json(new { message = "Error" + ex.Message, statusType = "failded" });
            }
        }

        // Job Vacancies

        [HttpPost]
        public async Task<IActionResult> JobInsert([FromBody] JobPost employee)
        {
            try
            {
                if (employee != null)
                {

                    if (employee.jobPid != 0)
                    {
                        // course.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("Userid"));
                        employee.jobDate = System.DateTime.Now;
                        var CollegeTypeResponse = await _CoreRecords.JobInsert(employee);
                        if (CollegeTypeResponse != 0)
                        {
                            return Json(new { message = "Vacancies Details Updated Successfully", statusType = "success" });
                        }
                        else
                        {
                            return Json(new { message = "Please check entries type", statusType = "notsuccess" });
                        }
                    }
                    else
                    {

                        var CollegeRegistrationChecked = false;
                        if (CollegeRegistrationChecked == false)
                        {
                            // course.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("Userid"));
                            employee.jobDate = System.DateTime.Now;
                            var CollegeTypeResponse = await _CoreRecords.JobInsert(employee);
                            if (CollegeTypeResponse != 0)
                            {
                                return Json(new { message = "Vacancies Details Added Successfully", statusType = "success" });
                            }
                            else
                            {
                                return Json(new { message = "Vacancies Added Failed ", statusType = "notsuccess" });
                            }
                        }
                        else
                        {
                            return Json(new { message = "This Vacancy is already exists", statusType = "exists" });
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

        [HttpGet]
        public async Task<IActionResult> GetJobList()
        {
            try
            {
                var DataCourse = await _CoreRecords.GetJobList();
                var ResponseResult = DataCourse.Select(y => new
                {
                    y.jobPid,
                    y.jobTitle,
                    y.jobExp,
                    y.jobLocation,
                    y.jobType,
                    y.jobSkills,
                    y.jobDescription,
                    y.Remarks,
                    y.jobDate
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
    }
}
