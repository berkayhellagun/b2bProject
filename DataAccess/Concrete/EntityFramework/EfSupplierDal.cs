using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSupplierDal : EfEntityRepositoryBase<Supplier, DBContext>//, ISupplierDal
    {
        public SupplierDetail GetSupplierDetail(int id)
        {
            using (var db = new DBContext())
            {
                var result = from supplier in db.Suppliers
                             //join category in db.Categories on supplier.CategoryId equals category.Id
                             join user in db.Users on supplier.SupplierContactPersonId equals user.Id
                             where supplier.Id == id
                             select new SupplierDetail
                             {
                                 Id = supplier.Id,
                                 SupplierDescription = supplier.SupplierDescription,
                                 //CategoryId = category.Id,
                                 //CategoryName = category.Name,
                                 EmployeeCount = supplier.EmployeeCount,
                                 SupplierContactPersonId = supplier.SupplierContactPersonId,
                                 PersonName = user.FirstName + " " + user.LastName,
                                 PersonEmail = user.Email,
                                 SupplierCountry = supplier.SupplierCountry,
                                 SupplierFoundYear = supplier.SupplierFoundYear,
                                 SupplierGiro = supplier.SupplierGiro,
                                 SupplierMail = supplier.SupplierMail,
                                 SupplierName = supplier.SupplierName,
                                 SupplierTaxNo = supplier.SupplierTaxNo,
                                 SupplierTelephoneNumber = supplier.SupplierTelephoneNumber
                             };
                return result.First();
            }
        }
    }
}
