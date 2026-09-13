using CRT_Core.Interface;
using CRT_Data.Interface;
using CRT_Entity;  
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 

namespace CRT_Core
{
    public class CoreRecords : ICoreRecords
    {
        private readonly IRecords _Records;

        public CoreRecords(IRecords records)
        {
            _Records = records;
        }

        public Task<int> RecInsert(Books obj)
        {
            return _Records.RecInsert(obj);
        }
        public async Task<bool> CourseCheckedName(string CourseName)
        {
            return await _Records.CourseCheckedName(CourseName);
        }

        //Employee
        public Task<int> EmpInsert(Employee obj)
        {
            return _Records.EmpInsert(obj);
        }

        public async Task<bool> EmpCheckedName(string CourseName)
        {
            return await _Records.EmpCheckedName(CourseName);
        }

        public async Task<List<Employee>> GetEmpList()
        {
            return await _Records.GetEmpList();
        }
        public async Task<Employee> GetEditEmp(int id)
        {
            return await _Records.GetEditEmp(id);
        }
        public async Task<int> DeletedEmp(int id)
        {
            return await _Records.DeletedEmp(id);
        }
        public async Task<List<Roles>> GetRolls()
        {
            return await _Records.GetRolls();
        }
        public async Task<List<UspDisplayProc>> GetRolls2()
        {
            return await _Records.GetRolls2();
        }
        public Task<int> NewAdmin(AdminAccess obj)
        {
            return _Records.NewAdmin(obj);
        }
        public async Task<bool> AdminCheckedName(string CourseName)
        {
            return await _Records.AdminCheckedName(CourseName);
        }

        public async Task<AdminAccess> UserLogin(AdminAccess obj)
        {
            return await _Records.UserLogin(obj);
        }
        public async Task<AdminAccess> GetEmployeeById(int Userid)
        {
            return await _Records.GetEmployeeById(Userid);
        }
        public async Task<List<AdminAccess>> GetAdminList()
        {
            return await _Records.GetAdminList();
        }

        public Task<int> ActiveAccount(int UserRId, bool IsActive)
        {
            return _Records.ActiveAccount(UserRId, IsActive);
        }
        public Task<int> AccountStatus(int UserRId, bool IsActive)
        {
            return _Records.AccountStatus(UserRId, IsActive);
        }
        public async Task<dynamic> GetDynamiMenu(int UserRId)
        {
            return await _Records.GetDynamiMenu(UserRId);
        }

        public Task<long> JobInsert(JobPost obj)
        {
            return _Records.JobInsert(obj);
        }

        
        public async Task<List<JobPost>> GetJobList()
        {
            return await _Records.GetJobList();
        }
        public async Task<JobPost> GetEditJob(long id)
        {
            return await _Records.GetEditJob(id);
        }
        public async Task<long> DeletedJob(long id)
        {
            return await _Records.DeletedJob(id);
        }
    }
}
