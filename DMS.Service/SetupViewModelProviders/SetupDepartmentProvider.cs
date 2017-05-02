using System;
using System.Linq;
using DMS.Areas.SetupArea.Models.SetupMasterViewModel;
using System.Data.Entity;
using DMS.DMS.Entities;
using DMS.CommonUtilities;


namespace DMS.Service.SetupViewModelProviders
{
    public interface ISetupDepartmentProvider
    {
        int Save(SetupDepartmentViewModel model);
        SetupDepartmentViewModel Edit(int departmentId);
        SetupDepartmentViewModel GetDepartmentList(string departmentName, int? page);
        int Delete(int departmentId);

    }
    public class SetupDepartmentProvider : ISetupDepartmentProvider
    {
        public readonly DMSEntities _ent;
        public SetupDepartmentProvider()
        {
            this._ent = new DMSEntities();
        }
        public int Save(SetupDepartmentViewModel model)
        {
            //var user = Userprovider.CustomeUserManager.GetUserDetails();
            SetupDepartment setupDepartmentEntity = new SetupDepartment();
            setupDepartmentEntity.DepartmentName = model.DepartmentName;

            var data = _ent.SetupDepartments.Where(x => x.DepartmentName == model.DepartmentName.Trim() && x.DeletedDate == null).ToList();
            if (data.Count > 0)
            {
                return 2;
            }
            if (model.DepartmentId > 0)
            {
                setupDepartmentEntity.Status = model.Status;
                setupDepartmentEntity.UpdatedBy = 1;// user.UserId;
                setupDepartmentEntity.DepartmentId = model.DepartmentId;
                setupDepartmentEntity.UpdatedDate = DateTime.UtcNow;
                _ent.Entry(setupDepartmentEntity).State = EntityState.Modified;
                _ent.Entry(setupDepartmentEntity).Property(x => x.CreatedBy).IsModified = false;
                _ent.Entry(setupDepartmentEntity).Property(x => x.CreatedDate).IsModified = false;

            }
            else
            {
                setupDepartmentEntity.Status = true;
                setupDepartmentEntity.CreatedBy = 1;// user.UserId;
                setupDepartmentEntity.CreatedDate = DateTime.UtcNow;
                _ent.Entry(setupDepartmentEntity).State = EntityState.Added;
            }
            try
            {
                _ent.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public SetupDepartmentViewModel GetDepartmentList(string departmentName, int? page)
        {

            int pageSize = 2;// Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);


            SetupDepartmentViewModel model = new SetupDepartmentViewModel();
            if (departmentName != "")
            {
                var pager = new Pager(_ent.SetupDepartments.Where(x => x.DeletedDate == null && x.DepartmentName.Contains(departmentName)).Count(), page);
                model.SetupDepartmentViewModelList = (from d in _ent.SetupDepartments
                                                      where d.DepartmentName.Contains(departmentName) && d.DeletedDate == null
                                                      select new SetupDepartmentViewModel
                                                      {
                                                          DepartmentId = d.DepartmentId,
                                                          DepartmentName = d.DepartmentName,
                                                          Status = d.Status ?? false
                                                      }).OrderBy(x => x.DepartmentId).
                                      Skip((pager.CurrentPage - 1) * pageSize).Take(pageSize).ToList();
                model.pager = pager;

                model.pager = pager;
            }
            else
            {
                var pager = new Pager(_ent.SetupDepartments.Where(x => x.DeletedDate == null).Count(), page);

                model.SetupDepartmentViewModelList = (from d in _ent.SetupDepartments
                                                      where d.DeletedDate == null
                                                      select new SetupDepartmentViewModel
                                                      {
                                                          DepartmentId = d.DepartmentId,
                                                          DepartmentName = d.DepartmentName,
                                                          Status = d.Status ?? false
                                                      }).OrderBy(x => x.DepartmentId).
                                      Skip((pager.CurrentPage - 1) * pageSize).Take(pageSize).ToList();
                model.pager = pager;
            }

            return model;
        }
        public int Delete(int departmentId)

        {
            //var user = Userprovider.CustomeUserManager.GetUserDetails();
            var data = _ent.SetupDepartments.Where(x => x.DepartmentId == departmentId).FirstOrDefault();
            if (data != null)
            {
                //data.DeletedBy = user.UserId;
                data.DeletedDate = DateTime.UtcNow;
                _ent.Entry(data).State = EntityState.Modified;
                try
                {
                    _ent.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {

                    return 0;
                }

            }
            return 0;

        }

        public SetupDepartmentViewModel Edit(int departmentId)
        {
            SetupDepartmentViewModel model = new SetupDepartmentViewModel();
            var data = _ent.SetupDepartments.Where(d => d.DepartmentId == departmentId &&d.DeletedDate==null).FirstOrDefault();
            if(data!=null)
            {
                model.DepartmentId = data.DepartmentId;
                model.DepartmentName = data.DepartmentName;
               
            }
            return model;
        }
    }

}
