CREATE DATABASE StudentInfo
ON
(NAME = StudentData1,
FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\StudentData1.mdf',
SIZE = 20 MB,
MAXSIZE = UNLIMITED,
FILEGROWTH = 20%)

LOG ON
(NAME = StudentLog1,
FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\StudentLog1.ldf',
SIZE = 20 MB,
MAXSIZE = UNLIMITED,
FILEGROWTH = 20%)
GO
USE StudentInfo
GO
CREATE TABLE Student 
(
	StudentNumber VARCHAR(20) PRIMARY KEY,
	Name VARCHAR(10) NOT NULL,
	Surname VARCHAR(10) NOT NULL,
	StudentImage IMAGE NULL,
	DOB Date,
	Gender VARCHAR(10),
	Phone VARCHAR(11),
	Address VARCHAR(60)
)

CREATE TABLE Module
(ModuleCode VARCHAR(10) NOT NULL PRIMARY KEY,
Name VARCHAR(20) NOT NULL,
Description VARCHAR(100) NOT NULL,
OnlineResources VARCHAR(60) NOT NULL
)

CREATE TABLE StudentModule
(
	StudentNumber VARCHAR(20) References Student(StudentNumber),
	ModuleCode VARCHAR(10) References Module(ModuleCode)
)

GO

INSERT INTO Module
VALUES 
('DF101', 'Default', 'Default module', ''),
('PRG281', 'Programming 281', 'Advanced Programming course', 'www.stackoverflow.com'),
('PRG282', 'Programming 282', 'Continuation of PRG282', 'www.stackoverflow'),
('DBD281', 'Database Development', 'Advanced Database course', 'w3schools.com/sql/'),
('IOT281', 'Internet Of Things', 'Course on the Internet of Things', 'www.google.com'),
('INF281', 'Information Systems', 'Course on Information Systems', 'www.google.com'),
('STA281', 'Statistics', 'Course on Statistics', 'www.google.com'),
('MAT281', 'Mathematics', 'Course on Mathematics', 'calculator.com'),
('PRG251', 'Programming 251', 'The same as PRG281', 'www.stackoverflow.com'),
('PRG252', 'Programming 252', 'The same as PRG282', 'www.stackoverflow.com'),
('DBD251', 'Database Development', 'Advanced Database course', 'w3schools.com/sql/'),
('IOT251', 'Internet of Things', 'Advanced course on IOT', 'www.google.com'),
('INF251', 'Information Systems', 'Advanced course on Information Systems', 'www.google.com'),
('STA251', 'Statistics', 'Advanced course on Statistics', 'www.google.com'),
('MAT251', 'Mathematics', 'Advanced course on Mathematics', 'calculator.com')

GO

