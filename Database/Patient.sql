USE [ClinicManagementSystemDB]
GO

/****** Object:  Table [dbo].[Patient]    Script Date: 5/29/2024 3:28:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Patient](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[PatientFirstName] [varchar](50) NOT NULL,
	[PatientMiddleName] [varchar](50) NULL,
	[PatientLastName] [varchar](50) NOT NULL,
	[PatientBirthDate] [date] NOT NULL,
	[PatientEmail] [nvarchar](255) NULL,
	[PatientPhoneNumber] [nvarchar](50) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

