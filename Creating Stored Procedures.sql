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
  @Id VARCHAR(20), @Name VARCHAR(10), @Surname VARCHAR(10), @Image Image, @DOB Date, @Gender VARCHAR(10), @Phone VARCHAR(11), @Address VARCHAR(60)
)
AS
BEGIN
  UPDATE Student SET
	Name = @Name, Surname = @Surname, Image = @Image, DOB = @DOB, Gender = @Gender, Phone = @Phone, Address = @Address
	WHERE StudentNumber = @Id
END