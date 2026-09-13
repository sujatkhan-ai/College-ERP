using System;
using System.Collections.Generic;
using System.Text;
using CRT_Data.Interface;
using Microsoft.EntityFrameworkCore;
using CRT_Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Dynamic;
using System.IO;

namespace CRT_Data
{
    public class Records:IRecords
    {
        private readonly SKContextFile _sKContextFile; 
           
        public Records(SKContextFile sKContextFile)
        {
            _sKContextFile = sKContextFile; 
        }
        public async Task<int> RecInsert(Books obj)
        {
            try
            {
                if (obj.bookid == 0)
                {
                    _sKContextFile.Add(obj);
                    await _sKContextFile.SaveChangesAsync();
                }
                else
                {
                    var collegedate = await _sKContextFile.Books.FirstOrDefaultAsync(x => x.bookid == obj.bookid);
                    if (collegedate != null)
                    {
                        collegedate.bookname = obj.bookname;
                        collegedate.bfile = obj.bfile; 
                        await _sKContextFile.SaveChangesAsync();
                    }

                }

                return obj.bookid;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<bool> CourseCheckedName(string CourseName)
        {
            var CoursenameChecked = await _sKContextFile.Books.FirstOrDefaultAsync(y => y.bookname == CourseName);
            if (CoursenameChecked != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Employeee
        public async Task<int> EmpInsert(Employee obj)
        {
            try
            {
                if (obj.empId == 0)
                {
                    _sKContextFile.Add(obj);
                    await _sKContextFile.SaveChangesAsync();
                }
                else
                {
                    var collegedate = await _sKContextFile.Employees.FirstOrDefaultAsync(x => x.empId == obj.empId);
                    if (collegedate != null)
                    {
 
                        collegedate.empName = obj.empName; 
                        collegedate.empEmail = obj.empEmail;
                        collegedate.empGender = obj.empGender;
                        collegedate.empMobileNo = obj.empMobileNo;
                        collegedate.empState = obj.empState;
                        collegedate.empCity = obj.empCity;
                        collegedate.empEmpTypes = obj.empEmpTypes;
                        collegedate.empExpYears = obj.empExpYears;  
                        collegedate.empQualification = obj.empQualification;
                        collegedate.empApplyFor = obj.empApplyFor;
                        collegedate.empResume = obj.empResume;
                        collegedate.empDate = obj.empDate; 
                        await _sKContextFile.SaveChangesAsync();
                    }

                }

                return obj.empId;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<bool> EmpCheckedName(string CourseName)
        {
            var CoursenameChecked = await _sKContextFile.Employees.FirstOrDefaultAsync(y => y.empEmail == CourseName);
            if (CoursenameChecked != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<Employee>> GetEmpList()
        {
            return await _sKContextFile.Employees.ToListAsync();
        }

        public async Task<Employee> GetEditEmp(int id)
        {
            return await _sKContextFile.Employees.FirstOrDefaultAsync(x => x.empId == id);
        }
        public async Task<int> DeletedEmp(int id)
        {
            var deletedCourse = await _sKContextFile.Employees.FirstOrDefaultAsync(x => x.empId == id);
            if (deletedCourse != null)
            {
                _sKContextFile.Remove(deletedCourse);
                await _sKContextFile.SaveChangesAsync();

                return deletedCourse.empId;
            }
            else
            {
                return 0;
            }

        }
        //Login
        public async Task<AdminAccess> UserLogin(AdminAccess obj)
        {
            var employeeresponse = await _sKContextFile.AdminAccesses.FirstOrDefaultAsync(x => x.UserName == obj.UserName && x.Password == obj.Password);
            if (employeeresponse != null)
            {
                return employeeresponse;
            }
            else
            {
                return null;
            }
        }
        public async Task<AdminAccess> GetEmployeeById(int Userid)
        {
            return await _sKContextFile.AdminAccesses.FirstOrDefaultAsync(y => y.UserRId == Userid);
        }

        public async Task<List<Roles>> GetRolls()
        {
            return await _sKContextFile.Roles.ToListAsync();
        }

        public async Task<List<UspDisplayProc>> GetRolls2()
        {
            return await _sKContextFile.UspDisplayProcs.ToListAsync();
        }

        public async Task<int> NewAdmin(AdminAccess obj)
        {
            try
            {
                if (obj.UserRId == 0)
                {
                    _sKContextFile.Add(obj);
                    await _sKContextFile.SaveChangesAsync();
                }
                else
                {
                    var collegedate = await _sKContextFile.AdminAccesses.FirstOrDefaultAsync(x => x.UserRId == obj.UserRId);
                    if (collegedate != null)
                    {
                        collegedate.UserName = obj.UserName;
                        collegedate.Password = obj.Password;
                        collegedate.IsActive = obj.IsActive;
                        collegedate.Rollid = obj.Rollid;
                        collegedate.CreatedBy = obj.CreatedBy;
                        collegedate.IsAccount = obj.IsAccount;

                        await _sKContextFile.SaveChangesAsync();
                    }

                }

                return obj.UserRId;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<bool> AdminCheckedName(string CourseName)
        {
            var CoursenameChecked = await _sKContextFile.AdminAccesses.FirstOrDefaultAsync(y => y.UserName == CourseName);
            if (CoursenameChecked != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<AdminAccess>> GetAdminList()
        {
            return await _sKContextFile.AdminAccesses.ToListAsync();
        }
      
        public async Task<int> ActiveAccount(int UserRId,bool IsActive)
        {
            try
            { 
                    var collegedate = await _sKContextFile.AdminAccesses.FirstOrDefaultAsync(x => x.UserRId == UserRId);
                    if (collegedate != null)
                    {
                        collegedate.IsActive = IsActive; 
                        await _sKContextFile.SaveChangesAsync();
                    }

               
                return UserRId;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<int> AccountStatus(int UserRId, bool IsActive)
        {
            try
            {
                var collegedate = await _sKContextFile.AdminAccesses.FirstOrDefaultAsync(x => x.UserRId == UserRId);
                if (collegedate != null)
                {
                    collegedate.IsAccount = IsActive;
                    await _sKContextFile.SaveChangesAsync();
                }


                return UserRId;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<dynamic> GetDynamiMenu(int UserRId)
        {


            var menulist = await _sKContextFile.AdminAccesses.Where(x => x.UserRId == UserRId).ToListAsync();
          
            dynamic menumode = new ExpandoObject(); 
            menumode.AdminAccesses = menulist; 
            return menumode;
        }

        public async Task<long> JobInsert(JobPost obj)
        {
            try
            {
                if (obj.jobPid == 0)
                {
                    _sKContextFile.Add(obj);
                    await _sKContextFile.SaveChangesAsync();
                }
                else
                {
                    var collegedate = await _sKContextFile.JobPosts.FirstOrDefaultAsync(x => x.jobPid == obj.jobPid);
                    if (collegedate != null)
                    {

                        collegedate.jobTitle = obj.jobTitle;
                        collegedate.jobExp = obj.jobExp;
                        collegedate.jobLocation = obj.jobLocation;
                        collegedate.jobType = obj.jobType;
                        collegedate.jobSkills = obj.jobSkills;
                        collegedate.jobDescription = obj.jobDescription;
                        collegedate.Remarks = obj.Remarks;
                        collegedate.jobDate = obj.jobDate; 
                        await _sKContextFile.SaveChangesAsync();
                    }

                }

                return obj.jobPid;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
         
        public async Task<List<JobPost>> GetJobList()
        {
            return await _sKContextFile.JobPosts.ToListAsync();
        }

        public async Task<JobPost> GetEditJob(long id)
        {
            return await _sKContextFile.JobPosts.FirstOrDefaultAsync(x => x.jobPid == id);
        }
        public async Task<long> DeletedJob(long id)
        {
            var deletedCourse = await _sKContextFile.JobPosts.FirstOrDefaultAsync(x => x.jobPid == id);
            if (deletedCourse != null)
            {
                _sKContextFile.Remove(deletedCourse);
                await _sKContextFile.SaveChangesAsync();

                return deletedCourse.jobPid;
            }
            else
            {
                return 0;
            }

        }
    }
     
}
