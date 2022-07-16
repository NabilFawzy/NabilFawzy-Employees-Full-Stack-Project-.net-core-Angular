using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnionArch.Data.Entities;
using OnionArch.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Repository.Data.DataSeed
{
    public static  class SystemDataSeed
    {
  
      //  private static readonly IConfiguration configuration;

      
        public static async Task  Seed(EmployeeContext context,ConfigurationManager manager)
        {
            List<PositionType> positions = new List<PositionType>();

            try
            {

                if (!context.PositionTypes.Any())
                {
                  var json=  manager.GetSection("PositionsTypes").Value;
                    var positionTypes = JsonConvert.DeserializeObject<List<PositionTypeDto>>(json);

                    foreach(var type in positionTypes)
                    {
                        var position = new PositionType()
                        {
                            Id = new Guid(),
                            Name = type.Name
                        };

                        positions.Add(position);
                        context.PositionTypes.Add(position);
                    }

                    await context.SaveChangesAsync();

                  


                }
                else
                {
                    positions = await context.PositionTypes.ToListAsync();
                 
                }


                if (!context.Employees.Any())
                {
                    var json = manager.GetSection("AdminEmp").Value;
                    EmployeeSeederDto Admin = JsonConvert.DeserializeObject<List<EmployeeSeederDto>>(json).FirstOrDefault();

                    Guid empid = Guid.NewGuid();
                    var emp = new Employee()
                    {
                        Id =  empid,
                        FullName = Admin.FullName,
                        UserName = Admin.UserName,
                        Email = Admin.Email,
                        Password = Admin.Password,
                        Job = Admin.Job,
                        isAdmin = Admin.isAdmin,
                          PositionTypeId = positions[Convert.ToInt32(Admin.PositionType)].Id
                    }
                    ;


                    await context.Employees.AddAsync(emp);
                    await context.SaveChangesAsync();


                    json = manager.GetSection("Employees").Value;
                    List<EmployeeSeederDto> emps = JsonConvert.DeserializeObject<List<EmployeeSeederDto>>(json).ToList();
                    foreach(var e in emps)
                    {
                        empid = Guid.NewGuid();

                        var empN = new Employee()
                        {
                            Id = empid,
                            FullName = e.FullName,
                            UserName = e.UserName,
                            Email = e.Email,
                            Password = e.Password,
                            Job = e.Job,
                            isAdmin = e.isAdmin,
                            PositionTypeId = positions[Convert.ToInt32(e.PositionType)].Id
                        }
                    ;


                        await context.Employees.AddAsync(empN);
                 
                    }

                    await context.SaveChangesAsync();
                }


            }
            catch (Exception e)
            {

            }

        }
    }
}
