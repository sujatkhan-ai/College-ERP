using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRT_Entity; 

namespace CRT_Data.Interface
{
    public interface IRecords
    {
       public Task<int> RecInsert(Books obj);

       Task<bool> CourseCheckedName(string CourseName);
        //Employee
        Task<int> EmpInsert(Employee obj);
        Task<bool> EmpCheckedName(string CourseName);
        Task<List<Employee>> GetEmpList();
        Task<Employee> GetEditEmp(int id);
        Task<int> DeletedEmp(int id);

        Task<List<Roles>> GetRolls();
        Task<List<UspDisplayProc>> GetRolls2();

        public Task<int> NewAdmin(AdminAccess obj);
        Task<bool> AdminCheckedName(string CourseName);

        Task<AdminAccess> UserLogin(AdminAccess obj);
        Task<AdminAccess> GetEmployeeById(int Userid);
        Task<List<AdminAccess>> GetAdminList();
           
        Task<int> ActiveAccount(int UserRId, bool IsActive);

        Task<int> AccountStatus(int UserRId, bool IsActive);
        
        Task<dynamic> GetDynamiMenu(int UserRId);

        Task<long> JobInsert(JobPost obj); 
        Task<List<JobPost>> GetJobList();
        Task<JobPost> GetEditJob(long id);
        Task<long> DeletedJob(long id);
    }
}