INSERT INTO Student (StudentNumber, Name, Surname, DOB, Gender, Phone, Address)
VALUES
(
		'Er6',
		'Ivor',
		'Everett',
		'1987-09-13',
		'M',
		'0845 46 43',
		'712-1903 Vitae, St.'
	),
	(
		'Mn3',
		'Fulton',
		'Myers',
		'1985-12-31',
		'M',
		'0800 1111',
		'6905 Pellentesque Rd.'
	),
	(
		'Oy7',
		'Bradley',
		'Oneil',
		'1999-05-25',
		'M',
		'024765058',
		'601-4597 Congue. St.'
	),
	(
		'Mn0',
		'Shannon',
		'Malone',
		'1988-02-12',
		'F',
		'0720 5621',
		'778-2818 Ultrices Ave'
	),
	(
		'Ct1',
		'Amethyst',
		'Callahan',
		'1993-08-15',
		'F',
		'01548 817',
		'8789 Augue Rd.'
	),
	(
		'Fc5',
		'Eric',
		'Fuller',
		'1997-10-20',
		'M',
		'03 3652687',
		'1350 Natoque Road'
	),
	(
		'Bh8',
		'Marah',
		'Brooks',
		'1987-05-22',
		'F',
		'0167 5981',
		'7144 Sed Avenue'
	),
	(
		'Kr4',
		'Baxter',
		'Kent',
		'2000-09-16',
		'M',
		'011 290 83',
		'213-6042 Dignissim. St.'
	),
	(
		'De8',
		'Chloe',
		'Dodson',
		'1983-04-19',
		'F',
		'03 303 272',
		'6671 Phasellus Ave'
	),
	(
		'Ay2',
		'Cassady',
		'Aguirre',
		'1990-02-06',
		'F',
		'08 917 72',
		'107-4447 Dui, Street'
	),
	(
		'Dn6',
		'Harrison',
		'Durham',
		'1989-11-11',
		'M',
		'080 533 67',
		'522-9008 Justo Rd.'
	),
	(
		'Ro0',
		'Cleo',
		'Roberson',
		'1998-06-29',
		'F',
		'014 6134',
		'P.O. Box 506, 8991 Sed Road'
	),
	(
		'Be3',
		'Reece',
		'Bradley',
		'1998-09-19',
		'M',
		'01 336 618',
		'Ap #279-8062 Donec Rd.'
	),
	(
		'Cn3',
		'Harrison',
		'Curry',
		'1986-02-28',
		'M',
		'056 44 382',
		'Ap #615-3217 Leo, Rd.'
	),
	(
		'Mz9',
		'Inez',
		'Mays',
		'1991-01-04',
		'F',
		'056 94 851',
		'Ap #836-8016 Accumsan St.'
	),
	(
		'My7',
		'Shelley',
		'Murphy',
		'1992-05-19',
		'F',
		'076 537 23',
		'P.O. Box 474, 7024 Dictum St.'
	),
	(
		'Sa5',
		'Melinda',
		'Short',
		'1989-04-21',
		'F',
		'056 43 018',
		'354-428 Ut St.'
	),
	(
		'Sl4',
		'Uriel',
		'Soto',
		'1992-12-30',
		'F',
		'025 268 32',
		'P.O. Box 508, 2986 Ut Road'
	),
	(
		'Bn8',
		'Raven',
		'Burris',
		'1981-09-30',
		'F',
		'011 314 81',
		'6467 Aliquam Av.'
	),
	(
		'Kr0',
		'Piper',
		'Kennedy',
		'1991-12-25',
		'M',
		'074 715',
		'P.O. Box 209, 1942 Donec Av.'
	)
GO

