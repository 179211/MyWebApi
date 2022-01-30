Response Object
Status Code
	100-199		Information
	200-299		Success
		200		OK
		201		Created
		204		No Content
	300-399		Redirection
	400-499		Client Error
		400		Bad Request
		401		Unauthorized
		404		Not Found
		409		Conflict
	500-599		Serve Error
		500		Internal Server Error

Package Manager Console
Add-Migration AddDepartmentAndUser
Update-Database

Home Work
Db created with nvarchar(max) how to limit that to nvarchar(50)

DTO
-Decouple dbModel from ApiModel
-Full control / custom changes
-Different Dto for diff version
-Expose only set of attributes / Abstraction

DTO Mapping
-Add Packages 
-Need to create ApiMappings Class
-Update Startup Class
-services.AddAutoMapper(typeof(ApiMappings));

Home Work
Other options for [FromBody]
Possible return types of API

Swagger

Why Swagger
-All user must get latest postman collection

NuGet package Swashbuckle.AspNetCore
Config Service AddSwaggerGen
Config UseSwagger, UseSwaggerUI
Remove launchUrl from Properties > launchSettings.json

Update Project Properties > Build > XML doc file
MyWebApi.xml
Update AddSwaggerGen
///
Suppress warning 1591 go to Build > Suppress warning append 1591

Update All return types
[ProducesResponseType(200, Type = typeof(List<DepartmentDto>))]
[ProducesResponseType(StatusCodes.Status404NotFound)]


Versioning
pack Microsoft.AspNetCore.Mvc.Versioning
pack Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
Add service
AddApiVersioning, AddVersionedApiExplorer

create class ConfigureSwaggerOptions
move content of AddSwaggerGen to class 
AddTransient of this class in start up
Add IApiVersionDescriptionProvider provider in Configure
Use for each loop 
Update rout for controller     
//[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
update CreatedAtRoute with version info - Version = HttpContext.GetRequestedApiVersion().ToString()

Make sure to have Separate Action Verbs like two Get can cause routing problem 


Api Authentication

Add User Model
Migrate User
Create User Repo
Add not mapped Token in User and Dto
Make sure Api is ur startup project
Migrate - Add-Migration AddUserRole, Update-Database
Create AppSettings config and class
Add AppSettings Singleton
install Microsoft.AspNetCore.Authentication.JwtBearer 3.*.* for net core 3
Add cors and Authentication in Configure
Update UserRepo to generate token 
Add DI in UserRepo IOptions<AppSettings>
Show Jwt.io with verifing by key






