EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';

DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql += 'ALTER TABLE [' + s.name + '].[' + t.name + '] DROP CONSTRAINT [' + f.name + '];'
FROM sys.foreign_keys f
INNER JOIN sys.tables t ON f.parent_object_id = t.object_id
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id;
EXEC sp_executesql @sql;

EXEC sp_MSforeachtable 'DROP TABLE ?';

SELECT * FROM INFORMATION_SCHEMA.TABLES;



CREATE TABLE [User] (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    MobileNo NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);

-- Department Table
CREATE TABLE Department (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(250) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

-- Doctor Table
CREATE TABLE Doctor (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Qualification NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

-- DoctorDepartment Table (Junction table for many-to-many relationship)
CREATE TABLE DoctorDepartment (
    DoctorDepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DoctorID INT NOT NULL,
    DepartmentID INT NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID),
    FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

-- Patient Table
CREATE TABLE Patient (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    DateOfBirth DATETIME NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(100) NOT NULL,
    Address NVARCHAR(250) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    State NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

-- Appointment Table
CREATE TABLE Appointment (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    DoctorID INT NOT NULL,
    PatientID INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    AppointmentStatus NVARCHAR(20) NOT NULL,
    Description NVARCHAR(250) NOT NULL,
    SpecialRemarks NVARCHAR(100) NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    TotalConsultedAmount DECIMAL(5,2) NULL,
    FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);






--========================Users=============================

create proc SP_Users_Select
as
begin
	select * from [User]
end

exec SP_Users_Select

----

CREATE PROCEDURE SP_User_Login
    @Email NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SELECT Email, Password 
    FROM [User]
    WHERE Email = @Email AND Password = @Password
END

exec SP_User_Login 'admin@gmail.com','admin'
--------

create proc SP_Users_SelectById
	@userid int
as
begin
	select * from [User] where UserID = @userid
end

exec SP_Users_SelectById 1

----

create proc SP_Users_Insert
	@UserName NVARCHAR(100),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @MobileNo NVARCHAR(100),
    @IsActive BIT = 1
as
begin
	insert into [User](UserName,Password,Email,MobileNo,IsActive,Modified)
	values(@UserName,@Password,@Email,@MobileNo,@IsActive,GETDATE())
end

exec SP_Users_Insert 'va','abc','abc@gmail.com','1234567890'

-----------

CREATE PROCEDURE sp_Users_Update
    @UserID INT,
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @MobileNo NVARCHAR(100),
    @IsActive BIT
AS
BEGIN
    UPDATE [User]
    SET UserName = @UserName,Password = @Password,Email = @Email,MobileNo = @MobileNo,IsActive = @IsActive,Modified = GETDATE()
    WHERE UserID = @UserID
END

exec sp_Users_Update 2,'admin','admin','admin@gmail.com','1234567890',1

-------

create proc sp_Users_Delete
	@UserId INT
as 
begin
	delete from [User] where UserID=@UserId
end

exec sp_Users_Delete 3


--========================Department=============================

create proc sp_Department_Select
as 
begin
	select * from Department
end

exec sp_Department_Select
-----
create proc sp_Department_SelectById
	@DepartmentID int
as 
begin
	select * from Department where DepartmentID=@DepartmentID	
end

exec sp_Department_SelectById 1

-----

create proc sp_Department_Insert
	@DepartmentName nvarchar(100),
	@Description nvarchar(250),
	@IsActive bit = 1,
	@UserID int
as
begin
	insert into Department(DepartmentName,[Description],IsActive,Created,Modified,UserID)
	values (@DepartmentName,@Description,@IsActive,GETDATE(),GETDATE(),@UserID)
end

exec sp_Department_Insert 'abc','abc',1,2

------

create proc sp_Department_Update
	@DepartmentID int,
	@DepartmentName nvarchar(100),
	@Description nvarchar(250),
	@IsActive bit = 1,
	@UserID int
as
begin
	update Department set DepartmentName=@DepartmentName,[Description]=@Description,IsActive=@IsActive,Modified=GETDATE(),UserID=@UserID
	where DepartmentID= @DepartmentID
end

exec sp_Department_Update 1,'xyz','xyz',1,2


--------

create proc sp_Department_Delete
	@DepartmentID int
as
begin
	delete from Department where DepartmentID=@DepartmentID
end

exec sp_Department_Delete 1




--========================Doctor=============================

CREATE proc sp_Doctor_Select
AS
BEGIN
    SELECT * FROM Doctor
END

exec sp_Doctor_Select

------

CREATE proc sp_Doctor_SelectById
    @DoctorID INT
AS
BEGIN
    SELECT * FROM Doctor WHERE DoctorID = @DoctorID
END

exec sp_Doctor_SelectById 1


-----

CREATE proc sp_Doctor_Insert
    @Name NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Email NVARCHAR(100),
    @Qualification NVARCHAR(100),
    @Specialization NVARCHAR(100),
    @IsActive BIT = 1,
    @UserID INT
AS
BEGIN
    INSERT INTO Doctor (Name, Phone, Email, Qualification, Specialization, IsActive, Created, Modified, UserID)
    VALUES (@Name, @Phone, @Email, @Qualification, @Specialization, @IsActive, GETDATE(), GETDATE(), @UserID)
END

exec sp_Doctor_Insert 'n','123457890','n@gmail.com','MBBS','NECK',1,2

--------------

CREATE proc sp_Doctor_Update
    @DoctorID INT,
    @Name NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Email NVARCHAR(100),
    @Qualification NVARCHAR(100),
    @Specialization NVARCHAR(100),
    @IsActive BIT,
    @UserID INT
AS
BEGIN
    UPDATE Doctor
    SET Name = @Name,
        Phone = @Phone,
        Email = @Email,
        Qualification = @Qualification,
        Specialization = @Specialization,
        IsActive = @IsActive,
        Modified = GETDATE(),
        UserID = @UserID
    WHERE DoctorID = @DoctorID
END


exec sp_Doctor_Update 1,'n','123457890','n@gmail.com','MBBS','NECK',1,2

---------

CREATE proc sp_Doctor_Delete
    @DoctorID INT
AS
BEGIN
    DELETE FROM Doctor WHERE DoctorID = @DoctorID
END

exec sp_Doctor_Delete 1



--========================DoctorDepartment=============================

CREATE PROCEDURE sp_DepartmentDoctor_select
AS
SELECT DD.DoctorDepartmentID, DD.DoctorID, Doc.Name AS DoctorName, DD.DepartmentID, Dept.DepartmentName,
       DD.Created, DD.Modified, U.UserName
FROM DoctorDepartment DD
INNER JOIN Doctor Doc ON DD.DoctorID = Doc.DoctorID
INNER JOIN Department Dept ON DD.DepartmentID = Dept.DepartmentID
INNER JOIN [User] U ON DD.UserID = U.UserID
ORDER BY Doc.Name, Dept.DepartmentName

exec sp_DoctorDepartment_Insert 2,2,1

-----

CREATE OR ALTER PROCEDURE [dbo].[sp_DoctorDepartment_SelectByUserID]
AS
BEGIN
    SELECT 
        dd.DoctorDepartmentID,
        doc.Name AS DoctorName,
        dept.DepartmentName
    FROM 
        DoctorDepartment dd
    INNER JOIN Doctor doc ON doc.DoctorID = dd.DoctorID
    INNER JOIN Department dept ON dept.DepartmentID = dd.DepartmentID
END




CREATE OR ALTER PROCEDURE sp_DoctorDepartment_SelectByID
    @DoctorDepartmentID INT
AS
BEGIN
    SELECT DoctorDepartmentID, DoctorID, DepartmentID, UserID
    FROM DoctorDepartment
    WHERE DoctorDepartmentID = @DoctorDepartmentID
END

-----

CREATE proc sp_DepartmentDoctor_select
    @DoctorID INT
AS
BEGIN
    SELECT d.* 
    FROM Department d
    INNER JOIN DoctorDepartment dd ON d.DepartmentID = dd.DepartmentID
    WHERE dd.DoctorID = @DoctorID
END

exec sp_DepartmentDoctor_select 2

-------

CREATE proc sp_DoctorsDepartment_select
    @DepartmentID INT
AS
BEGIN
    SELECT doc.* 
    FROM Doctor doc
    INNER JOIN DoctorDepartment dd ON doc.DoctorID = dd.DoctorID
    WHERE dd.DepartmentID = @DepartmentID
END

exec sp_DoctorsDepartment_select 2


CREATE OR ALTER PROCEDURE sp_Doctor_Department_Update
    @DoctorDepartmentID INT,
    @DoctorID INT,
    @DepartmentID INT
AS
BEGIN
    UPDATE DoctorDepartment
    SET 
        DoctorID = @DoctorID,
        DepartmentID = @DepartmentID
    WHERE DoctorDepartmentID = @DoctorDepartmentID
END


-----


CREATE proc sp_DoctortoDepartment_Delete
    @DoctorDepartmentID INT
AS
BEGIN
    DELETE FROM DoctorDepartment WHERE DoctorDepartmentID = @DoctorDepartmentID
END

exec sp_DoctortoDepartment_Delete 1



CREATE OR ALTER PROCEDURE [dbo].[sp_DoctorDepartment_GetDoctorAndDepartment]
AS
BEGIN
    SELECT 
        d.DoctorID,
        d.Name AS DoctorName,
        dept.DepartmentID,
        dept.DepartmentName
    FROM 
        Doctor d
    INNER JOIN DoctorDepartment dd ON d.DoctorID = dd.DoctorID
    INNER JOIN Department dept ON dd.DepartmentID = dept.DepartmentID
END
GO


EXEC sp_DoctorDepartment_GetDoctorAndDepartment

--=========================================================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_DoctorDepartment_GetDropdownData]
AS
BEGIN
    SELECT DoctorID, Name AS DoctorName FROM Doctor;

    SELECT DepartmentID, DepartmentName FROM Department;
END
GO

exec sp_DoctorDepartment_GetDropdownData

--=========================================================================================

--========================Patient=============================

CREATE proc sp_Patients_Select
AS
BEGIN
    SELECT * FROM Patient
END

exec sp_Patients_Select

------

CREATE PROCEDURE sp_Patients_SelectById
    @PatientID INT
AS
BEGIN
    SELECT * FROM Patient WHERE PatientID = @PatientID
END

exec sp_Patients_SelectById 1

----

CREATE proc sp_Patient_Insert
    @Name NVARCHAR(100),
    @DateOfBirth DATETIME,
    @Gender NVARCHAR(10),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(100),
    @Address NVARCHAR(250),
    @City NVARCHAR(100),
    @State NVARCHAR(100),
    @IsActive BIT = 1,
    @UserID INT
AS
BEGIN
    INSERT INTO Patient (Name, DateOfBirth, Gender, Email, Phone, Address, City, State, IsActive, Created, Modified, UserID)
    VALUES (@Name, @DateOfBirth, @Gender, @Email, @Phone, @Address, @City, @State, @IsActive, GETDATE(), GETDATE(), @UserID)
END


exec sp_Patient_Insert 'vai','2003-02-11','female','abc@gmail.com','1234567890','abc','jamnagar','gujarat',1,2


-----

CREATE proc sp_Patient_Update
    @PatientID INT,
    @Name NVARCHAR(100),
    @DateOfBirth DATETIME,
    @Gender NVARCHAR(10),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(100),
    @Address NVARCHAR(250),
    @City NVARCHAR(100),
    @State NVARCHAR(100),
    @IsActive BIT,
    @UserID INT
AS
BEGIN
    UPDATE Patient
    SET Name = @Name,
        DateOfBirth = @DateOfBirth,
        Gender = @Gender,
        Email = @Email,
        Phone = @Phone,
        Address = @Address,
        City = @City,
        State = @State,
        IsActive = @IsActive,
        Modified = GETDATE(),
        UserID = @UserID
    WHERE PatientID = @PatientID
END

exec sp_Patient_Update 1,'xyz','2003-02-11','female','abc@gmail.com','1234567890','abc','jamnagar','gujarat',1,2


------


CREATE proc sp_Patient_Delete
    @PatientID INT
AS
BEGIN
    DELETE FROM Patient WHERE PatientID = @PatientID
END

exec sp_Patient_Delete 1


--========================Appointment=============================

CREATE proc sp_Appointment_Insert
    @DoctorID INT,
    @PatientID INT,
    @AppointmentDate DATETIME,
    @AppointmentStatus NVARCHAR(20),
    @Description NVARCHAR(250),
    @SpecialRemarks NVARCHAR(100),
    @UserID INT,
    @TotalConsultedAmount DECIMAL(5,2) = NULL
AS
BEGIN
    INSERT INTO Appointment (DoctorID, PatientID, AppointmentDate, AppointmentStatus, Description, SpecialRemarks, Created, Modified, UserID, TotalConsultedAmount)
    VALUES (@DoctorID, @PatientID, @AppointmentDate, @AppointmentStatus, @Description, @SpecialRemarks, GETDATE(), GETDATE(), @UserID, @TotalConsultedAmount)
END

exec sp_Appointment_Insert 2,1,'2025-01-01','pending','abc','abc',2,45.20

----

CREATE proc sp_Appointment_select
AS
BEGIN
    SELECT * FROM Appointment
END

exec sp_Appointment_select


----

CREATE proc sp_Appointment_selectById
    @AppointmentID INT
AS
BEGIN
    SELECT * FROM Appointment WHERE AppointmentID = @AppointmentID
END

exec sp_Appointment_selectById 1

--------

CREATE proc sp_Appointment_Update
    @AppointmentID INT,
    @DoctorID INT,
    @PatientID INT,
    @AppointmentDate DATETIME,
    @AppointmentStatus NVARCHAR(20),
    @Description NVARCHAR(250),
    @SpecialRemarks NVARCHAR(100),
    @UserID INT,
    @TotalConsultedAmount DECIMAL(5,2) = NULL
AS
BEGIN
    UPDATE Appointment
    SET DoctorID = @DoctorID,
        PatientID = @PatientID,
        AppointmentDate = @AppointmentDate,
        AppointmentStatus = @AppointmentStatus,
        Description = @Description,
        SpecialRemarks = @SpecialRemarks,
        Modified = GETDATE(),
        UserID = @UserID,
        TotalConsultedAmount = @TotalConsultedAmount
    WHERE AppointmentID = @AppointmentID
END

exec sp_Appointment_Update 1,2,1,'2036-02-01','success','sdfsdf','efewfew',2,45


----


CREATE proc sp_Appointment_Delete
    @AppointmentID INT
AS
BEGIN
    DELETE FROM Appointment WHERE AppointmentID = @AppointmentID
END

exec sp_Appointment_Delete 1


