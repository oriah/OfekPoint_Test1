# OfekPoint_Test1

### **Live URL (Azure App Service in conjunction with Azure SQL Database) :**  
https://oria2.azurewebsites.net/swagger/index.html
   
  
### **Elements, technologies and/or paradigms used in this tester :**  
ASP.NET 6.0  
MSSQL   
Entity Framework Core (Code-First-Design)    
Dependendancy Injection (ASP.CORE Out-of-the-box) -  cross cutting   
AutoMapper  
Multi-Layer Exception Typing and Handling (cross-cutting)  
Log4Net  (cross-cutting)   
DataAnnotations   
  
  
  
### **Architecture (3-tier) :**  
Sisma.Web (Presentation-Layer)  
Sisma.BL  (Business-Logic-Layer)  
Sisma.DAL  (Data-Access-Layer)  
[Sisma.Shared  (a cross-cutting shared layer)]   
DB:  MSSQL DB  (Code-First Design]  



### **API Structure**  

/api  -  normal site actions  
in swagger: **Site** section.

/api/admin  -  admin site actions  
in swagger: **AdminSchools, AdminClasseses, AdminStudents, AdminStudentInClasses** sections.
  