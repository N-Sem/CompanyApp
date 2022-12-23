using CompanyApp.Models.Entities;

namespace CompanyApp.Dal.Initialization
{
    public static class SampleData
    {
        public static List<Department> Departments => new()
        {
            new() { Id = 1, Name = "Продажи" },
            new() { Id = 2, Name = "Склад" },
            new() { Id = 3, Name = "Уборка" }
        };

        public static List<Employee> Employees => new()
        {
            new() { Id = 1, FirstName = "Михаил", MiddleName = "Матвеевич", LastName = "Петров", BirthDate = DateTime.Parse("11.8.1999"), Gender = Enums.Gender.М, DepartmentId = 1, IsDirector = true },
            new() { Id = 2, FirstName = "Анастасия", MiddleName = "Владимировна", LastName = "Рулькина", BirthDate = DateTime.Parse("1.7.1984"), Gender = Enums.Gender.Ж, DepartmentId = 1 },
            new() { Id = 3, FirstName = "Олег", MiddleName = "Евгеньевич", LastName = "Макаревич", BirthDate = DateTime.Parse("13.12.1989"), Gender = Enums.Gender.М, DepartmentId = 1 },
            new() { Id = 4, FirstName = "Ольга", MiddleName = "Анатольевна", LastName = "Колобкова", BirthDate = DateTime.Parse("9.1.1991"), Gender = Enums.Gender.Ж, DepartmentId = 2, IsDirector = true },
            new() { Id = 5, FirstName = "Евгения", MiddleName = "Владимирована", LastName = "Пятачкова", BirthDate = DateTime.Parse("22.4.1996"), Gender = Enums.Gender.Ж, DepartmentId = 2 },
            new() { Id = 6, FirstName = "Тимофей", MiddleName = "Александрович", LastName = "Кинчиков", BirthDate = DateTime.Parse("14.6.1997"), Gender = Enums.Gender.М, DepartmentId = 2 },
            new() { Id = 7, FirstName = "Петр", MiddleName = "Петрович", LastName = "Гепардов", BirthDate = DateTime.Parse("17.9.1988"), Gender = Enums.Gender.М, DepartmentId = 2 },
            new() { Id = 8, FirstName = "Василий", MiddleName = "Дмитриевич", LastName = "Алешин", BirthDate = DateTime.Parse("15.2.1991"), Gender = Enums.Gender.М, DepartmentId = 3, IsDirector = true },
            new() { Id = 9, FirstName = "Дмитрий", MiddleName = "Данилович", LastName = "Ильин", BirthDate = DateTime.Parse("28.7.1981"), Gender = Enums.Gender.М, DepartmentId = 3 },
            new() { Id = 10, FirstName = "Екатерина", MiddleName = "Ильинишна", LastName = "Волкова", BirthDate = DateTime.Parse("6.3.1980"), Gender = Enums.Gender.Ж, DepartmentId = 3 },
            new() { Id = 11, FirstName = "Арина", MiddleName = "Максимовна", LastName = "Чапарова", BirthDate = DateTime.Parse("1.5.1975"), Gender = Enums.Gender.Ж, DepartmentId = 3 }
        };

        public static List<Order> Orders => new()
        {
            new() { Id = 1, Name = "Платье" },
            new() { Id = 2, Name = "Юбка" },
            new() { Id = 3, Name = "Блузка" },
            new() { Id = 4, Name = "Туфли" },
            new() { Id = 5, Name = "Ботинки" },
            new() { Id = 6, Name = "Перчатки" },
            new() { Id = 7, Name = "Куртка" },
            new() { Id = 8, Name = "Пальто" },
            new() { Id = 9, Name = "Шапка" },
            new() { Id = 10, Name = "Шарф" },
            new() { Id = 11, Name = "Свитер" },
            new() { Id = 12, Name = "Колготки" },
            new() { Id = 13, Name = "Носки" },
            new() { Id = 14, Name = "Очки" }
        };

        public static List<int[]> EmployeeOrder = new()
        {
            new[] { 1, 1 },
            new[] { 1, 5 },
            new[] { 1, 14 },
            new[] { 8, 2 },
            new[] { 8, 12 },
            new[] { 3, 3 },
            new[] { 3, 13 },
            new[] { 4, 4 },
            new[] { 4, 14 },
            new[] { 5, 5 },
            new[] { 5, 1 },
            new[] { 5, 10 },
            new[] { 6, 6 },
            new[] { 6, 2 },
            new[] { 6, 11 },
            new[] { 7, 7 },
            new[] { 7, 3 },
            new[] { 8, 8 },
            new[] { 8, 7 },
            new[] { 8, 9 },
            new[] { 9, 9 },
            new[] { 9, 13 },
            new[] { 10, 10 },
            new[] { 11, 11 }
        };

    }
}
