using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Import.Entity;
using MySqlConnector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MISA.Import.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static List<Customer> customers = new List<Customer>();
        private IDbConnection dbConnection;

        private string connectionString = ""
                + "Host=47.241.69.179;"
                + "Port=3306;"
                + "User Id=dev;"
                + "Password=12345678;"
                + "Database = MF810-Import-TMQuy;"
                + "convert zero datetime=True";

        /// <summary>
        /// Import file excel
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("Import")]
        public IActionResult Import(IFormFile formFile)
        {
            if (formFile == null || formFile.Length < 0)
            {
                return BadRequest();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }
            GetAllCustomer(formFile);
            Validate(customers);
            var result = new
            {
                TotalRecord = customers.Count,
                Data = customers
            };
            return Ok(result);
        }

        /// <summary>
        /// Gửi dữ liệu lên database
        /// </summary>
        /// <returns></returns>
        [HttpPost("Insert")]
        public IActionResult Post()
        {
            var res = InsertCustomer(customers);
            var result = new
            {
                TotalRecord = customers.Count,
                Success = res,
                Data = customers
            };
            return Ok(result);
        }

        /// <summary>
        /// Đọc file excel
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        private List<Customer> GetAllCustomer(IFormFile formFile)
        {
            customers.Clear();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    for (int row = 3; row < worksheet.Dimension.Rows; row++)
                    {
                        Customer c = new Customer(
                            worksheet.Cells[row, 2].Value == null ? null : worksheet.Cells[row, 2].Value.ToString(),
                            worksheet.Cells[row, 1].Value == null ? null : worksheet.Cells[row, 1].Value.ToString(),
                            worksheet.Cells[row, 3].Value == null ? null : worksheet.Cells[row, 3].Value.ToString(),
                            worksheet.Cells[row, 4].Value == null ? null : worksheet.Cells[row, 4].Value.ToString(),
                            GetCustomerGroupId(worksheet.Cells[row, 4].Value == null ? null : worksheet.Cells[row, 4].Value.ToString()),
                            worksheet.Cells[row, 5].Value == null ? null : worksheet.Cells[row, 5].Value.ToString(),
                            FormatDob(worksheet.Cells[row, 6].Value == null ? null : worksheet.Cells[row, 6].Value.ToString()),
                            worksheet.Cells[row, 7].Value == null ? null : worksheet.Cells[row, 7].Value.ToString(),
                            worksheet.Cells[row, 8].Value == null ? null : worksheet.Cells[row, 8].Value.ToString(),
                            worksheet.Cells[row, 9].Value == null ? null : worksheet.Cells[row, 9].Value.ToString(),
                            worksheet.Cells[row, 10].Value == null ? null : worksheet.Cells[row, 10].Value.ToString(),
                            worksheet.Cells[row, 11].Value == null ? null : worksheet.Cells[row, 11].Value.ToString(),
                            null
                        );
                        customers.Add(c);
                    }
                }
                return customers;
            }
        }
        /// <summary>
        /// Lấy dữ liệu CustomerGroupId từ customerGroupName
        /// </summary>
        /// <param name="customerGroupName"></param>
        /// <returns>CustomerGroupId</returns>
        private Guid? GetCustomerGroupId(string customerGroupName)
        {
            try
            {
                using (dbConnection = new MySqlConnection(connectionString))
                {
                    var sqlCommand = "Proc_GetCustomerGroup";
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@CustomerGroupName", customerGroupName);
                    var res = dbConnection.QueryFirstOrDefault<Guid>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                    if (res.Equals(Guid.Empty))
                    {
                        return null;
                    }
                    else
                    {
                        return res;
                    }
                   
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Format date
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        private DateTime? FormatDob(string dob)
        {
            try
            {
                string[] formats = { "dd/MM/yyyy", "MM/yyyy", "yyyy" };
                dob = dob.Trim();
                if (dob == null)
                {
                    return null;
                }
                else
                {
                    var result = DateTime.ParseExact(dob, formats, provider: null);
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Thêm customer vào database
        /// </summary>
        /// <param name="customes"></param>
        /// <returns></returns>
        private int InsertCustomer(List<Customer> customes)
        {
            string sqlCommand = "Proc_InsertCustomer";
            var res = 0;
            foreach (Customer customer in customes)
            {
                if (customer.Status == null)
                {
                    using (dbConnection = new MySqlConnection(connectionString))
                    {
                        dbConnection.Execute(sqlCommand, param: customer, commandType: CommandType.StoredProcedure);
                        res++;
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// Kiểm tra dữ liệu
        /// </summary>
        /// <param name="customers"></param>
        private void Validate(List<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                CheckCustomerCodeExists(customer);
                CheckCustomerGroupNameExists(customer);
                CheckPhoneNumberExists(customer);
            }
            //Check CustomerCode
            for (int i = 1; i < customers.Count; i++)
            {
                for (int j = i + 1; j < customers.Count - 1; j++)
                {
                    if (customers[i].CustomerCode == customers[j].CustomerCode)
                    {
                        customers[i].Status += Properties.Resources.MsgCustomerCodeExits + "\n";
                        break;
                    }
                }
            }

            //Check PhoneNumber
            for (int i = 1; i < customers.Count; i++)
            {
                for (int j = i + 1; j < customers.Count - 1; j++)
                {
                    if (customers[i].PhoneNumber == customers[j].PhoneNumber)
                    {
                        customers[i].Status += $"Số điện thoại {customers[i].PhoneNumber} tồn tại trên tệp\n";
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="customer"></param>
        private void CheckCustomerCodeExists(Customer customer)
        {
            var sqlCommand = "Proc_CheckCustomerCodeExists";
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var res = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: customer, commandType: CommandType.StoredProcedure);

                if (res)
                {
                    customer.Status += "Mã khách hàng tồn tại trên hê thống\n";
                }
            }
        }

        /// <summary>
        /// Kiểm tra trùng tên nhóm khách hàng
        /// </summary>
        /// <param name="customer"></param>
        private void CheckCustomerGroupNameExists(Customer customer)
        {
            var sqlCommand = "Proc_CheckCustomerGroupNameExists";
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var res = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: customer, commandType: CommandType.StoredProcedure);
                if (!res)
                {
                    customer.Status += "Tên nhóm khách hàng không tồn tại trên hê thống\n";
                }
            }
        }

        /// <summary>
        /// Kiểm tra trùng số điện thoại
        /// </summary>
        /// <param name="customer"></param>
        private void CheckPhoneNumberExists(Customer customer)
        {
            var sqlCommand = "Proc_CheckPhoneNumberExists";
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var res = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: customer, commandType: CommandType.StoredProcedure);
                if (res)
                {
                    customer.Status += "Số điện thoại tồn tại trên hê thống\n";
                }
            }
        }
    }
}