INSERT INTO StudentModule (StudentNumber, ModuleCode)
VALUES
('Ay2', 'DBD251'),
('Ay2', 'INF251'),
('Ay2', 'IOT251'),
('Ay2', 'MAT251'),
('Ay2', 'PRG251'),
('Ay2', 'PRG252'),
('Ay2', 'STA251'),
('Be3', 'DBD281'),
('Be3', 'INF281'),
('Be3', 'IOT281'),
('Be3', 'MAT281'),
('Be3', 'PRG281'),
('Be3', 'PRG282'),
('Be3', 'STA281'),
('Bh8', 'DBD281'),
('Bh8', 'INF281'),
('Bh8', 'IOT281'),
('Bh8', 'MAT281'),
('Bh8', 'PRG281'),
('Bh8', 'PRG282'),
('Bh8', 'STA281'),
('Cn3', 'DBD251'),
('Cn3', 'INF251'),
('Cn3', 'IOT251'),
('Cn3', 'MAT251'),
('Cn3', 'PRG251'),
('Cn3', 'PRG252'),
('Cn3', 'STA251'),
('Ct1', 'DBD281'),
('Ct1', 'INF281'),
('Ct1', 'IOT281'),
('Ct1', 'MAT281'),
('Ct1', 'PRG281'),
('Ct1', 'PRG282'),
('Ct1', 'STA281'),
('De8', 'DBD281'),
('De8', 'INF281'),
('De8', 'IOT281'),
('De8', 'MAT281'),
('De8', 'PRG281'),
('De8', 'PRG282'),
('De8', 'STA281'),
('Dn6', 'DBD251'),
('Dn6', 'INF251'),
('Dn6', 'IOT251'),
('Dn6', 'MAT251'),
('Dn6', 'PRG251'),
('Dn6', 'PRG252'),
('Dn6', 'STA251'),
('Er6', 'DBD281'),
('Er6', 'INF281'),
('Er6', 'IOT281'),
('Er6', 'MAT281'),
('Er6', 'PRG281'),
('Er6', 'PRG282'),
('Er6', 'STA281'),
('Fc5', 'DBD251'),
('Fc5', 'INF251'),
('Fc5', 'IOT251'),
('Fc5', 'MAT251'),
('Fc5', 'PRG251'),
('Fc5', 'PRG252'),
('Fc5', 'STA251'),
('Kr0', 'DBD251'),
('Kr0', 'INF251'),
('Kr0', 'IOT251'),
('Kr0', 'MAT251'),
('Kr0', 'PRG251'),
('Kr0', 'PRG252'),
('Kr0', 'STA251'),
('Kr4', 'DBD281'),
('Kr4', 'INF281'),
('Kr4', 'IOT281'),
('Kr4', 'MAT281'),
('Kr4', 'PRG281'),
('Kr4', 'PRG282'),
('Kr4', 'STA281'),
('Mn0', 'DBD251'),
('Mn0', 'INF251'),
('Mn0', 'IOT251'),
('Mn0', 'MAT251'),
('Mn0', 'PRG251'),
('Mn0', 'PRG252'),
('Mn0', 'STA251'),
('Mn3', 'DBD251'),
('Mn3', 'INF251'),
('Mn3', 'IOT251'),
('Mn3', 'MAT251'),
('Mn3', 'PRG251'),
('Mn3', 'PRG252'),
('Mn3', 'STA251'),
('My7', 'DBD281'),
('My7', 'INF281'),
('My7', 'IOT281'),
('My7', 'MAT281'),
('My7', 'PRG281'),
('My7', 'PRG282'),
('My7', 'STA281'),
('Mz9', 'DBD281'),
('Mz9', 'INF281'),
('Mz9', 'IOT281'),
('Mz9', 'MAT281'),
('Mz9', 'PRG281'),
('Mz9', 'PRG282'),
('Mz9', 'STA281'),
('Oy7', 'DBD281'),
('Oy7', 'INF281'),
('Oy7', 'IOT281'),
('Oy7', 'MAT281'),
('Oy7', 'PRG281'),
('Oy7', 'PRG282'),
('Oy7', 'STA281'),
('Ro0', 'DBD251'),
('Ro0', 'INF251'),
('Ro0', 'IOT251'),
('Ro0', 'MAT251'),
('Ro0', 'PRG251'),
('Ro0', 'PRG252'),
('Ro0', 'STA251'),
('Sa5', 'DBD281'),
('Sa5', 'INF281'),
('Sa5', 'IOT281'),
('Sa5', 'MAT281'),
('Sa5', 'PRG281'),
('Sa5', 'PRG282'),
('Sa5', 'STA281'),
('Sl4', 'DBD251'),
('Sl4', 'INF251'),
('Sl4', 'IOT251'),
('Sl4', 'MAT251'),
('Sl4', 'PRG251'),
('Sl4', 'PRG252'),
('Sl4', 'STA251')
GO


CREATE PROC spInsertStudent
(
  @Name VARCHAR(10), @Surname VARCHAR(10), @Image Image, @DOB Date, @Gender VARCHAR(10), @Phone VARCHAR(11), @Address VARCHAR(60)
)
AS
BEGIN
  INSERT INTO Student
  (Name, Surname, StudentImage, DOB, Gender, Phone, Address)
  VALUES (@Name, @Surname, @Image, @DOB, @Gender, @Phone, @Address)
END

GO

CREATE PROC spDeleteStudent
(
  @Id VARCHAR(20)
)
AS
BEGIN
  DELETE FROM Student WHERE StudentNumber = @Id
END

GO

CREATE PROC SearchStudent
(
  @Id VARCHAR(20)
)
AS
BEGIN
  SELECT * FROM Student WHERE StudentNumber = @Id
END

GO

CREATE PROC spUpdateStudent
(
  @Id VARCHAR(20), @Name VARCHAR(10), @Surname VARCHAR(10), @Image IMAGE, @DOB Date, @Gender VARCHAR(10), @Phone VARCHAR(11), @Address VARCHAR(60)
)
AS
BEGIN
  UPDATE Student SET
	Name = @Name, Surname = @Surname, StudentImage = @Image, DOB = @DOB, Gender = @Gender, Phone = @Phone, Address = @Address
	WHERE StudentNumber = @Id
END