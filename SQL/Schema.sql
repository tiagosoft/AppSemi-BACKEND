
CREATE DATABASE AppSemi;

--tabla pacientes
CREATE TABLE [dbo].[Patients](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
)
--tabla ordenes

CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[PatientId] [int] NOT NULL,
	[AttentionDate] [datetime] NOT NULL,
	[CreatedAt] [datetime] NOT NULL
)
--tabla examenes

CREATE TABLE [dbo].[Exams](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Code] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL
)
--tabla ordenes

CREATE TABLE [dbo].[OrderExams](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[OrderId] [int] NOT NULL,
	[ExamId] [int] NOT NULL,

	 FOREIGN KEY (OrderId) REFERENCES Orders(Id),
	 FOREIGN KEY (ExamId) REFERENCES Exams(Id)
)

--SP PARA OBTENER PACIENTES
CREATE PROCEDURE sp_GetPatients 
AS  
BEGIN    
    SELECT * FROM Patients 
END 

--SP PARA OBTENER EXAMENES
CREATE PROCEDURE sp_GetExams
AS  
BEGIN    
    SELECT * FROM Exams 
END 

--SP PARA CREAR ORDENES CON NOMBRE DEL PACIENTE

CREATE TYPE ExamIdList AS TABLE
(
    ExamId INT
);

CREATE OR ALTER PROCEDURE sp_CreateOrder
(
    @PatientName VARCHAR(100),
    @AttentionDate DateTime,
    @ExamsId ExamIdList READONLY,
    @OrderId INT OUTPUT
    )
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @PatientId AS INT

    SET @PatientId = (SELECT Id FROM Patients WHERE FullName = @PatientName);

    INSERT INTO Orders (PatientId,AttentionDate, CreatedAt)
    VALUES (@PatientId,@AttentionDate,GETDATE());

    SET @OrderId = SCOPE_IDENTITY();

        INSERT INTO OrderExams(OrderId, ExamId)
    SELECT @OrderId, ExamId
    FROM @ExamsId;

END;

--SP PARA OBTENER ORDENES

CREATE OR ALTER PROCEDURE sp_GetAllOrders
AS
BEGIN
    SELECT 
        o.Id AS OrderId,
        o.PatientId,
        p.FullName,
        o.CreatedAt,
        COUNT(oe.ExamId) ExamCount,
        o.AttentionDate,
        STRING_AGG(e.Name, ', ') AS Exams
    FROM Orders o
    INNER JOIN Patients p ON o.PatientId = p.Id
    INNER JOIN OrderExams oe ON oe.OrderId = o.Id
    INNER JOIN Exams e ON e.Id = oe.ExamId
    GROUP BY o.Id, o.PatientId, p.FullName,  o.CreatedAt,o.AttentionDate
    ORDER BY o.Id;
END

--DETALLE DE UNA ORDER

CREATE OR ALTER PROCEDURE sp_GetOrderDetail 
    @Id INT
AS  
BEGIN    
    SELECT 
        o.Id AS OrderId,
        STRING_AGG(e.Name, ', ') AS Exams
    FROM Orders o
    INNER JOIN OrderExams oe ON oe.OrderId = @Id
    INNER JOIN Exams e ON e.Id = oe.ExamId
    GROUP BY o.Id
    ORDER BY o.Id;
END 

--INSERTAR DATOS EN PACIENTES
INSERT INTO Patients (FullName,CreatedAt)
VALUES ('Santiago Guevara',GETDATE());

INSERT INTO Patients (FullName,CreatedAt)
VALUES ('Laura Ariza',GETDATE());

INSERT INTO Patients (FullName,CreatedAt)
VALUES ('Juan Lopez',GETDATE());

INSERT INTO Patients (FullName,CreatedAt)
VALUES ('Camila Murillo',GETDATE());

--INSERTAR DATOS EN EXAMENES
INSERT INTO Exams (Code,Name)
VALUES ('EX01','Examen Corazon');

INSERT INTO Exams (Code,Name)
VALUES ('EX02','Examen Higado');

INSERT INTO Exams (Code,Name)
VALUES ('EX03','Examen Cabeza');

INSERT INTO Exams (Code,Name)
VALUES ('EX04','Examen Tiroides');

