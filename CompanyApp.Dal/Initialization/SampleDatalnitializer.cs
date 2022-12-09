using CompanyApp.Dal.EfStructures;
using CompanyApp.Models.Base;
using CompanyApp.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyApp.Dal.Initialization
{
    public class SampleDatalnitializer
    {

        public static void DropAndCreateDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        internal static void ClearData(ApplicationDbContext context)
        {
            var entities = new[]
            {
                typeof(Employee).FullName,
                typeof(Department).FullName,
                typeof(Order).FullName
            };

            foreach (var entityName in entities)
            {
                var entity = context.Model.FindEntityType(entityName!);
                var tableName = entity!.GetTableName();
                context.Database.ExecuteSqlRaw($"DELETE FROM {tableName};");
                context.Database.ExecuteSqlRaw($"ALTER TABLE {tableName} AUTO_INCREMENT = 1;");
            }
            context.Database.ExecuteSqlRaw($"DELETE FROM EmployeeOrder;");
        }

        internal static void SeedData(ApplicationDbContext context)
        {
            try
            {
                ProcessInsert(context, context.Departments!, SampleData.Departments);
                ProcessInsert(context, context.Employees!, SampleData.Employees);
                ProcessInsert(context, context.Orders!, SampleData.Orders);
                InsertEmployeeOrderRelations(context, SampleData.EmployeeOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            static void ProcessInsert<TEntity>(
                ApplicationDbContext context, DbSet<TEntity> table, List<TEntity> records) where TEntity : BaseEntity
            {
                if (table.Any())
                {
                    return;
                }

                IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
                strategy.Execute(() =>
                {
                    using var transaction = context.Database.BeginTransaction();
                    var metaData = context.Model.FindEntityType(typeof(TEntity).FullName!);
                    try
                    {
                        table.AddRange(records);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        Console.Beep();
                        transaction.Rollback();
                    }
                });
            }

            static void InsertEmployeeOrderRelations(ApplicationDbContext context,List<int[]> records)
            {
                IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
                strategy.Execute(() =>
                {
                    using var transaction = context.Database.BeginTransaction();
                    try
                    {
                        foreach (var record in records)
                        {
                            context.Database.ExecuteSqlRaw($"INSERT INTO EmployeeOrder (EmployeesId, OrdersId) VALUES({record[0]}, {record[1]});");
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        Console.Beep();
                        transaction.Rollback();
                    }
                });
            }
        }

        public static void InitializeData(ApplicationDbContext context)
        {
            DropAndCreateDatabase(context);
            SeedData(context);
        }

        public static void ClearAndReseedDatabase(ApplicationDbContext context)
        {
            ClearData(context);
            SeedData(context);
        }
    }
}
