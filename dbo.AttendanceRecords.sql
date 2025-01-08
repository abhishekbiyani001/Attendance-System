CREATE TABLE [dbo].[AttendanceRecords] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [StudentId] INT           NOT NULL,
    [Date]      DATETIME2 (7) NOT NULL,
    [IsPresent] BIT           NOT NULL,
    [StudentName] NCHAR(20) NOT NULL, 
    CONSTRAINT [PK_AttendanceRecords] PRIMARY KEY CLUSTERED ([Id] ASC)
);

