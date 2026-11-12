using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    // 10 Distinct Columns[cite: 1, 2]
    public int Id { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Department { get; set; } 
    public string JobTitle { get; set; } 
    public decimal MonthlySalary { get; set; } 
    public double TaxRate { get; set; } 
    public DateTime HireDate { get; set; } 
    public bool IsActive { get; set; } 
    public string Email { get; set; } 
}

public class Program
{
    // Program must start with an empty list (0 pre-defined records)[cite: 1]
    static List<Employee> employees = new List<Employee>();
    static int nextId = 1001; // Auto-increment starting at 1001[cite: 1]

    public static void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n--- Employee Payroll & HR Management System ---");
            Console.WriteLine("1. Create New Employee");
            Console.WriteLine("2. Read All Employees");
            Console.WriteLine("3. Search Employees by Department");
            Console.WriteLine("4. Update Employee Salary");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option (1-6): ");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PromptCreateEmployee();
                    break;
                case "2":
                    ReadEmployees();
                    break;
                case "3":
                    Console.Write("Enter Department to Search: ");
                    string dept = Console.ReadLine();
                    SearchEmployees(dept);
                    break;
                case "4":
                    Console.Write("Enter Employee ID to Update: ");
                    if (int.TryParse(Console.ReadLine(), out int updateId))
                    {
                        Console.Write("Enter New Monthly Salary: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newSalary))
                        {
                            UpdateEmployee(updateId, newSalary);
                        }
                    }
                    break;
                case "5":
                    Console.Write("Enter Employee ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteId))
                    {
                        DeleteEmployee(deleteId);
                    }
                    break;
                case "6":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1-6.");
                    break;
            }
        }
    }

    static void PromptCreateEmployee()
    {
        Console.WriteLine("\n--- Enter Employee Details ---");
        
        Console.Write("First Name: ");
        string fName = Console.ReadLine();
        
        Console.Write("Last Name: ");
        string lName = Console.ReadLine();
        
        Console.Write("Department (e.g., IT, HR): ");
        string dept = Console.ReadLine();
        
        Console.Write("Job Title: ");
        string title = Console.ReadLine();
        
        Console.Write("Monthly Salary: ");
        decimal salary = decimal.Parse(Console.ReadLine() ?? "0");
        
        Console.Write("Tax Rate (e.g., 0.12): ");
        double tax = double.Parse(Console.ReadLine() ?? "0");
        
        Console.Write("Hire Date (YYYY-MM-DD): ");
        DateTime hireDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());
        
        Console.Write("Is Active (true/false): ");
        bool isActive = bool.Parse(Console.ReadLine() ?? "true");
        
        Console.Write("Email: ");
        string email = Console.ReadLine();

        Employee newEmp = new Employee
        {
            Id = nextId++, // Auto-incrementing identifier[cite: 1]
            FirstName = fName,
            LastName = lName,
            Department = dept,
            JobTitle = title,
            MonthlySalary = salary,
            TaxRate = tax,
            HireDate = hireDate,
            IsActive = isActive,
            Email = email
        };
        
        employees.Add(newEmp);
        Console.WriteLine($"\n[SUCCESS] Added: {fName} {lName} with ID {newEmp.Id}");
    }

    static void ReadEmployees()
    {
        Console.WriteLine("\n--- All Employees ---");
        if (!employees.Any())
        {
            Console.WriteLine("No employees found.");
            return;
        }

        // READ using LINQ[cite: 1]
        var allEmployees = employees.Select(e => e).ToList();
        foreach (var emp in allEmployees)
        {
            Console.WriteLine($"ID: {emp.Id} | Name: {emp.FirstName} {emp.LastName} | Dept: {emp.Department} | Title: {emp.JobTitle} | Salary: {emp.MonthlySalary:C}");
        }
    }

    static void SearchEmployees(string department)
    {
        Console.WriteLine($"\n--- Search Results for {department} ---");
        
        // SEARCH/FILTER using LINQ[cite: 1]
        var results = employees.Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase)).ToList();
        
        if (!results.Any())
        {
            Console.WriteLine("No employees found in this department.");
            return;
        }

        foreach (var emp in results)
        {
            Console.WriteLine($"Found: ID {emp.Id} - {emp.FirstName} {emp.LastName}");
        }
    }

    static void UpdateEmployee(int id, decimal newSalary)
    {
        // UPDATE using LINQ[cite: 1]
        var empToUpdate = employees.FirstOrDefault(e => e.Id == id);
        if (empToUpdate != null)
        {
            empToUpdate.MonthlySalary = newSalary;
            Console.WriteLine($"\n[SUCCESS] Updated {empToUpdate.FirstName}'s salary to {newSalary:C}");
        }
        else
        {
            Console.WriteLine("\n[ERROR] Employee not found.");
        }
    }

    static void DeleteEmployee(int id)
    {
        // DELETE using LINQ[cite: 1]
        var empToDelete = employees.FirstOrDefault(e => e.Id == id);
        if (empToDelete != null)
        {
            employees.Remove(empToDelete);
            Console.WriteLine($"\n[SUCCESS] Deleted employee with ID {id}");
        }
        else
        {
            Console.WriteLine("\n[ERROR] Employee not found.");
        }
    }
}