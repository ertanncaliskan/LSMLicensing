# LSMLicensing
Basic licensing application

asp.net core projects has been used for creating this solution. and for heroku publishing, Docker Containers and Heroku Docker CLI is used. 
Sample application url is below, this url is contaning just rest api project;

https://simons-voss-license-demo.herokuapp.com/

For making the process realistic, Socket Handshake and License Generator projects are hosted on another environment for representing the intranet side of SimonsVoss. This environment has no endpoints accessible by public. It is working like a windows service. Its url is below,

https://simons-voss-intranet.herokuapp.com/

### Overview

- This is an sample cloud based license generator application. In this project, user provides required information(company name, contact person, email, license key etc.) from a basic user interface and gets signed license.

 - This process provided by 2 main stakeholders. The first one is shared api and the second one is license generator library which is stored in intranet. All connections are provided by sockets between these two main stakeholders.

We can seperate application with three main folders;
* [LSMLicensing] - This project is a rest api. Project name is LicenseRegistrationAPI. Addition to its rest api feature It contains basic view renderer and renders html files.
* [LicenseSignatureServiceHandshake] - This project is taking care of socket client connections. It is keeping socket alive and making comminication between license generator and rest api via SignalR sockets. Project name is LicenseSignatureServiceHandshake
* [LSMLicensing] - This project is an license generator. Retrieving required information from socket handshake, generating license and keeping it its basic database.

### Technologies
#### LicenseRegistrationAPI

It is using, 
 - vuejs, axios, bootstrap, html and css for user interface. rest api is generating and returning views from basic view engine named BasicViewEngine class.
 - Microsoft.AspNetCore.SignalR is used for socket connection. SocketTokenRequirementHandler class used as an authorization handler for socket security. ManualResetEventSlim is used for socket thread synchronization in SignatureContainer class.
 - All environment variables are providing from appsettings.json files which are seperated by its environment type.
 - It is using FluentValidation.AspNetCore for dynamic server side validation of request model objects.
#### LicenseSignatureServiceHandshake
 
 It is using,
  - Microsoft.AspNetCore.SignalR.Client and connecting to rest api. Making bridge between license signature creator library and rest api. SocketConnector class is trying to reconnect and trying to keeping connection forever between two stakeholders.
  - All environment variables are providing from appsettings.json files which are seperated by its environment type.
 
#### LicenseSignatureService
 
 It is using,
  - SQL Lite for keeping signed license values and making data access operation.
  - It is using Microsoft.EntityFrameworkCore.Sqllite for ORM layer and all data operations are processing by this ORM layer.
  
### Sample License Keys
  Here is the sample license keys for testing,
  
EFDFC4FA-6FC7-4B03-907E-52A7ADCDF099

4BFADFF5-DE2C-4186-918F-22295725E42B

B9C745B1-E064-45F1-96AB-042693C3AA75

FD4BFAEA-D9BA-4633-8DAB-3AD2504D6A8B

20751CAD-AF42-4947-84EE-8CDD77F37F70

EB1E2D5B-86F9-486F-A3BA-693F00C31ED5

8E6A4E03-4575-4854-984D-68603874832D

F28277C6-CD43-48F8-A63B-E007733E4C31

4BFBD023-5454-4AB0-AC4D-0142E58CD628

7CEA86C4-98A4-446A-A23F-C25CFB231FA4

F750ABE4-EEB4-4B21-9A49-4F226777647D

7361A500-E1CF-4835-9FAF-B4A65F10C6DC

5B8B9A12-3040-41A3-8FF6-9862E437E409

842072CD-BACA-46C2-8F01-46939EFE0C1F

41542830-ABE7-4A5E-AFA2-19A7FB0B8C58

FFF0AA21-9076-4230-A577-D0E694DE8EEE

38919BFE-C000-4063-9FF9-DA67420ED7DC

4A08AD2C-D726-4238-A799-A420D93658FA

8523BA55-FB57-4687-A563-7FF0ABAE7EB3

4047941B-AD32-4975-8F35-845521A417AB

4A437E04-B874-481F-96EA-DFCAC0F0DEB2

87DDE537-AF95-4471-9FFD-8B1D0FE030FB

377D3C1F-1AEC-48EB-BEC9-8957FFC8F23C

7BB81731-42A4-4881-851D-7D216EB7E52A

E347854F-12DA-4C1B-ADA0-F5D777B769F5

20810340-D035-4476-96DC-68B0B4326DB9

7C0D9A46-9A8E-4190-A1DD-065193C4E931

BB6E84DC-39A1-453D-9172-13CEE49B7DA8

1922173D-DE07-44B3-87E3-F72D7321551F

0FBA9180-C699-4BB5-8419-A0C044A430A9

14614ABC-FBA5-4513-9065-E247BF311F78

43840319-D75F-496E-A493-AA8798AD1AB3

252B3039-ACEE-47FF-9053-30A707D9AC34

16448DC1-95AA-4EE5-978D-C8CBDD258595

AAE3CB54-5090-417E-8928-2AD6B7DF2693

ED76057E-3934-4DC2-9F79-0CFFA676E7FD

DE2E0B82-72C0-4C3C-A92A-9ED60AB880CA

E8D222AD-306D-4EDA-B7A2-6B79A2D82BB3

75D4DEDD-417A-44A6-8623-E6CCC5F6CD84

3A8083B0-8A43-447E-BC04-E199B4302A1A

DB66E266-BCD2-4BF3-A483-2349694586E1

4EF0A1C5-4F7A-4A20-AC14-160B990DE43C

7E02D308-D849-4BC9-B416-6F92594DE198

E432E896-BCE9-4FA6-80E5-09E75244E011

739DF5E3-CFEF-4FCD-95DD-F9A1FE9A95AC

46ECB871-BF23-4700-8269-DB87FE3BDA5F

45C6E44B-3B3E-4BEA-808C-29CFBB990002

F3236A27-56F6-4254-930F-E99924E0B834

16C1FE4B-AC63-47A3-B2C3-90C2735DBE3E

5E9FAEF6-F8C1-47CB-A1D6-A0D5CBE9B3D9

  
  
