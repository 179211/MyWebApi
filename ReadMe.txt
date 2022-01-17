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


