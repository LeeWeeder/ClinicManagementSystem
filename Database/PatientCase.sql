USE [ClinicManagementSystemDB]
GO

/****** Object:  Table [dbo].[PatientCase]    Script Date: 5/29/2024 3:30:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PatientCase](
	[PatientCaseId] [int] NOT NULL,
	[PatientCaseStartDate] [datetime] NOT NULL,
	[PatientCaseEndDate] [datetime] NULL,
	[PatientCaseStatus] [varchar](50) NOT NULL,
	[PatientCaseDescription] [nvarchar](max) NOT NULL,
	[PatientCasePatientId] [int] NOT NULL,
 CONSTRAINT [PK_PatientCase] PRIMARY KEY CLUSTERED 
(
	[PatientCaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PatientCase]  WITH CHECK ADD  CONSTRAINT [FK_PatientCase_Patient] FOREIGN KEY([PatientCasePatientId])
REFERENCES [dbo].[Patient] ([PatientId])
GO

ALTER TABLE [dbo].[PatientCase] CHECK CONSTRAINT [FK_PatientCase_Patient]
GO

