CREATE TABLE [dbo].[Grade_History]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[EnrollmentId] INT NOT NULL,
	[StudentId] INT NOT NULL,
	[CourseId] INT NOT NULL,
	Grade DECIMAL(5,2) NOT NULL,
	Updated_On DATETIME NOT NULL,
	[Updated_By] INT NOT NULL
    CONSTRAINT [FK_Grade_History_Enrollments] FOREIGN KEY ([EnrollmentId]) REFERENCES [Enrollment]([EnrollmentId]),
	CONSTRAINT [FK_Grade_History_Instructors] FOREIGN KEY ([Updated_By]) REFERENCES [Instructors]([InstructorId]),
)
